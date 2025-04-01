using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    using System.Collections;

    public class SpecificationAttributeDescriptor : BaseEntity
    {
        public int AttributeId { get; set; }

        public int DescriptorAttributeId { get; set; }

        public int DisplayOrder { get; set; }

        public string DisplayName { get; set; }

        public virtual SpecificationAttribute Attribute { get; set; }
    }
}
