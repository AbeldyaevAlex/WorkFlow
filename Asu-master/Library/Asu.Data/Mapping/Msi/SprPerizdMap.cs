using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprPerizdMap : NopEntityTypeConfiguration<Spr_Perizd>
    {
        public SprPerizdMap()
        {
            this.ToTable("Spr_Perizd");
            this.HasKey(l => l.Id);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.Spr_pvi)
              .WithMany()
              .HasForeignKey(x => x.PviId)
              .WillCascadeOnDelete(false);
        }
    }
}
