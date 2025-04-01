using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.ProductGroups
{
    using System;
    using System.Collections.Generic;

    public class DigitalData : BaseEntity
    {
        private ICollection<ProductGroupDigitalData> productGroupDigitalData;
        private ICollection<BrandDigitalData> brandDigitalData;

        public string Path { get; set; }

        public string Name { get; set; }

        public DigitalDataType Type { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual ICollection<ProductGroupDigitalData> ProductGroupDigitalData
        {
            get { return this.productGroupDigitalData ?? (this.productGroupDigitalData = new List<ProductGroupDigitalData>()); }
            protected set { this.productGroupDigitalData = value; }
        }

        public virtual ICollection<BrandDigitalData> BrandDigitalData
        {
            get { return this.brandDigitalData ?? (this.brandDigitalData = new List<BrandDigitalData>()); }
            protected set { this.brandDigitalData = value; }
        }
    }
}
