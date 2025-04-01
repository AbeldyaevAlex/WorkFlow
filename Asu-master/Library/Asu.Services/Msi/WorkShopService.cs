using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class WorkShopService : IWorkShopService
    {
        private readonly IRepository<Spr_cex> _sprCexRepository;
        public WorkShopService(IRepository<Spr_cex> sprCexRepository)
        {
            _sprCexRepository = sprCexRepository;
        }
        public IList<Spr_cex> GetWorkShopList()
        {
            var query = _sprCexRepository.Table;
            return query.ToList();
        }
    }
}
