using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.DirectoryOfMaterialCodifiers
{
    public partial class MemorandumBaseMap : NopEntityTypeConfiguration<MemorandumBase>
    {
        public MemorandumBaseMap()
        {
            this.ToTable("MemorandumBase");
            this.HasKey(m => m.Id);

            //this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            //this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
        }
    }
}
