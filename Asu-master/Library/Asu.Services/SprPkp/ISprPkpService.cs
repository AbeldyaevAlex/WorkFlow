using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.SprPkp
{
    public partial interface ISprPkpService
    {
        /// <summary>
        /// Gets all Pkp
        /// </summary>
        /// <returns>Pkp</returns>
        IList<Spr_pkp> GetAllPkp();

        /// <summary>
        /// Gets all Pkp
        /// </summary>
        /// <returns>Pkp</returns>
        IList<Spr_pkp> GetAllPkpId();


        void UpdatePkp(Spr_pkp item);

        void DeletePkp(Spr_pkp item);

        Spr_pkp GetPkpById(int pkpId);
    }
}
