using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class MarkaMaterialService : IMarkaMaterialService
    {
        private readonly IRepository<MarkMater> _MarkMaterRepository;
        public MarkaMaterialService(IRepository<MarkMater> MarkMaterRepository)
        {
            _MarkMaterRepository = MarkMaterRepository;
        }
        public IQueryable<MarkMater> GetAllMarkMater()
        {
            return _MarkMaterRepository.Table;
        }

        public List<MarkMater> GetAllMarkMaterList()
        {
            return _MarkMaterRepository.Table.ToList();
        }

        public int GetIdFromNameMarkMater(string nameMarkMaterial)
        {
            var nameMarkMaterId = _MarkMaterRepository.Table.Where(x => x.MarkaMater == nameMarkMaterial).Select(c => c.Id).FirstOrDefault();
            return nameMarkMaterId;
        }
    }
}
