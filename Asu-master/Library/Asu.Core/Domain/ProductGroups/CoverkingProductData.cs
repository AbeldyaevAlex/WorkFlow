using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.ProductGroups
{
    public class CoverkingProductData : BaseEntity
    {
        public string ItemId { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Upc { get; set; }

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public int ProductFamilyId { get; set; }

        public string ProductFamilyDescription { get; set; }
    }
}
