using Asu.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class AmazonRegisterResult
    {
        public AmazonRegisterResult()
        {
            Errors = new List<string>();
        }
        public Customer NewCustomer { get; set; }
        public string NotEncodedPassword { get; set; }
        public List<string> Errors { get; set; }
    }
}
