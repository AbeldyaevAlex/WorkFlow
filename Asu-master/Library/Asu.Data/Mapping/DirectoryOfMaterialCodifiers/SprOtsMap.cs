using Asu.Core.Domain.DirectoryOfMaterialCodifiers;


namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprOtsMap : NopEntityTypeConfiguration<SprOts>
    {
        public SprOtsMap()
        {
            this.ToTable("Spr_Ots");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.PviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.StatusDocument).WithMany().HasForeignKey(x => x.StatusDocumentId).WillCascadeOnDelete(false);
        }
    }
}
