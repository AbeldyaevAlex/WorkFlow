using System.Collections.Generic;
using System.Linq;

namespace Asu.Services.UsersTasks
{
    public partial interface IUserTaskService
    {
        IQueryable<Core.Domain.Tasks.UsersTasks> GetAllUser();

        int GetSubTaskId(string naimTask);
    }
}