using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    public class ManufacturerPiesCategory : BaseEntity
    {
        public int ManufacturerId { get; set; }

        public int CategoryId { get; set; }
    }
}
