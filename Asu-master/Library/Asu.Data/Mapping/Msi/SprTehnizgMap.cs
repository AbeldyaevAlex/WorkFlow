using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprTehnizgMap : NopEntityTypeConfiguration<Spr_Tehnizg>
    {
        public SprTehnizgMap()
        {
            this.ToTable("Spr_Tehnizg");
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
