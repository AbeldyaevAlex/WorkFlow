using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class SprKgrService : ISprKgrService
    {
        private readonly IRepository<SprKgr> _sprKgrRepository;
        public SprKgrService(IRepository<SprKgr> sprKgrRepository)
        {
            _sprKgrRepository = sprKgrRepository;
        }
        public IList<SprKgr> GetAllKgrToList()
        {
            return _sprKgrRepository.Table.ToList();
        }
    }
}
