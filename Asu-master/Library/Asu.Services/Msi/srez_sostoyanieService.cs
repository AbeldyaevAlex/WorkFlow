using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class srez_sostoyanieService : Isrez_sostoyanieService
    {
        private readonly IRepository<srez_sostoyanie> _srezSostoyanieRepository;
        public srez_sostoyanieService(IRepository<srez_sostoyanie> srezSostoyanieRepository)
        {
            _srezSostoyanieRepository = srezSostoyanieRepository;
        }
        public List<srez_sostoyanie> GetAllListParameterInvent()
        {
            var listInventParametr = _srezSostoyanieRepository.Table.ToList();
            return listInventParametr;
        }
    }
}
