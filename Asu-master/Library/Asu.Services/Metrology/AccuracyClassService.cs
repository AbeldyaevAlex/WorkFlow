using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial class AccuracyClassService : IAccuracyClassService
    {
        private readonly IRepository<Spr_klass_tochn> _AccuracyClassRepository;
        public AccuracyClassService(IRepository<Spr_klass_tochn> AccuracyClassRepository)
        {
            _AccuracyClassRepository = AccuracyClassRepository;
        }

        public List<Spr_klass_tochn> GetAllAccuracyClass()
        {
            var accuracyClass = _AccuracyClassRepository.Table.ToList();
            return accuracyClass;
        }
    }
}
