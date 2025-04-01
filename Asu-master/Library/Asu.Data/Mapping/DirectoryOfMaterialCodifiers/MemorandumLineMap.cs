using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class MemorandumLineMap : NopEntityTypeConfiguration<MemorandumLine>
    {
        public MemorandumLineMap()
        {
            this.ToTable("MemorandumLine");
            this.HasKey(m => m.Id);
            this.Property(skmves => skmves.Ves).HasPrecision(38, 7);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DirectoryOfMaterialName).WithMany().HasForeignKey(x => x.NmSkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.MarkMater).WithMany().HasForeignKey(x => x.MarkaId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.GostMater).WithMany().HasForeignKey(x => x.GostId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprBalSch).WithMany().HasForeignKey(x => x.BalschId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprPrKm).WithMany().HasForeignKey(x => x.PrkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprGrMater).WithMany().HasForeignKey(x => x.GRMaterId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.MemorandumBase).WithMany().HasForeignKey(x => x.MemorandumBaseId).WillCascadeOnDelete(false);
        }
    }
}
