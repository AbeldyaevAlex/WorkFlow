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
using System.Web.Mvc;

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
                             join status in context.StatusTask
                             on task.StatusId equals status.Id
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
            tasks.CustomerId = 2;
            tasks.RoleId = int.Parse(model.Brigade);
            tasks.OpenDate = DateTime.Now;
            tasks.ClosedDate = model.ClosedDate;
            db.Task.Add(tasks);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = model.TaskId }, model);
        }
        //PUT: api/TasksApiController
        public IHttpActionResult PutTask(int id, TaskDomain model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Task tasks = new Task();
            tasks.TaskId = id;
            tasks.TaskName = model.TaskName;
            tasks.StatusId = db.StatusTask.Where(x => x.Status == model.Status).Select(c => c.Id).FirstOrDefault();
            tasks.CustomerId = 2;
            tasks.RoleId = db.CustomerRole.Where(x => x.Name == model.Brigade).Select(c => c.Id).FirstOrDefault();
            tasks.OpenDate = model.OpenDate;

            if (model.Status == "Closed")
            {
                tasks.ClosedDate = DateTime.Now;
            }
            if (model.Status == "InProgress" || model.Status == "Open")
            {
                tasks.ClosedDate = null;
            }
            
            db.Entry(tasks).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
        private bool TaskExests(int id)
        {
            return db.Task.Count(e => e.TaskId == id) > 0;
        }
    }
}
