using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using Asu.Mapping.Skm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class SprTemService : ISprTemService
    {
        private readonly IRepository<Spr_tem> _SprTemRepository;
        public SprTemService(IRepository<Spr_tem> SprTemRepository)
        {
            _SprTemRepository = SprTemRepository;
        }
        public List<Spr_tem> GetAllListTem()
        {
            var listOfTheme = _SprTemRepository.Table.ToList();
            return listOfTheme;
        }
    }
}
