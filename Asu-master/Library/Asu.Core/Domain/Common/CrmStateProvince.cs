using Asu.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Common
{
    public class CrmState : BaseEntity, ILocalizedEntity
    {
        public string Name { get; set; }

        public string OfficialTitle { get; set; }

        public string Code { get; set; }

        public bool IsContiguousArea { get; set; }

        public int CountryId { get; set; }

        public virtual CrmCountry Country { get; set; }
    }
}
