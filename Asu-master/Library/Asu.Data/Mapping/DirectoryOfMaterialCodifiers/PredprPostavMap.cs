using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class PredprPostavMap : NopEntityTypeConfiguration<PredprPostav>
    {
        public PredprPostavMap()
        {
            this.ToTable("Predpr_Postav");
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.StatusDocument).WithMany().HasForeignKey(x => x.StatusDocumentId).WillCascadeOnDelete(false);
        }
    }
}
