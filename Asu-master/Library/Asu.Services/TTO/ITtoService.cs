using Asu.Core.Domain.TypicalTechnologicalOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.TTO
{
    public partial interface ITtoService
    {
        IQueryable<Spr_tto> GetAllTto();
        IList<Spr_tto> GetAllTtoToList();
        IEnumerable<FullSkmInfo> GetFullSkmInfo();
        IList<Group_TTO> GetUniQTTO();
        IQueryable<Spr_tto> Get_TTO(object masterRowKey);
    }
}
