namespace Asu.Core.Domain.Seo
{
    public partial class ProductGroupUrlRecord : BaseEntity
    {
        public int? ParentEntityId { get; set; }

        public string ParentEntitySlug { get; set; }

        public int EntityId { get; set; }

        public string EntitySlug { get; set; }

        public GroupEntityType EntityType { get; set; }
    }
}