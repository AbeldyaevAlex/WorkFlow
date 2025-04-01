using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class DokumObosnovMap : NopEntityTypeConfiguration<DokumObosnov>
    {
        public DokumObosnovMap()
        {
            this.ToTable("Docum_Obosnov");
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.StatusDocument).WithMany().HasForeignKey(x => x.StatusDocumentId).WillCascadeOnDelete(false);
        }
    }
}
