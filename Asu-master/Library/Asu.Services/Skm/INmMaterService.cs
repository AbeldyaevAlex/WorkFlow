using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface INmMaterService
    {
        IQueryable<DirectoryOfMaterialName> GetAllNameMater();
        List<DirectoryOfMaterialName> GetAllNameMaterList();
        int GetIdFromNameMater(string nameMaterial);
    }
}
