using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface IUnitOfMeasurementService
    {
        List<SprEizm> GetAllUnitOfMeasurementList();
    }
}
