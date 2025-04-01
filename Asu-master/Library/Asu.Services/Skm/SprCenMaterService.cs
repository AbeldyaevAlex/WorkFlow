using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class SprCenMaterService : ISprCenMaterService
    {
        private readonly IRepository<SprCenMater> _sprCenMaterRepository;
        public SprCenMaterService(IRepository<SprCenMater> sprCenMaterRepository)
        {
            _sprCenMaterRepository = sprCenMaterRepository;
        }
        public IList<SprCenMater> GetAllCenMaterToList()
        {
           return _sprCenMaterRepository.Table.ToList();
        }
    }
}
