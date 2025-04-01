using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core
{
    public class ConstantStorage
    {
        public const int US = 1;

        public const string DefaultShippingMethodName = "FREE GROUND SHIPPING";

        public const string USZipCodeValidationRegex = "^[0-9]{5}(?:-[0-9]{4})?$";

        public const string AddressValidationRegex = "^((?!.*p-o-b-o-x|P.O. Box|PO box|PO |Postal.*).)*$";

        public const string CC_SUBMIT_ATTEMPT_KEY = "CcSubmitAttempt";

        public const string CAPTCHA_RESPONSE_KEY = "g-recaptcha-response";

        public const string CAPTCHA_VALID_KEY = "captchaValid";

        public const string TWO_DAY_SHIPPING_METHOD_NAME = "2 Day Shipping";

        public const string TWO_DAY_CLUB_SHIPPING_METHOD_NAME = "2 Day Club Delivery";

        public static readonly int[] SHIPPING_INSURANCE_PRODUCT_IDS = { 15365223, 15425574, 15425575, 15425576, 15425577, 15425578, 15425579 };

        public static readonly int[] RETURN_EXTENSION_PRODUCT_IDS = { 15563513, 15563514, 15563515, 15563516, 15563517, 15563518, 15563519 };
    }
}
