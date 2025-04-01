using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprSortamMap : NopEntityTypeConfiguration<SprSortam>
    {
        public SprSortamMap()
        {
            this.ToTable("Spr_Sortam");
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
