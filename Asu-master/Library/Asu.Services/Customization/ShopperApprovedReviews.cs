namespace Asu.Services.Customization
{
    using System.Xml.Serialization;

    [XmlRoot("reviews")]
    public class ShopperApprovedReviews
    {
        [XmlElement("review")]
        public ShopperApprovedReview[] Reviews { get; set; }
    }
}