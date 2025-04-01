using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Metrology
{
    public partial class Spr_metrol : BaseEntity
    {
        public int? link_podgrupp { get; set; }

        public int? link_naznach { get; set; }

        public int? link_cex { get; set; }

        public int? link_period_poverk { get; set; }

        public int? MestoPoverkId { get; set; }

        public int? link_mesto_k { get; set; }

        public int? link_mesto_rem { get; set; }

        public int? link_rod_poverk { get; set; }

        public int? link_tip_pribora { get; set; }

        public int? link_konserv { get; set; }

        public int? n_pasporta { get; set; }

        public string n_zavod { get; set; }

        public DateTime? data_pover { get; set; }

        public DateTime? data_pred_pov { get; set; }

        public string remont { get; set; }

        public string prim { get; set; }

        public int? link_usl { get; set; }

        public int? link_ree { get; set; }

        public int? link_stan { get; set; }

        public string god_vip { get; set; }

        public int? link_predpr { get; set; }

        public DateTime? data_izm { get; set; }

        public int CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string operation { get; set; }

        public DateTime? operation_date { get; set; }

        public DateTime? period_open_date { get; set; }

        public DateTime? period_close_date { get; set; }

        public int? link_pvi { get; set; }

        public int? link_slugba { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Rod_poverk Rod_poverk { get; set; }

        public virtual Podgr_prib Podgr_prib { get; set; }

        public virtual Nazn_prib Nazn_prib { get; set; }

        public virtual Spr_cex Spr_cex { get; set; }

        public virtual Period_pover Period_pover { get; set; }

        public virtual Spr_cex MestoPoverk { get; set; }
    }
}
