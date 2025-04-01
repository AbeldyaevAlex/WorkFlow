using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfTaskNamesViewModel
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Naim_task { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? Id_Roditel { get; set; }
        public byte[] screen { get; set; }
        public string naim_screen { get; set; }
        public string AlternativeText { get; set; }
        public string Controller_Name { get; set; }
        public string Action_Name { get; set; }
        public bool? IsGroup { get; set; }
        public string RouteUrl { get; set; }
        public string Title { get; set; }
    }
}