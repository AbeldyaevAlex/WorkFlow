using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprCexPriznMap : NopEntityTypeConfiguration<Spr_cex_prizn>
    {
        public SprCexPriznMap()
        {
            this.ToTable("Spr_cex_prizn");
            this.HasKey(l => l.Id);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

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
