using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial class UnitOfMeasurementService : IUnitOfMeasurementService
    {
        private readonly IRepository<SprEizm> _SprEizmRepository;
        public UnitOfMeasurementService(IRepository<SprEizm> sprEizmRepository)
        {
            _SprEizmRepository = sprEizmRepository;
        }

        public List<SprEizm> GetAllUnitOfMeasurementList()
        {
            return _SprEizmRepository.Table.ToList();
        }
    }
}
