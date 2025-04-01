using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asu.Web.Models.ContextDb;

namespace Asu.Web.Models
{
    public class Tema
    {
        public string SelectedTem { get; set; }
        public List<Msi.Spr_tem> Spr_tem
        {
            get
            {
                AsuAviaDbContext dbcont = new AsuAviaDbContext();
                return dbcont.Spr_tem.Where(x => x.Id != 1).ToList();
            }
        }
    }
    public class TestViewModel
    {
        public IEnumerable<Spr_obozn> Spr_Obozns{ get; set; }
        public IEnumerable<Spr_mater> Spr_Maters { get; set; }
    }
}