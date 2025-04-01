using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.DocumentStatusService
{
    public partial interface IDocumentStatus
    {
        IQueryable<DocumentStatus> GetAllStatus();

        IList<DocumentStatus> GetAllStatusList();

        int GetStatusForInsertKm();
    }
}
