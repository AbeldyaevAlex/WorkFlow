namespace Asu.Core.Domain.Shipping
{
    public partial class ZipCode : BaseEntity
    {
        public string ZIPCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string ZipClass { get; set; }
    }
}
