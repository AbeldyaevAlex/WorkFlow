using DomainCore;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class TaskMeneger
    {
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
        public TaskDomain GetAllEmployeeById(int id)
        {
            TaskDomain tasks = null;
            using (ShopDBEntities1 context = new ShopDBEntities1())
            {
                tasks = (from task in context.Task
                         where task.TaskId == id
                         select new TaskDomain
                         {
                             TaskId = task.TaskId,
                             TaskName = task.TaskName,
                             RoleId = task.RoleId,
                             CustomerId = task.CustomerId,
                             StatusId = task.StatusId,
                             OpenDate = task.OpenDate,
                             ClosedDate = task.ClosedDate
                         }).FirstOrDefault();
            }
            return tasks;
        }
    }
}
