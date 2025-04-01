using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models
{
    public class TemAndPerizdViewModel
    {
        public long Id { get; set; }
        public string nm_izd { get; set; }
        public int TemId { get; set; }
        public int PerIzdId { get; set; }
    }
}