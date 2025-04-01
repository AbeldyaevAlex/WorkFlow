using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class SkmViewModel
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Km { get; set; }
        public int Ksim { get; set; }
        public string Dbt { get; set; }
        public string Dsh { get; set; }

        //public decimal? ves { get; set; }
        //[Required(ErrorMessage = "Обязательное поле")]
        //public long? link_nm_skm { get; set; }
        //public long? link_marka { get; set; }
        //public long? link_gost { get; set; }
        //[Required(ErrorMessage = "Обязательное поле")]
        //public long? link_eizm { get; set; }
        //public long? link_kgr { get; set; }
        //public long? link_ots { get; set; }
        //[Required(ErrorMessage = "Обязательное поле")]
        //public long? link_ogt { get; set; }
        //public long? link_balsch { get; set; }
        //public long? link_prkm { get; set; }
        //public int? link_status { get; set; }
        //public string link_user { get; set; }
        //public string operation { get; set; }
        //public DateTime? operation_date { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime? period_open_date { get; set; }
        //public DateTime? period_close_date { get; set; }
        //public int? link_GR_Mater { get; set; }
        //public string nomenkl_nomer { get; set; }
        //public int? sort_OGT { get; set; }
        //public int? link_pvi { get; set; }
    }
}