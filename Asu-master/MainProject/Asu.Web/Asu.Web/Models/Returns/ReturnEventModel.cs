namespace Asu.Web.Models.Returns
{
    using System;
    using System.Collections.Generic;

    public class ReturnEventModel
    {
        public ReturnEventModel()
        {
            this.Attributes = new List<ReturnEventAttributeModel>();
            this.Items = new List<ReturnItemModel>();
        }

        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public List<ReturnItemModel> Items { get; set; }

        public List<ReturnEventAttributeModel> Attributes { get; set; }

        public ReturnEventType Type { get; set; }
    }
}