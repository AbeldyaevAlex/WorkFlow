using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using Asu.Core.CustomerAsu;

namespace Asu.Data
{
    public class AppLicationRole : IdentityRole
    {
        public AppLicationRole() : base() { }
        public AppLicationRole(string roleName) : base(roleName)
        {

        }
    }
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("ApplicationServices")
        //: base("AsuAviaContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Asu.Web.Models.AspNetRolesViewModel> AspNetRolesViewModels { get; set; }
    }
}
