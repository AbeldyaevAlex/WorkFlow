using Asu.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class PkpViewModel
    {
        public int Id { get; set; }

        public string Pkp { get; set; }

        public string NmPkp { get; set; }

        public int RazdId { get; set; }

        public int RazdDse { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public string ImageFileName { get; set; }

        public string PkpDos { get; set; }

        public int? PviId { get; set; }


        //[Required]
        //[StringLength(7, ErrorMessage = "{0} может содержать не более {1} символов.")]
        //[Display(Name = "ПКП")]
        //public string pkp { get; set; }
        //[Required]
        //[Display(Name = "Наименование ПКП")]
        //public string nm_pkp { get; set; }
        //[Display(Name = "Имя файла")]
        //public string ImageFileName { get; set; }
        //[Display(Name = "Путь файла")]
        //public string ImageUrl
        //{
        //    get { return string.Format("Content/PkpImage/{0}", ImageFileName); }
        //}
        //public long? link_razd { get; set; }
        //public long? link_razd_dse { get; set; }
        //public int? link_status { get; set; }
        //public int? link_user { get; set; }
        //public string operation { get; set; }
        //public DateTime? operation_date { get; set; }
        //public DateTime? period_open_date { get; set; }
        //public DateTime? period_close_date { get; set; }
    }
}