namespace Asu.Web.Models.Media
{
    using Asu.Core.Domain.ProductGroups;
    using Asu.Framework.Mvc;

    public class DigitalDataModel : BaseNopModel
    {
        public DigitalDataModel()
        {
            this.Url = string.Empty;
            this.ThumbUrl = string.Empty;
        }

        public string Url { get; set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }

        public DigitalDataType Type  { get; set; }
    }
}