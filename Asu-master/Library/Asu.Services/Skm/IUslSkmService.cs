using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.TypicalTechnologicalOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface IUslSkmService
    {
        IQueryable<UslSkm> GetAllUsl();
    }
}
