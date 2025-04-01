using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.ProductGroups;
using Asu.Data;
using Asu.Data.Mapping.ProductGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Web.Models.Review
{
    public class ChunkedReviewModel
    {
        public int id { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}