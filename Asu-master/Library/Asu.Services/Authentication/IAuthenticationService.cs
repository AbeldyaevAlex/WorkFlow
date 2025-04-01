using Asu.Core.Domain.Customers;

namespace Asu.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        void SignIn(Customer customer, bool createPersistentCookie);
        void SignOut();
        Customer GetAuthenticatedCustomer();
    }
}