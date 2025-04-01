using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public partial class NomenclatureViewModel
    {
        public string NomenclatureObozn { get; set; }
        public string Pkp { get; set; }
        public string Ss { get; set; }
        public string Spo { get; set; }
        public int? Ksb { get; set; }
        public int? Kizd { get; set; }
        public string NameDet { get; set; }
        public string OboznNaim { get; set; }
        public string Tk1 { get; set; }
        public string Tk2 { get; set; }
        public string Tk3 { get; set; }
        public decimal? Mas1sh { get; set; }
        public decimal? Masizd { get; set; }
        public int? Kp1 { get; set; }
        public int? Kp2 { get; set; }
        public int? Kp3 { get; set; }
        public string RascexPoln { get; set; }
        public int? PviId { get; set; }
        public string NaimIzd { get; set; }
        public string PrimKonstructor { get; set; }
        public string PrimTehnolog{ get; set; }
        public string PrimPrinadlegn { get; set; }
        public string PrimIzmenChast { get; set; }
        public string NmRazdIzd { get; set; }
        public string NmGroup { get; set; }
        public string AgregateObozn { get; set; }
        public string GroupAgregate { get; set; }
        public string Komplekt { get; set; }
        public string Status { get; set; }
        public DateTime? PeriodOpenDate { get; set; }
        public DateTime? PeriodCloseDate { get; set; }
        public string Condition { get; set; }
    }
}