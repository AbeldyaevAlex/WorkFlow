using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class NaznPribService : INaznPribService
    {
        private readonly IRepository<Nazn_prib> _naznPribRepository;
        public NaznPribService(IRepository<Nazn_prib> naznPribRepository)
        {
            _naznPribRepository = naznPribRepository;
        }

        public IList<Nazn_prib> GetNaznPribList()
        {
            var query = _naznPribRepository.Table;
            return query.ToList();
        }
    }
}
