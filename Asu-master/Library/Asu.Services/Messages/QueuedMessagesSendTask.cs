using System;
using Asu.Services.Logging;
using Asu.Services.Tasks;
using Asu.Services.Customization;

namespace Asu.Services.Messages
{
    using System.Threading;

    /// <summary>
    /// Represents a task for sending queued message 
    /// </summary>
    public partial class QueuedMessagesSendTask : ITask
    {
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ICustomService _customService; // WC
        private static readonly Random randomizer = new Random();

        private static readonly object locker = new object();
        private const string LOCKER_NAME = "EmailQueueLocker";

        public QueuedMessagesSendTask(IQueuedEmailService queuedEmailService,
            IEmailSender emailSender, ILogger logger, 
            ICustomService customService    // WC
            )
        {
            this._queuedEmailService = queuedEmailService;
            this._emailSender = emailSender;
            this._logger = logger;
            this._customService = customService;    // WC
        }

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute()
        {
            Thread.Sleep(randomizer.Next(3000, 10000));
            lock (locker)
            {
                try
                {
                    if (!this._customService.SetLockedIfUnlocked(LOCKER_NAME, 60 * 60))
                    {
                        return;
                    }
                }
                catch (Exception exc)
                {
                    _logger.Error(string.Format("Error with email queue busy checking. {0}", exc.Message), exc);
                }

                var maxTries = 7;
                var queuedEmails = _queuedEmailService.SearchEmails(null, null, null, null, true, maxTries, false, 0, 10);
                foreach (var queuedEmail in queuedEmails)
                {
                    var bcc = String.IsNullOrWhiteSpace(queuedEmail.Bcc) ? null : queuedEmail.Bcc.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var cc = String.IsNullOrWhiteSpace(queuedEmail.CC) ? null : queuedEmail.CC.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        _emailSender.SendEmail(queuedEmail.EmailAccount,
                            queuedEmail.Subject,
                            queuedEmail.Body,
                           queuedEmail.From,
                           queuedEmail.FromName,
                           queuedEmail.To,
                           queuedEmail.ToName,
                           queuedEmail.ReplyTo,
                           queuedEmail.ReplyToName,
                           bcc,
                           cc,
                           queuedEmail.AttachmentFilePath,
                           queuedEmail.AttachmentFileName);

                        queuedEmail.SentOnUtc = DateTime.UtcNow;
                    }
                    catch (Exception exc)
                    {
                        _logger.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
                    }
                    finally
                    {
                        queuedEmail.SentTries = queuedEmail.SentTries + 1;
                        _queuedEmailService.UpdateQueuedEmail(queuedEmail);
                    }
                }

                _customService.SetUnlocked(LOCKER_NAME);
            }
        }
    }
}
