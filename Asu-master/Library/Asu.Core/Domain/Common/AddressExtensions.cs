using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Common
{
    public static class AddressExtensions
    {
        public static string GetCustomerFullName(this Address address)
        {
            var fullName = string.Empty;
            var firstName = address.FirstName;
            var lastName = address.LastName;

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                fullName = $"{firstName} {lastName}";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    fullName = firstName;
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    fullName = lastName;
                }
            }

            return fullName;
        }
    }
}
