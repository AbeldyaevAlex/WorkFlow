using DomainCore;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;


namespace EFCore
{
    public class TaskMeneger : ApiController
    {
        ShopDBEntities2 db = new ShopDBEntities2();
        public IEnumerable<TaskDomain> GetAllTask()
        {
            List<TaskDomain> tasks = null;
            using (ShopDBEntities2 context = new ShopDBEntities2())
            {
                tasks = (from task in context.Task
                             select new TaskDomain
                             {
                                 TaskId = task.TaskId,
                                 TaskName = task.TaskName,
                                 RoleId = task.RoleId,
                                 CustomerId = task.CustomerId,
                                 StatusId = task.StatusId,
                                 OpenDate = task.OpenDate,
                                 ClosedDate = task.ClosedDate
                             }).ToList();
            }
            return tasks;
        }
    }
}
