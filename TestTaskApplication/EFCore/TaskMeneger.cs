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
                         join status in context.StatusTask
                         on task.StatusId equals status.Id
                         join role in context.CustomerRole
                         on task.RoleId equals role.Id
                         join customer in context.Customer
                         on task.CustomerId equals customer.Id
                         select new TaskDomain
                             {
                                 TaskId = task.TaskId,
                                 TaskName = task.TaskName,
                                 Brigade = role.Name,
                                 Status = status.Status,
                                 FullCustomerName = customer.LastName + " " + customer.FirstName + " " + customer.MiddleName,
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
