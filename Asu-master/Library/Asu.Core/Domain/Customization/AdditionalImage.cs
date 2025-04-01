namespace Asu.Core.Domain.Customization
{
    public partial class AdditionalImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string PictureName { get; set; }
    }
}
