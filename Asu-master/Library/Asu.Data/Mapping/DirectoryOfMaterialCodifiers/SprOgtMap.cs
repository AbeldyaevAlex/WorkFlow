using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprOgtMap : NopEntityTypeConfiguration<SprOgt>
    {
        public SprOgtMap()
        {
            this.ToTable("SPR_OGT");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.StatusDocument).WithMany().HasForeignKey(x => x.StatusDocumentId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprPrKm).WithMany().HasForeignKey(x => x.PrkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SortMater).WithMany().HasForeignKey(x => x.SortMaterId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprSortam).WithMany().HasForeignKey(x => x.SortamMaterId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprGrMater).WithMany().HasForeignKey(x => x.GrMaterId).WillCascadeOnDelete(false);
        }
    }
}
