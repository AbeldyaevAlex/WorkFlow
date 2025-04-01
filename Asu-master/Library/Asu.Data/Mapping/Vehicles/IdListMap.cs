using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class IdListMap : NopEntityTypeConfiguration<IdList>
    {
        public IdListMap()
        {
            this.HasKey(t => t.Id);
        }
    }
}
