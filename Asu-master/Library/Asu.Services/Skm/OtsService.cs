using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class OtsService : IOtsService
    {
        private readonly IRepository<SprOts> _otsRepository;
        public OtsService(IRepository<SprOts> otsRepository)
        {
            _otsRepository = otsRepository;
        }

        public IList<SprOts> GetAllNaimOts()
        {
            return _otsRepository.Table.ToList();
        }
    }
}
