using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.ProductGroups
{
    public class ProductGroupDigitalData : BaseEntity
    {
        public int ProductGroupId { get; set; }

        public int DigitalDataId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual DigitalData DigitalData { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}
