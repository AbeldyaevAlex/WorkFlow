using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprZakazMap : NopEntityTypeConfiguration<Spr_Zakaz>
    {
        public SprZakazMap()
        {
            this.ToTable("Spr_Zakaz");
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
