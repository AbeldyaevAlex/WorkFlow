using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface IOgtService
    {
        int GetIdFromOgt(int ogt);
        int GetIdFromNaimOgt(string NaimOgt);
        string GetNaimOgtFromId(int? IdOgt);
        int? GetOgtFromNaimOgt(string NaimOgt);
        IList<SprOgt> GetAllOgt();
        IList<ExtendedNaimOgt> GetAllNaimOgt();
        void InsertOgt(SprOgt ogt);
    }
}
