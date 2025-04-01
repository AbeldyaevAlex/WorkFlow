namespace Asu.Web.Models.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public class ProductGroupResourceModel
    {
        public ProductGroupResourceModel()
        {
            this.Url = string.Empty;
            this.ThumbUrl = string.Empty;
        }

        public string Url { get; set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }
    }
}