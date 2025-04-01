using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ModelMap : NopEntityTypeConfiguration<Model>
    {
        public ModelMap()
        {
            this.ToTable("WCS_Model");
            this.HasKey(t => t.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(128);
            this.Property(p => p.IsActiveForFilter).IsRequired();
            this.Property(p => p.IsActiveForSeo).IsRequired();
        }
    }
}
