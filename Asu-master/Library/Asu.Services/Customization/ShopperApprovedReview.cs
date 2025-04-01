namespace Asu.Services.Customization
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "review")]
    public class ShopperApprovedReview
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "location")]
        public string Location { get; set; }

        [XmlElement(ElementName = "justlocation")]
        public string JustLocation { get; set; }

        [XmlElement(ElementName = "displaydate")]
        public string DisplayDate { get; set; }

        [XmlElement(ElementName = "textcomments")]
        public string TextComments { get; set; }

        [XmlElement(ElementName = "fullurl")]
        public string FullUrl { get; set; }

        [XmlElement(ElementName = "orderid")]
        public int? OrderId { get; set; }

        [XmlElement(ElementName = "Overall")]
        public string Overall { get; set; }

        [XmlElement(ElementName = "Product")]
        public string Product { get; set; }

        [XmlElement(ElementName = "Delivery")]
        public string Delivery { get; set; }

        [XmlElement(ElementName = "public")]
        public string Public { get; set; }

        [XmlElement(ElementName = "verified")]
        public string Verified { get; set; }

        [XmlElement(ElementName = "followup")]
        public string FollowUp { get; set; }
    }
}
