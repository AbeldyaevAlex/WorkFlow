using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprKgrMap : NopEntityTypeConfiguration<SprKgr>
    {
        public SprKgrMap()
        {
            this.ToTable("Spr_kgr");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Spr_pvi)
                .WithMany()
                .HasForeignKey(x => x.Spr_pviId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.StatusDocument)
                .WithMany()
                .HasForeignKey(x => x.StatusDocumentId)
                .WillCascadeOnDelete(false);
        }
    }
}
