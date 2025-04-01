using Asu.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class AspNetRolesController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public AspNetRolesController()
        {
        }

        public AspNetRolesController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ActionResult GetAllRoles()
        {
            List<AspNetRolesViewModel> listRoles = new List<AspNetRolesViewModel>();
            foreach (var role in RoleManager.Roles)
            {
                listRoles.Add(new AspNetRolesViewModel(role));
            }
            return View(listRoles);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AspNetRolesViewModel model)
        {
            var role = new AppLicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("GetAllRoles", "AspNetRoles");
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new AspNetRolesViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(AspNetRolesViewModel model)
        {
            var role = new AppLicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("GetAllRoles", "AspNetRoles");
        }
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new AspNetRolesViewModel(role));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            Session["RoleId"] = id;
            return View(new AspNetRolesViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed()
        {
            string id = Session["RoleId"].ToString();
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("GetAllRoles", "AspNetRoles");
        }
    }
}