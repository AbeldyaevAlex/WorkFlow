using Asu.Core.Domain.Pvi;

namespace Asu.Data.Mapping.Pvi
{
    public partial class PviMap : NopEntityTypeConfiguration<Spr_pvi>
    {
        public PviMap() 
        {
            this.ToTable("Spr_pvi");
            this.HasKey(a => a.Id);

            this.Ignore(p => p.PviLevel);
        }
    }
}
