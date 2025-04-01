using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface IDirectoryOfMaterialCodifiersService
    {
        IQueryable<SprSkm> GetAllKm(SprSkm param);

        void InsertKm(SprSkm km);

        IList<SprPrKm> GetAllPrKms();
    }
}
