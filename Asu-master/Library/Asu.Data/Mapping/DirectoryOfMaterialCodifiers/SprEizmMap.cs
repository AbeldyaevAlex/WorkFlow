using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprEizmMap : NopEntityTypeConfiguration<SprEizm>
    {
        public SprEizmMap()
        {
            this.ToTable("Spr_eizm");
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
