using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class DerIzdService : IDerIzdService
    {
        private readonly IRepository<Der_izd> _derIzdRepository;
        public DerIzdService(IRepository<Der_izd> derIzdRepository)
        {
            _derIzdRepository = derIzdRepository;   
        }
        public List<Der_izd> GetAllDerIzd()
        {
            var derIzdList = _derIzdRepository.Table.ToList();
            return derIzdList;  
        }
    }
}
