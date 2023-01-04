namespace GoogleShoppingScraper.Errors
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;

    using Properties;
    using Repository;

    using Scraping;

    public class Logger
    {
        private readonly string htmlFilesPath;
        private readonly string mailTo;
        private readonly string mailSubject;

        public Logger(string htmlFilesPath, string mailTo, string mailSubject)
        {
            this.htmlFilesPath = htmlFilesPath;
            this.mailTo = mailTo;
            this.mailSubject = mailSubject;
        }

        public void AddLog(Keyword keyword, CustomException exception)
        {
            try
            {
                long logId;
                using (var context = new DatabaseContext(Settings.Default.ScraperConnectionString))
                {
                    var log = new Log
                    {
                        ProductId = keyword.ProductId,
                        SearchTerm = keyword.SearchTerm,
                        ErrorType = (int)exception.ErrorType,
                        Message = exception.Message
                    };

                    context.Logs.InsertOnSubmit(log);
                    context.SubmitChanges();
                    logId = log.Id;
                }

                if (exception.SaveToFile)
                {
                    this.SaveHtmlFile(logId, exception.PageText);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void AddLog(Keyword keyword, Exception exception)
        {
            try
            {
                using (var context = new DatabaseContext(Settings.Default.ScraperConnectionString))
                {
                    var log = new Log
                    {
                        ProductId = keyword.ProductId,
                        SearchTerm = keyword.SearchTerm,
                        Message = $"{exception.Message} Stack: {exception.StackTrace}",
                        ErrorType = (int)ErrorType.Unhandled
                    };

                    context.Logs.InsertOnSubmit(log);
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public void SendMail(string body)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mailSettings = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (mailSettings != null)
            {
                var port = mailSettings.Smtp.Network.Port;
                var from = mailSettings.Smtp.From;
                var host = mailSettings.Smtp.Network.Host;
                var password = mailSettings.Smtp.Network.Password;
                var userName = mailSettings.Smtp.Network.UserName;
                var enableSsl = mailSettings.Smtp.Network.EnableSsl;
                var defaultCredentials = mailSettings.Smtp.Network.DefaultCredentials;

                var message = new MailMessage { From = new MailAddress(from) };

                message.To.Add(new MailAddress(this.mailTo));
                message.CC.Add(new MailAddress(from));
                message.Subject = this.mailSubject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    UseDefaultCredentials = defaultCredentials,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = enableSsl
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void SaveHtmlFile(long logId, string pageContent)
        {
            try
            {
                var directory = new DirectoryInfo(this.htmlFilesPath);
                var today = DateTime.UtcNow.ToString("MM-dd-yyyy");
                if (directory.GetDirectories().All(i => i.Name != today))
                {
                    directory.CreateSubdirectory(today);
                }

                var path = directory.GetDirectories(today).Single().FullName;
                var fileName = Path.Combine(path, $"{logId}.html");

                using (var writer = new StreamWriter(fileName))
                {
                    writer.Write(pageContent);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
