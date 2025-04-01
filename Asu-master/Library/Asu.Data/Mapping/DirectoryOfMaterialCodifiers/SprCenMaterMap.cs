using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprCenMaterMap : NopEntityTypeConfiguration<SprCenMater>
    {
        public SprCenMaterMap()
        {
            this.ToTable("Spr_cen_mater");
            this.HasKey(a => a.Id);
            this.Property(cmat => cmat.Cmat).HasPrecision(18, 2);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.StatusDocument).WithMany().HasForeignKey(x => x.StatusDocumentId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Currency).WithMany().HasForeignKey(x => x.CurrencyId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.PredprPostav).WithMany().HasForeignKey(x => x.PredprId).WillCascadeOnDelete(true);
            this.HasRequired(a => a.DocumObosnov).WithMany().HasForeignKey(x => x.ObosnovId).WillCascadeOnDelete(true);
        }
    }
}
