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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace TestTaskApplication.Controllers
{
    public class TasksApiController : ApiController
    {
        TaskMeneger taskManager;
        ShopDBEntities1 db;
        public TasksApiController()
        {
            taskManager = new TaskMeneger();
            db = new ShopDBEntities1();
        }
        //GET: api/TasksApiController
        public IEnumerable<TaskDomain> GetTask()
        {
            return taskManager.GetAllTask();
        }
        //GET: api/TasksApiController/2
        [ResponseType(typeof(EFCore.Model.Task))]
        public async Task<IHttpActionResult> GetAllTaskId(int id)
        {
            EFCore.Model.Task tasks = await db.Task.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            return Ok(tasks);
        }
        //PUT: api/TasksApiController/2
        public async Task<IHttpActionResult> PutTask(int id, EFCore.Model.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != task.TaskId)
            {
                return BadRequest(ModelState);
            }
            db.Entry(task).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExests(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }                   
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        //POST: api/TasksApiController/
        [ResponseType(typeof(EFCore.Model.Task))]
        public async Task<IHttpActionResult> PostTask(EFCore.Model.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Task.Add(task);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }
        private bool TaskExests(int id)
        {
            return db.Task.Count(e => e.TaskId == id) > 0;
        }
    }
}
