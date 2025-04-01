using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class PopularModelMap : NopEntityTypeConfiguration<PopularModel>
    {
        public PopularModelMap()
        {
            this.ToTable("WCS_PopularModels");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.Year, m.MakeId, m.ModelId });
        }
    }
}
