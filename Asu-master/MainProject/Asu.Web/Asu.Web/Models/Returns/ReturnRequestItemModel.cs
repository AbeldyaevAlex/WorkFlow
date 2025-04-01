namespace Asu.Web.Models.Returns
{
    using FluentValidation.Attributes;
    using Media;
    using Asu.Framework.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Validators.Customization;


    [Validator(typeof(ReturnRequestItemValidator))]
    public class ReturnRequestItemModel : BaseNopEntityModel
    {
        public ReturnRequestItemModel()
        {
            this.SelectedReturnReasonId = 0;
        }

        [Key]
        public int OrderLineId { get; set; }

        public long? OrderItemId { get; set; }

        public int Quantity { get; set; }

        public int SelectedQuantity { get; set; }

        public long? AssociatedOrderLineId { get; set; }

        public string AssociatedProductName { get; set; }

        public bool IsWarranty { get; set; }

        public string Comment { get; set; }

        public int SelectedReturnReasonId { get; set; }

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

        public string ImagePath { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public OrderItemModel OrderItem { get; set; }
    }
}