using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class SignUpCoupon : BaseEntity
    {
        public SignUpCoupon(string email)
        {
            this.Email = email;
            this.SignUpTime = DateTime.Now;
        }

        public string Email { get; set; }

        public DateTime SignUpTime { get; set; }
    }
}
