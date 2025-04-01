using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asu.Web.Models;
using Asu.Web.ViewModel;
using Asu.Web.Models.ContextDb;

namespace Asu.Web.Data
{
    public class GetSubTask
    {
        private AsuAviaDbContext context;
        private string NaimTask;
        private string userId;
        public GetSubTask(string naimTask, string userId)
        {
            context = new AsuAviaDbContext();
            NaimTask = naimTask;
            this.userId = userId;
        }
        public IEnumerable<Models.UsersTask.Spr_nm_task> Index()
        {
            //var userId = userDbContext.Users.Where(x => x.Email == User.Identity.Name).Select(j => j.Id).ToList()[0];
            string userName = System.Net.Dns.GetHostName();
            IEnumerable<Models.UsersTask.Spr_nm_task> qwery = GetTask(userId);
            return qwery;
        }
        public IEnumerable<Models.UsersTask.Spr_nm_task> GetTask(string userId)
        {
            var main_Id = context.Spr_nm_task.Where(x => x.Naim_task == NaimTask).Select(k => k.Id).FirstOrDefault();
            var subtask = (from nm_task in context.Spr_nm_task
                           join tasks in context.Tasks
                           on nm_task.Id equals tasks.link_nm_task
                           where nm_task.Id_Roditel == main_Id && tasks.link_user == userId
                           select nm_task).ToList();
            return subtask;
        }
    }
}