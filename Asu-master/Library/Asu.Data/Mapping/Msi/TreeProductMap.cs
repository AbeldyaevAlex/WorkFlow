using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Msi
{
    public partial class TreeProductMap : NopEntityTypeConfiguration<TreeProduct>
    {
        public TreeProductMap()
        {
            this.ToTable("TreeProduct");
            this.HasKey(l => l.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_agr).WithMany().HasForeignKey(x => x.AgrId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_Perizd).WithMany().HasForeignKey(x => x.PerizdId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.PviId).WillCascadeOnDelete(false);
        }
    }
}
