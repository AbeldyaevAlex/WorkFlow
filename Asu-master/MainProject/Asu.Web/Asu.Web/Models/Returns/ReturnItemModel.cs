namespace Asu.Web.Models.Returns
{
    using System;

    using Asu.Web.Models.Media;

    public class ReturnItemModel
    {
        public long? OrderItemId { get; set; }

        public int OrderLineId { get; set; }

        public int? AssociatedOrderLineId { get; set; }

        public bool IsWarranty { get; set; }

        public int Quantity { get; set; }

        public OrderItemModel OrderItem { get; set; }

        public DateTime UpdatedOn { get; set; }

        public PictureModel Picture { get; set; }

        public bool IsImageLoader
        {
            get
            {
                if (string.IsNullOrEmpty(this.Picture?.ImageUrl))
                {
                    return false;
                }

                return this.Picture.ImageUrl.Contains("ImageLoader/");
            }
        }
    }
}