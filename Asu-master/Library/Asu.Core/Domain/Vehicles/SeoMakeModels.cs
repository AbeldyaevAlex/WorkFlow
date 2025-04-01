namespace Asu.Core.Domain.Vehicles
{
    public class SeoMakeModel : BaseEntity
    {
        public int MakeId { get; set; }

        public int? ModelId { get; set; }

        public bool Remove { get; set; }

        public int StoreId { get; set; }

        public bool IsActive { get; set; }

        public virtual Make Make { get; set; }

        public virtual Model Model { get; set; }
    }
}
