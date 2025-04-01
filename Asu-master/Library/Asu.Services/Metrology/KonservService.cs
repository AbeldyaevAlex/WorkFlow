using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class KonservService : IKonservService
    {
        private readonly IRepository<Konserv> _KonservRepository;
        public KonservService(IRepository<Konserv> KonservRepository)
        {
            _KonservRepository = KonservRepository;
        }
        public IList<Konserv> GetKonservList()
        {
            var query = _KonservRepository.Table;
            return query.ToList();
        }
    }
}
