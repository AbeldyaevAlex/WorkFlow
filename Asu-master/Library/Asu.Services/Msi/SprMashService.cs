using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class SprMashService : ISprMashService
    {
        private readonly IRepository<Spr_mash> _sprMashRepository;
        public SprMashService(IRepository<Spr_mash> sprMashRepository)
        {
            _sprMashRepository = sprMashRepository; 
        }
        public List<Spr_mash> GetAllListMash()
        {
            var mashList = _sprMashRepository.Table.ToList();
            return mashList;    
        }
    }
}
