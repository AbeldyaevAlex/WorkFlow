namespace Asu.Services.Customization
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;

    using Core.Domain.FreshdeskTickets;

    using Asu.Core;
    using Asu.Core.Domain.Customization;
    using Asu.Core.Domain.Orders;
    using Asu.Services.Configuration;

    public class FreshdeskTicketService : IFreshdeskTicketService
    {
        private readonly IStoreContext storeContext;
        private readonly FreshdeskSettings freshdeskSettings;
        private readonly ISettingService settingService;

        public FreshdeskTicketService(IStoreContext storeContext, ISettingService settingService)
        {
            this.storeContext = storeContext;
            this.settingService = settingService;
            this.freshdeskSettings = this.settingService.LoadSetting<FreshdeskSettings>(this.storeContext.CurrentStore.Id);
        }

        public CreateTicketResponse CreateTicket(CreateTicket ticket, Channel channel)
        {
            var request = new CreateTicketRequest
            {
                Ticket = ticket,
                CcEmails = ticket.CcEmail
            };

            try
            {
                string errorMessage;
                var responseBody = this.SendRequest(this.GetCreateTicketUrl(channel), JsonConvert.SerializeObject(request), RequestMethod.Post, channel, out errorMessage);
                if (!string.IsNullOrEmpty(responseBody))
                {
                    return JsonConvert.DeserializeObject<CreateTicketResponse>(responseBody);
                }

                return new CreateTicketResponse { ErrorMessage = errorMessage };

            }
            catch (Exception exception)
            {
                return new CreateTicketResponse { ErrorMessage = exception.Message };
            }
        }

        public string GetCreateTicketUrl(Channel channel)
        {
            string url = null;
            switch (channel)
            {
                case Channel.RpmWareOld:
                case Channel.RpmWareNew:
                case Channel.ManualOrdersThm:
                    url = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Thmotorsports).CreateTicketUrl;
                    break;
                default:
                    url = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Autoplicity).CreateTicketUrl;
                    break;
            }

            return url;
        }

        public string GetApiKey(Channel channel)
        {
            string apiKey = null;
            switch (channel)
            {
                case Channel.Thmotorsports:
                case Channel.RpmWareOld:
                case Channel.RpmWareNew:
                case Channel.ManualOrdersThm:
                    apiKey = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Thmotorsports).ApiKey;
                    break;
                default:
                    apiKey = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Autoplicity).ApiKey;
                    break;
            }

            return apiKey;
        }

        public long GetReturnsDepartmentGroupId(Channel channel)
        {
            var groupId = 0L;
            switch (channel)
            {
                case Channel.Autoplicity:
                case Channel.ManualOrdersAp:
                case Channel.CycleplicityOld:
                case Channel.Cycleplicity:
                case Channel.GoogleExpressAp:
                    groupId = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Autoplicity).SupportGroupId;
                    break;
                case Channel.Boatplicity:
                    groupId = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Boatplicity).SupportGroupId;
                    break;
                case Channel.Thmotorsports:
                case Channel.RpmWareOld:
                case Channel.RpmWareNew:
                case Channel.ManualOrdersThm:
                    groupId = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Thmotorsports).SupportGroupId;
                    break;
                case Channel.Amazon:
                    groupId = this.freshdeskSettings.AmazonGroupId;
                    break;
                case Channel.Ebay:
                    groupId = this.freshdeskSettings.EbayGroupId;
                    break;
                case Channel.Walmart:
                    groupId = this.freshdeskSettings.WalmartGroupId;
                    break;
                case Channel.AmazonCanada:
                    groupId = this.freshdeskSettings.AmazonCanadaGroupId;
                    break;
            }

            return groupId;
        }

        public long GetSupportEmailId(Channel channel)
        {
            var id = 0L;
            switch (channel)
            {
                case Channel.Autoplicity:
                case Channel.ManualOrdersAp:
                case Channel.CycleplicityOld:
                case Channel.Cycleplicity:
                case Channel.GoogleExpressAp:
                    id = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Autoplicity).SupportEmailId;
                    break;
                case Channel.Boatplicity:
                    id = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Boatplicity).SupportEmailId;
                    break;
                case Channel.Thmotorsports:
                case Channel.RpmWareOld:
                case Channel.RpmWareNew:
                case Channel.ManualOrdersThm:
                    id = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Thmotorsports).SupportEmailId;
                    break;
                case Channel.Amazon:
                    id = this.freshdeskSettings.AmazonSupportEmailId;
                    break;
                case Channel.Ebay:
                    id = this.freshdeskSettings.EbaySupportEmailId;
                    break;
                case Channel.Walmart:
                    id = this.freshdeskSettings.WalmartSupportEmailId;
                    break;
                case Channel.AmazonCanada:
                    id = this.freshdeskSettings.AmazonSupportEmailId;
                    break;
            }

            return id;
        }

        public string GetSupportEmail(Channel channel)
        {
            string email = null;
            switch (channel)
            {
                case Channel.Autoplicity:
                case Channel.ManualOrdersAp:
                case Channel.CycleplicityOld:
                case Channel.Cycleplicity:
                case Channel.GoogleExpressAp:
                    email = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Autoplicity).SupportEmail;
                    break;
                case Channel.Boatplicity:
                    email = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Boatplicity).SupportEmail;
                    break;
                case Channel.Thmotorsports:
                case Channel.RpmWareOld:
                case Channel.RpmWareNew:
                case Channel.ManualOrdersThm:
                    email = this.settingService.LoadSetting<FreshdeskSettings>((int)NopStore.Thmotorsports).SupportEmail;
                    break;
                case Channel.Amazon:
                    email = this.freshdeskSettings.AmazonSupportEmail;
                    break;
                case Channel.AmazonCanada:
                    email = this.freshdeskSettings.AmazonCanadaSupportEmail;
                    break;
                case Channel.Ebay:
                    email = this.freshdeskSettings.EbaySupportEmail;
                    break;
                case Channel.Walmart:
                    email = this.freshdeskSettings.WalmartSupportEmail;
                    break;
            }

            return email;
        }

        private string SendRequest(string url, string body, RequestMethod method, Channel channel, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers["Authorization"] = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.GetApiKey(channel)}:X"))}";
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "application/json";
                request.Method = method.GetName();

                var byteArray = new byte[0];
                if (method != RequestMethod.Get)
                {
                    byteArray = Encoding.UTF8.GetBytes(body);
                    request.ContentLength = byteArray.Length;
                }

                if (method == RequestMethod.Get)
                {
                    var response = request.GetResponse();
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            return new StreamReader(stream).ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        var response = request.GetResponse();
                        using (var stream = response.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                return new StreamReader(stream).ReadToEnd();
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            return null;
        }
    }
}