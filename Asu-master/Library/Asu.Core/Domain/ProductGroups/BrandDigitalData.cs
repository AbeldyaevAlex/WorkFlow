using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.ProductGroups
{
    using Asu.Core.Domain.Catalog;

    public class BrandDigitalData : BaseEntity
    {
        public int ManufacturerId { get; set; }

        public int DigitalDataId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual DigitalData DigitalData { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
