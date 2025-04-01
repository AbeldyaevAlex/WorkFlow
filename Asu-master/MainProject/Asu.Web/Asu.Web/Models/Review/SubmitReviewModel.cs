using FluentValidation.Attributes;
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
using Asu.Web.Validators.Review;

namespace Asu.Web.Models.Review
{
    [Validator(typeof(SubmitReviewValidator))]
    public class SubmitReviewModel
    {
        public int? BaseVehicleId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CustomerId { get; set; }

        public int GroupId { get; set; }

        public int? ProductId { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }
    }
}