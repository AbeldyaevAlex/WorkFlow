using System;
using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Returns
{
    public class ReturnRequestModel : BaseNopEntityModel
    {
        public ReturnRequestModel()
        {
            this.Events = new List<ReturnEventModel>();
        }

        public string Number { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<ReturnEventModel> Events { get; set; }
    }
}