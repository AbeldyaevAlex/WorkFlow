using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface IGrMaterService
    {
        int GetGrMaterIdFromOgt(int ogt);
        int GetGrMaterIdFromNaimOgt(string NaimOgt);
        int GetGrMaterIdFromNoAndNmGrMater(string NoGrMater, string NmGrMater);
        IQueryable<SprGrMater> GetAllGrMater();
        List<SprGrMater> GetAllGrMaterList();
    }
}
