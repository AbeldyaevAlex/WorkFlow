using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Asu.Web.CustomAttribute
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
    public static class Role
    {
        public const string Administrator = "Admin";
        public const string User = "User";
        public const string Guest = "Guest";
        public const string Employee_BNM = "Employee_BNM";
        public const string Employee_OGMET = "Employee_OGMET";

        public const string AdministratorOrUser = User + "," + Guest;

    }
}