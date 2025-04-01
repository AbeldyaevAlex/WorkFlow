using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class SeoMakeModelMap : NopEntityTypeConfiguration<SeoMakeModel>
    {
        public SeoMakeModelMap()
        {
            this.ToTable("WCS_SEOMakeModels");
            this.HasKey(t => t.Id);
            this.Property(p => p.MakeId).IsRequired();
            this.Property(p => p.Remove).IsRequired();
            this.Property(p => p.StoreId).IsRequired();
            this.Property(p => p.IsActive).IsRequired();
        }
    }
}
