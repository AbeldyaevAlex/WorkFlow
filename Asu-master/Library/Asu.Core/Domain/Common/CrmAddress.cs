using Asu.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Common
{
    public class CrmCountry : BaseEntity, ILocalizedEntity
    {
        public string Name { get; set; }

        public string A2 { get; set; }

        public string A3 { get; set; }

        public string Number { get; set; }
    }
}
