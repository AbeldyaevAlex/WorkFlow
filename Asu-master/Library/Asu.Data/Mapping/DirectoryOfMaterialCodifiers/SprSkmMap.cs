using Asu.Core.Domain.DirectoryOfMaterialCodifiers;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class SprSkmMap : NopEntityTypeConfiguration<SprSkm>
    {
        public SprSkmMap()
        {
            this.ToTable("Spr_Skm");
            this.HasKey(m => m.Id);
            this.Property(skmves => skmves.Ves).HasPrecision(38, 7);

            this.Property(km => km.Km).IsRequired();
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DirectoryOfMaterialName).WithMany().HasForeignKey(x => x.NmSkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.MarkMater).WithMany().HasForeignKey(x => x.MarkaId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.GostMater).WithMany().HasForeignKey(x => x.GostId).WillCascadeOnDelete(false);
            //this.HasRequired(a => a.SprBalSch).WithMany().HasForeignKey(x => x.BalschId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprPrKm).WithMany().HasForeignKey(x => x.PrkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprGrMater).WithMany().HasForeignKey(x => x.GRMaterId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprOgt).WithMany().HasForeignKey(x => x.OgtId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprOts).WithMany().HasForeignKey(x => x.OtsId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprKgr).WithMany().HasForeignKey(x => x.KgrId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprEizm).WithMany().HasForeignKey(x => x.EizmId).WillCascadeOnDelete(false);
        }
    }
}
