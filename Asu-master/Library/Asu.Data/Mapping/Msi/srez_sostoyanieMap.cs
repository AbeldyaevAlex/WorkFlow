using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Msi
{
    public partial class srez_sostoyanieMap : NopEntityTypeConfiguration<srez_sostoyanie>
    {
        public srez_sostoyanieMap()
        {
            this.ToTable("srez_sostoyanie");
            this.HasKey(l => l.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.PviId).WillCascadeOnDelete(false);
        }
    }
}
