using DomainCore;
using EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TestTaskApplication.Controllers
{
    public class TasksApiController : ApiController
    {
        TaskMeneger taskManager;
        public TasksApiController()
        {
            taskManager = new TaskMeneger();
        }

        public IEnumerable<TaskDomain> Get()
        {
            return taskManager.GetAllTask();
        }
    }
}
