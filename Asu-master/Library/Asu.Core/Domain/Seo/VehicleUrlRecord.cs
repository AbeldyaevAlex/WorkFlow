namespace Asu.Core.Domain.Seo
{
    /// <summary>
    /// WC. For Vehicles SEO links
    /// </summary>
    public partial class VehicleUrlRecord : BaseEntity
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int MakeId { get; set; }
        public int? ModelId { get; set; }
        public int? YearId { get; set; }
    }
}