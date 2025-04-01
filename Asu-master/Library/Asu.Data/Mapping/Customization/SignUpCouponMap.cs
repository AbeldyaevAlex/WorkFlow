using Asu.Core.Domain.Customization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Data.Mapping.Customization
{
    public partial class SignUpCouponMap : NopEntityTypeConfiguration<SignUpCoupon>
    {
        public SignUpCouponMap()
        {
            this.ToTable("WCS_SignUpCoupon");
            this.HasKey(suc => suc.Id);

            this.Property(suc => suc.Email).HasMaxLength(64).IsRequired();
            this.Property(suc => suc.SignUpTime).IsRequired();
        }
    }
}
