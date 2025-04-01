namespace Asu.Core.Domain.Shipping
{
    public partial class ZipPrefix : BaseEntity
    {
        public string Prefix { get; set; }
        public string State { get; set; }
    }
}
