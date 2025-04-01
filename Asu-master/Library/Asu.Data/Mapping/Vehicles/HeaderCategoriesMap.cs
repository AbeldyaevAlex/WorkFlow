using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    class HeaderCategoriesMap : NopEntityTypeConfiguration<HeaderCategories>
    {
        public HeaderCategoriesMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.RootId).IsRequired();
        }
    }
}