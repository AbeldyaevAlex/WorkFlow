using DomainCore;
using EFCore;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;

namespace TestTaskApplication.Controllers
{
    public class TasksApiController : ApiController
    {
        TaskMeneger taskManager;
        ShopDBEntities2 db;
        public TasksApiController()
        {
            taskManager = new TaskMeneger();
            db = new ShopDBEntities2();
        }
        //GET: api/TasksApiController
        public IEnumerable<TaskDomain> GetTask()
        {
            return taskManager.GetAllTask();
        }
        //GET: api/TasksApiController/2
        public IHttpActionResult GetTaskById(int id)
        {
            TaskDomain tasksbyId = null;
            using (ShopDBEntities2 context = new ShopDBEntities2())
            {
                tasksbyId = (from task in context.Task
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
            return Ok(tasksbyId);
        }
        //POST: api/TasksApiController
        public IHttpActionResult PostTask(TaskDomain model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Task tasks = new Task();
            tasks.TaskName = model.TaskName;
            tasks.StatusId = model.StatusId;
            tasks.CustomerId = model.CustomerId;
            tasks.RoleId = model.RoleId;
            tasks.OpenDate = model.OpenDate;
            tasks.ClosedDate = model.ClosedDate;
            db.Task.Add(tasks);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = model.TaskId }, model);
        }
        //PUT: api/TasksApiController
        public IHttpActionResult PutTask(int id, Task model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != model.TaskId)
            {
                return BadRequest();
            }
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
        private bool TaskExests(int id)
        {
            return db.Task.Count(e => e.TaskId == id) > 0;
        }
    }
}
