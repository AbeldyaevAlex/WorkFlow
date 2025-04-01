using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class RodPoverkService : IRodPoverkService
    {
        private readonly IRepository<Rod_poverk> _RodPoverkRepository;
        public RodPoverkService(IRepository<Rod_poverk> RodPoverkRepository)
        {
            _RodPoverkRepository = RodPoverkRepository;
        }
        public List<Rod_poverk> GetRodPoverk()
        {
            var listRodPoverk = _RodPoverkRepository.Table.ToList();
            return listRodPoverk;
        }

        public List<int> GetRodPoverkId(string rodPoverk)
        {
            var RodPoverkId = _RodPoverkRepository.Table.Where(x => x.naim_rod.Contains(rodPoverk.ToLower()) || x.naim_rod == rodPoverk).Select(f => f.Id).ToList();
            return RodPoverkId;
        }

        public IList<Rod_poverk> GetRodPoverkList()
        {
            var query = _RodPoverkRepository.Table;
            return query.ToList();
        }
    }
}
