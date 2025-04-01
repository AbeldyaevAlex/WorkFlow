using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public partial class WorkshopPortfolioViewModel
    {
        public int DirectiveWorkId { get; set; }
        public string PotrtfolioObozn { get; set; }

        public string Pkp { get; set; }

        public string Ss { get; set; }

        public string Spo { get; set; }

        public int? Kizd { get; set; }

        public string Name { get; set; }

        public decimal? Mas1sh { get; set; }

        public decimal? MasIzd { get; set; }

        public int? Kp1 { get; set; }

        public int? Kp2 { get; set; }

        public int? Kp3 { get; set; }

        public string RascexPoln { get; set; }

        public string NameIzdel { get; set; }

        public string NameRazdIzd { get; set; }

        public string NameGroup { get; set; }

        public string Komplekt { get; set; }

        public string Status { get; set; }

        public string Condition { get; set; }

        public string Workshop { get; set; }

        [UIHint("EditorExeptionOfWork")]
        public string ExeptionOfWork { get; set; }

        [UIHint("EditorDirectiveWorkSdelnIzgOnUnit")]
        public decimal? DirectiveWorkSdelnIzgOnUnit { get; set; }

        public decimal? DirectiveWorkSdelnIzgOnProduct { get; set; }

        [UIHint("EditorDirectiveWorkSdelnUslOnUnit")]
        public decimal? DirectiveWorkSdelnUslOnUnit { get; set; }

        public decimal? DirectiveWorkSdelnUslOnProduct { get; set; }


        [UIHint("EditorDirectiveWorkPovrIzgOnUnit")]
        public decimal? DirectiveWorkPovrIzgOnUnit { get; set; }

        public decimal? DirectiveWorkPovrIzgOnProduct { get; set; }

        [UIHint("EditorDirectiveWorkPovrUslOnUnit")]
        public decimal? DirectiveWorkPovrUslOnUnit { get; set; }

        public decimal? DirectiveWorkPovrUslOnProduct { get; set; }
    }
}