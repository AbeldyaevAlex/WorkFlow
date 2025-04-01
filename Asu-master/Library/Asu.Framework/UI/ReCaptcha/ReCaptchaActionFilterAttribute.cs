namespace Asu.Framework.UI.ReCaptcha
{
    using System.Web.Mvc;

    using Asu.Core;
    using Asu.Core.Infrastructure;
    using Asu.Services.Configuration;
    using Asu.Framework.Controllers;
    using Asu.Framework.UI.Captcha;

    public class ReCaptchaValidatorAttribute : ActionFilterAttribute
    {
        private const string CC_SUBMIT_ATTEMPT_KEY = ConstantStorage.CC_SUBMIT_ATTEMPT_KEY;
        private const string CAPTCHA_RESPONSE_KEY = ConstantStorage.CAPTCHA_RESPONSE_KEY;
        private const string CAPTCHA_VALID_KEY = ConstantStorage.CAPTCHA_VALID_KEY;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var attempt = 0;
            var valid = false;
            var ccSubmitAttempt = filterContext.HttpContext.Session[CC_SUBMIT_ATTEMPT_KEY];
            if (ccSubmitAttempt != null)
            {
                attempt = (int)ccSubmitAttempt;
            }

            filterContext.HttpContext.Session[CC_SUBMIT_ATTEMPT_KEY] = ++attempt;

            if (attempt >= 3)
            {
                var captchaResponseValue = filterContext.HttpContext.Request[CAPTCHA_RESPONSE_KEY];
                if (!string.IsNullOrEmpty(captchaResponseValue))
                {
                    if (!string.IsNullOrEmpty(captchaResponseValue))
                    {
                        var storeContext = EngineContext.Current.Resolve<IStoreContext>();
                        var settingService = EngineContext.Current.Resolve<ISettingService>();
                        var captchaSettings = settingService.LoadSetting<CaptchaSettings>(storeContext.CurrentStore.Id);

                        var controller = new CustomController(captchaSettings);
                        var response = controller.ValidateCaptcha(captchaResponseValue);
                        valid = response != null && response.Success;
                    }
                }

                filterContext.ActionParameters[CAPTCHA_VALID_KEY] = valid;
            }
           

            base.OnActionExecuting(filterContext);
        }
    }
}
