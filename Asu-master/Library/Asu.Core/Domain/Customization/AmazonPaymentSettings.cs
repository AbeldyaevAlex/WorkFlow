namespace Asu.Core.Domain.Customization
{
    using Asu.Core.Configuration;

    public class AmazonPaymentSettings : ISettings
    {
        public string MerchantId { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationVersion { get; set; }

        public string Region { get; set; }

        public string AccessKey { get; set; }

        public string SecretAccessKey { get; set; }

        public string ClientId { get; set; }

        public string CertCn { get; set; }

        public string ServiceUrl { get; set; }

        public string WidgetUrl { get; set; }

        public string CbaUrlPrefix { get; set; }

        public string AmazonClientId { get; set; }

        public string Environment => this.UseSandbox ? "sandbox" : "live";

        public bool UseSandbox { get; set; }

        public string SandboxScriptUrl { get; set; }

        public string LiveScriptUrl { get; set; }

        public TransactMode TransactMode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to "additional fee" is specified as percentage. true - percentage, false - fixed value.
        /// </summary>
        public bool AdditionalFeePercentage { get; set; }
        /// <summary>
        /// Additional fee
        /// </summary>
        public decimal AdditionalFee { get; set; }
    }
}
