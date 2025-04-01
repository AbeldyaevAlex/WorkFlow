using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class SprPerizdService : ISprPerizdService
    {
        private readonly IRepository<Spr_Perizd> _Spr_PerizdRepository;
        public SprPerizdService(IRepository<Spr_Perizd> Spr_PerizdRepository)
        {
            _Spr_PerizdRepository = Spr_PerizdRepository;
        }
        List<Spr_Perizd> ISprPerizdService.GetAllistIzd()
        {
            var listPerIzd = _Spr_PerizdRepository.Table.ToList();
            return listPerIzd;
        }
    }
}
