namespace Asu.Core.Domain.FreshdeskTickets
{
    using Asu.Core.Configuration;

    public class FreshdeskSettings : ISettings
    {
        public string CreateTicketUrl{ get; set; }

        public string ApiKey { get; set; }

        public long SupportGroupId { get; set; }

        public long AmazonCanadaGroupId { get; set; }

        public long AmazonGroupId { get; set; }

        public long EbayGroupId { get; set; }

        public long WalmartGroupId { get; set; }

        public string SupportEmail { get; set; }

        public string WalmartSupportEmail { get; set; }

        public string EbaySupportEmail { get; set; }

        public string AmazonCanadaSupportEmail { get; set; }

        public string AmazonSupportEmail { get; set; }

        public long SupportEmailId { get; set; }

        public long AmazonSupportEmailId { get; set; }

        public long EbaySupportEmailId { get; set; }

        public long WalmartSupportEmailId { get; set; }
    }
}
