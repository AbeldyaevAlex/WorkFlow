using Asu.Web.Controllers;
using Asu.Web.Models;
using System;
using System.Linq;
using System.Data;
using Asu.Web.Models.ContextDb;
using System.Configuration;

namespace Asu.Web.Partial_Update_Delete
{
    public class Partial_Update : IUpdate_Directory
    {
        AsuAviaDbContext db = new AsuAviaDbContext();
        
        public Spr_agr Partial_Update_Spr_Agr(Spr_agr _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_balsch Partial_Update_Spr_balsch(Spr_balsch _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_cen_mater Partial_Update_Spr_cen_mater(Spr_cen_mater _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_cex Partial_Update_Spr_Cex(Spr_cex _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_cex_prizn Partial_Update_Spr_cex_prizn(Spr_cex_prizn _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_dse Partial_Update_Spr_dse(Spr_dse _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_eizm Partial_Update_Spr_Eizm(Spr_eizm _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_GR_Mater Partial_Update_Spr_GR_Mater(Spr_GR_Mater _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_kdan Partial_Update_Spr_kdan(Spr_kdan _naim_directory, string User_Id)
        {
            throw new NotImplementedException();
        }

        public Spr_kgr Partial_Update_Spr_kgr(Spr_kgr _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_kompl Partial_Update_Spr_kompl(Spr_kompl _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_mash Partial_Update_Spr_mash(Spr_mash _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_mash_sg Partial_Update_Spr_mash_sg(Spr_mash_sg _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_mater Partial_Update_Spr_mater(Spr_mater _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_METODIC Partial_Update_SPR_METODIC(Spr_METODIC _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_nmdet Partial_Update_Spr_nmdet(Spr_nmdet _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_nm_task Partial_Update_Spr_nm_task(Spr_nm_task _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_normali Partial_Update_Spr_normali(Spr_normali _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_obozn Partial_Update_Spr_obozn(Spr_obozn _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_obraz Partial_Update_Spr_obraz(Spr_obraz _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_Perizd Partial_Update_SPR_OGT(Spr_Perizd _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public SPR_OTS Partial_Update_SPR_OGT(SPR_OTS _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public SPR_OGT Partial_Update_SPR_OGT(SPR_OGT _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_per_dok_ob Partial_Update_Spr_per_dok_ob(Spr_per_dok_ob _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_PKP Partial_Update_Spr_PKP(Spr_PKP _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_prim_dse Partial_Update_Spr_prim_dse(Spr_prim_dse _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public SPR_PRKM Partial_Update_SPR_PRKM(SPR_PRKM _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_prpokr Partial_Update_Spr_prpokr(Spr_prpokr _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_pvi Partial_Update_Spr_pvi(Spr_pvi _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_rascex Partial_Update_Spr_rascex(Spr_rascex _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_rasc_vert Partial_Update_Spr_rasc_vert(Spr_rasc_vert _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_Razd_DSE Partial_Update_Spr_Razd_DSE(Spr_Razd_DSE _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_Razd_Izd Partial_Update_Spr_Razd_Izd(Spr_Razd_Izd _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_razd_izm Partial_Update_Spr_razd_izm(Spr_razd_izm _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Models.DirectoryOfMaterialCodifiers.Spr_skm Partial_Update_Spr_Skm(Models.DirectoryOfMaterialCodifiers.Spr_skm _naim_directory, string User_Id)
        {
            _naim_directory.link_user = User_Id;
            _naim_directory.link_pvi = db.Spr_pvi.Where(x => x.pvi == "И").Select(y => y.Id).FirstOrDefault();
            _naim_directory.operation_date = DateTime.Now;
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).FirstOrDefault();




            return _naim_directory;
        }

        public Spr_specif Partial_Update_Spr_specif(Spr_specif _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_Tehnizg Partial_Update_Spr_Tehnizg(Spr_Tehnizg _naim_directory, string User_Id)
        {
            throw new NotImplementedException();
        }

        public Spr_tem Partial_Update_Spr_tem(Spr_tem _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Models.TypicalTechnologicalOperations.Spr_tto Partial_Update_Spr_tto(Models.TypicalTechnologicalOperations.Spr_tto _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_user = User_Id;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_uch_cex Partial_Update_Spr_uch_cex(Spr_uch_cex _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public Spr_Zakaz Partial_Update_Spr_Zakaz(Spr_Zakaz _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }
        public Spr_obozn Partial_Update_Spr_Obozn(Spr_obozn _naim_directory, string User_Id)
        {
            _naim_directory.period_close_date = DateTime.Now;
            _naim_directory.link_status = db.DocumentStatus.Where(x => x.status == "Аннулирован").Select(y => y.Id).ToList()[0];
            _naim_directory.operation = "update";
            return _naim_directory;
        }

        public User Partial_Update_User(User _naim_directory, string User_Id)
        {
            throw new NotImplementedException();
        }
    }
}