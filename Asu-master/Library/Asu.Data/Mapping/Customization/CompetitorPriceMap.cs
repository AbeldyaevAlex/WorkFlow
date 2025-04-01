using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Customization
{
    public partial class CompetitorPriceMap : NopEntityTypeConfiguration<CompetitorPrice>
    {
        public CompetitorPriceMap()
        {
            this.ToTable("vw_CompetitorPrice");
            this.HasKey(cp => new { cp.ProductId, cp.StoreName });

            this.Property(cp => cp.StoreName).HasMaxLength(250).IsRequired().HasColumnName("SellerName");
            this.Property(cp => cp.Price).IsRequired();

            this.HasRequired(cp => cp.Product).WithMany(p => p.CompetitorPrices).HasForeignKey(cp => cp.ProductId);

            this.Ignore(cp => cp.Id);
        }
    }
}
