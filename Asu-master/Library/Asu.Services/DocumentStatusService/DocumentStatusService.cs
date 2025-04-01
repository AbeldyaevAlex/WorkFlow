using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.DocumentStatusService
{
    public partial class DocumentStatusService : IDocumentStatus
    {

        private readonly IRepository<DocumentStatus> _DocumentStatusRepository;
        private readonly IWorkContext _workContext;
        public DocumentStatusService(IRepository<DocumentStatus> DocumentStatusRepositor, IWorkContext workContext)
        {
            _DocumentStatusRepository = DocumentStatusRepositor;
            _workContext = workContext;
        }


        public IQueryable<DocumentStatus> GetAllStatus()
        {
            return _DocumentStatusRepository.Table;
        }

        public IList<DocumentStatus> GetAllStatusList()
        {
            var query = _DocumentStatusRepository.Table;
            return query.ToList();
        }

        public int GetStatusForInsertKm()
        {
            throw new NotImplementedException();
        }
    }
}
