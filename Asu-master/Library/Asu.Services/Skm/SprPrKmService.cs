using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class SprPrKmService : ISprPrKmService
    {
        private readonly IRepository<SprPrKm> _sprPrKmRepository;
        public SprPrKmService(IRepository<SprPrKm> sprPrKmRepository)
        {
            _sprPrKmRepository = sprPrKmRepository;
        }

        public IList<SprPrKm> GetAllPrKmToList()
        {
            var query = _sprPrKmRepository.Table.ToList();
            return query;
        }
    }
}
