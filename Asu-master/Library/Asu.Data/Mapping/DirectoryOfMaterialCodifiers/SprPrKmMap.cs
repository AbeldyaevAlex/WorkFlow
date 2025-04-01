using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprPrKmMap : NopEntityTypeConfiguration<SprPrKm>
    {
        public SprPrKmMap()
        {
            this.ToTable("Spr_Prkm");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Spr_pvi)
                .WithMany()
                .HasForeignKey(x => x.Spr_pviId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
