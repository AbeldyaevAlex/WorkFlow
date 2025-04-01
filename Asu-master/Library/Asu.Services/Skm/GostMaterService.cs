using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class GostMaterService : IGostMaterService
    {
        private readonly IRepository<GostMater> _GostMaterRepository;

        public GostMaterService(IRepository<GostMater> GostMaterRepository)
        {
            _GostMaterRepository = GostMaterRepository;
        }
        public IQueryable<GostMater> GetAllGostMater()
        {
            return _GostMaterRepository.Table;
        }

        public List<GostMater> GetAllGostMaterList()
        {
            return _GostMaterRepository.Table.ToList();
        }

        public int GetIdFromGost(string GostMaterial)
        {
            var GostMaterId = _GostMaterRepository.Table.Where(x => x.Gost == GostMaterial).Select(c => c.Id).FirstOrDefault();
            return GostMaterId;
        }
    }
}
