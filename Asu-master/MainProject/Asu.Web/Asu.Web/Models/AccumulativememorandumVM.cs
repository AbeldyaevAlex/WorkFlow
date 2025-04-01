using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models
{
    public class AccumulativememorandumVM
    {
        public long Id { get; set; }
        public string tema { get; set; }
        public string zakaz { get; set; }
        public string no_sz { get; set; }
        public string accumulativeno { get; set; }
        public DateTime? date { get; set; }
        public short version { get; set; }
        public string refcshortname { get; set; }
        public string sostavitel { get; set; }
        public string osnovanie { get; set; }
        public string soderjanie { get; set; }
        public string status { get; set; }
        public string status_version { get; set; }
        public string cex_otprav { get; set; }
        public long unxcode { get; set; }
        public string sborka_montaj { get; set; }
        public string divisshortname { get; set; }
        public bool ogmet { get; set; }
        public bool ogt { get; set; }
        public long unitcode { get; set; }
        public int worklineno { get; set; }
        public string level { get; set; }
        public string pkp { get; set; }
        public string oboznach { get; set; }
        public string naim { get; set; }
        public decimal weight { get; set; }
        public string eizm { get; set; }
        public string name { get; set; }
        public int samplequantity { get; set; }
        public int quantity { get; set; }
        public string productdesignationparent { get; set; }
        public string productnameparent { get; set; }
        public string rascehov { get; set; }
        public long kodmat { get; set; }
        public bool isstandard { get; set; }
        public string producttype { get; set; }
        public string tex_usl { get; set; }
        public decimal materialquantity { get; set; }
        public string operationname { get; set; }
        public string cex_izgotov { get; set; }
        public string cex_usl { get; set; }
        public string prodgrname { get; set; }
        public string opergrname { get; set; }
        public string no { get; set; }
        public string materialsize { get; set; }
        public string materialmark { get; set; }
        public string standard { get; set; }
        public string diameter { get; set; }
        public string dimension { get; set; }
        public string nomenclatureno { get; set; }
        public string namemater { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}