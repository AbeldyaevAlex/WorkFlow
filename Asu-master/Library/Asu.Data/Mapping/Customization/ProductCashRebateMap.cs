using Asu.Core.Domain.Customization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Customization
{
    public partial class ProductCashRebateMap : NopEntityTypeConfiguration<ProductCashRebate>
    {
        public ProductCashRebateMap()
        {
            this.ToTable("WC_ProductCashRebate");
            this.HasKey(pr => pr.ProductId);

            this.Property(pr => pr.RebateAmount).IsRequired();

            this.Ignore(re => re.Id);
        }
    }
}
