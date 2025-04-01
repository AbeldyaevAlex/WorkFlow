using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class PeriodPoverkService : IPeriodPoverkService
    {
        private readonly IRepository<Period_pover> _periodPoverRepository;
        public PeriodPoverkService(IRepository<Period_pover> periodPoverRepository)
        {
            _periodPoverRepository = periodPoverRepository;
        }
        public IList<Period_pover> GetPeriodPoverList()
        {
            var query = _periodPoverRepository.Table;
            return query.ToList();
        }
    }
}
