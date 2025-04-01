using Asu.Core.Domain.Customization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Customization
{
    public partial class AmazonPaymentAdvancedTaskMap : NopEntityTypeConfiguration<AmazonPaymentAdvancedTask>
    {
        public AmazonPaymentAdvancedTaskMap()
        {
            this.ToTable("WCS_AmazonPaymentAdvancedTask");
            this.HasKey(apat => apat.Id);

            this.Property(apat => apat.IsBusy).IsRequired();
            this.Property(apat => apat.UpdatedOn).IsRequired();
        }
    }
}
