namespace Asu.Core.Domain.Customization
{
    public partial class GoogleImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string PicturePath { get; set; }
    }
}