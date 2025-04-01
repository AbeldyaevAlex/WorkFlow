using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models
{
    public class AspNetRolesViewModel
    {
        public AspNetRolesViewModel()
        {

        }
        public AspNetRolesViewModel(AppLicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}