using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class GrMaterService : IGrMaterService
    {
        private readonly IRepository<SprGrMater> _SprGrMaterRepository;
        public GrMaterService(IRepository<SprGrMater> sprGrMaterRepository)
        {
            _SprGrMaterRepository = sprGrMaterRepository;
        }

        public IQueryable<SprGrMater> GetAllGrMater()
        {
            return _SprGrMaterRepository.Table;
        }

        public List<SprGrMater> GetAllGrMaterList()
        {
            return _SprGrMaterRepository.Table.ToList();
        }

        public int GetGrMaterIdFromNaimOgt(string NaimOgt)
        {
            throw new NotImplementedException();
        }

        public int GetGrMaterIdFromNoAndNmGrMater(string NoGrMater, string NmGrMater)
        {
            var GroupId = 0;
            var i = int.TryParse(NoGrMater, out int NomerGroup);
            if (i)
            {
                GroupId = _SprGrMaterRepository.Table.Where(x => x.NmGrMater == NmGrMater && x.NomerGrMater == NomerGroup).Select(c => c.Id).FirstOrDefault();
            }
            return GroupId;
        }

        public int GetGrMaterIdFromOgt(int ogt)
        {
            throw new NotImplementedException();
        }
    }
}
