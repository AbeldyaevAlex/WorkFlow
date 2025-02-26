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
        ShopDBEntities1 db = new ShopDBEntities1();
        public IEnumerable<TaskDomain> GetAllTask()
        {
            List<TaskDomain> tasks = null;
            using (ShopDBEntities1 context = new ShopDBEntities1())
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
        public async Task<IHttpActionResult> GetAllEmployeeById(int id)
        {
            Model.Task tasks = await db.Task.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            return Ok(tasks);
        }
    }
}
