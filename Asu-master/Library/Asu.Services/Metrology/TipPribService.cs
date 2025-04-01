using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class TipPribService : ITipPribService
    {
        private readonly IRepository<Tip_pribora> _tipPriboraRepository;
        public TipPribService(IRepository<Tip_pribora> tip_priboraRepository)
        {
            _tipPriboraRepository = tip_priboraRepository;
        }
        public IList<Tip_pribora> GetTipPribList()
        {
            var query = _tipPriboraRepository.Table;
            return query.ToList();
        }
    }
}
