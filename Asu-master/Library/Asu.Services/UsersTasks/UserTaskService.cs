using Asu.Core.Data;
using Asu.Core.Domain.Logging;
using System.Linq;
using System.Runtime.Remoting.Contexts;


namespace Asu.Services.UsersTasks
{
    public partial class UserTaskService : IUserTaskService
    {

        private readonly IRepository<Core.Domain.Tasks.UsersTasks> _UsersTasksRepository;
        private readonly IRepository<Log> _LogRepository;

        public UserTaskService(IRepository<Core.Domain.Tasks.UsersTasks> UsersTasksRepository, IRepository<Log> LogRepository)
        {
            _UsersTasksRepository = UsersTasksRepository;
            _LogRepository = LogRepository;
        }

        public IQueryable<Core.Domain.Tasks.UsersTasks> GetAllUser()
        {
            var query = _UsersTasksRepository.Table;
            return query;
        }

        public int GetSubTaskId(string naimTask)
        {
            var TaskId = _UsersTasksRepository.Table.Where(x => x.NaimTask == naimTask).Select(k => k.Id).FirstOrDefault();
            return TaskId;
        }
    }
}

