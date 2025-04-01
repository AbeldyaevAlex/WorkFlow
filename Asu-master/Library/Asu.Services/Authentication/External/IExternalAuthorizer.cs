//Contributor:  Nicholas Mayne


using Asu.Services.Authentication.External;

namespace Asu.Services.Authentication.External
{
    /// <summary>
    /// External authorizer
    /// </summary>
    public partial interface IExternalAuthorizer
    {
        AuthorizationResult Authorize(OpenAuthenticationParameters parameters);
    }
}