namespace Asu.Framework.Controllers
{
    using System.IO;
    using System.Net;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    using Asu.Framework.UI.Captcha;
    using Asu.Framework.UI.ReCaptcha;

    public class CustomController : BaseController
    {
        private readonly CaptchaSettings captchaSettings;

        public CustomController(CaptchaSettings captchaSettings)
        {
            this.captchaSettings = captchaSettings;
        }

        [HttpPost]
        public CaptchaResponse ValidateCaptcha(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }

            var client = new WebClient();
            CaptchaResponse captchaResponse = null;
            try
            {
                var verifyUrl = string.Format(this.captchaSettings.ReCaptchaVerifyUrlTemplate, this.captchaSettings.ReCaptchaPrivateKey, response);
                var jsonResult = client.DownloadString(verifyUrl);
                captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult);
            }
            catch (WebException ex)
            {

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var webRresponse = ex.Response as HttpWebResponse;
                    using (var streamReader = new StreamReader(webRresponse.GetResponseStream()))
                    {
                        var responseBody = streamReader.ReadToEnd();
                    }
                }
            }

            return captchaResponse;
        }
    }
}
