using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Web.Controllers
{
    interface IUpdate_Directory
    {
        Spr_cex Partial_Update_Spr_Cex(Spr_cex _naim_directory, string User_Id);
        Models.DirectoryOfMaterialCodifiers.Spr_skm Partial_Update_Spr_Skm(Models.DirectoryOfMaterialCodifiers.Spr_skm _naim_directory, string User_Id);
        Spr_agr Partial_Update_Spr_Agr(Spr_agr _naim_directory, string User_Id);
        Spr_eizm Partial_Update_Spr_Eizm(Spr_eizm _naim_directory, string User_Id);
        Spr_GR_Mater Partial_Update_Spr_GR_Mater(Spr_GR_Mater _naim_directory, string User_Id);
        Spr_kdan Partial_Update_Spr_kdan(Spr_kdan _naim_directory, string User_Id);
        Spr_kgr Partial_Update_Spr_kgr(Spr_kgr _naim_directory, string User_Id);
        Spr_kompl Partial_Update_Spr_kompl(Spr_kompl _naim_directory, string User_Id);
        Spr_mash Partial_Update_Spr_mash(Spr_mash _naim_directory, string User_Id);
        Spr_mash_sg Partial_Update_Spr_mash_sg(Spr_mash_sg _naim_directory, string User_Id);
        Spr_mater Partial_Update_Spr_mater(Spr_mater _naim_directory, string User_Id);
        Spr_METODIC Partial_Update_SPR_METODIC(Spr_METODIC _naim_directory, string User_Id);
        Spr_nmdet Partial_Update_Spr_nmdet(Spr_nmdet _naim_directory, string User_Id);
        Spr_nm_task Partial_Update_Spr_nm_task(Spr_nm_task _naim_directory, string User_Id);
        Spr_normali Partial_Update_Spr_normali(Spr_normali _naim_directory, string User_Id);
        Spr_obozn Partial_Update_Spr_obozn(Spr_obozn _naim_directory, string User_Id);
        Spr_obraz Partial_Update_Spr_obraz(Spr_obraz _naim_directory, string User_Id);
        SPR_OGT Partial_Update_SPR_OGT(SPR_OGT _naim_directory, string User_Id);
        SPR_OTS Partial_Update_SPR_OGT(SPR_OTS _naim_directory, string User_Id);
        Spr_Perizd Partial_Update_SPR_OGT(Spr_Perizd _naim_directory, string User_Id);
        Spr_per_dok_ob Partial_Update_Spr_per_dok_ob(Spr_per_dok_ob _naim_directory, string User_Id);
        Spr_PKP Partial_Update_Spr_PKP(Spr_PKP _naim_directory, string User_Id);
        Spr_prim_dse Partial_Update_Spr_prim_dse(Spr_prim_dse _naim_directory, string User_Id);
        SPR_PRKM Partial_Update_SPR_PRKM(SPR_PRKM _naim_directory, string User_Id);
        Spr_prpokr Partial_Update_Spr_prpokr(Spr_prpokr _naim_directory, string User_Id);
        Spr_pvi Partial_Update_Spr_pvi(Spr_pvi _naim_directory, string User_Id);
        Spr_rascex Partial_Update_Spr_rascex(Spr_rascex _naim_directory, string User_Id);
        Spr_rasc_vert Partial_Update_Spr_rasc_vert(Spr_rasc_vert _naim_directory, string User_Id);
        Spr_Razd_DSE Partial_Update_Spr_Razd_DSE(Spr_Razd_DSE _naim_directory, string User_Id);
        Spr_Razd_Izd Partial_Update_Spr_Razd_Izd(Spr_Razd_Izd _naim_directory, string User_Id);
        Spr_razd_izm Partial_Update_Spr_razd_izm(Spr_razd_izm _naim_directory, string User_Id);
        Spr_specif Partial_Update_Spr_specif(Spr_specif _naim_directory, string User_Id);
        Spr_Tehnizg Partial_Update_Spr_Tehnizg(Spr_Tehnizg _naim_directory, string User_Id);
        Spr_tem Partial_Update_Spr_tem(Spr_tem _naim_directory, string User_Id);
        Models.TypicalTechnologicalOperations.Spr_tto Partial_Update_Spr_tto(Models.TypicalTechnologicalOperations.Spr_tto _naim_directory, string User_Id);
        User Partial_Update_User(User _naim_directory, string User_Id);
        Spr_uch_cex Partial_Update_Spr_uch_cex(Spr_uch_cex _naim_directory, string User_Id);
        Spr_Zakaz Partial_Update_Spr_Zakaz(Spr_Zakaz _naim_directory, string User_Id);      
        Spr_balsch Partial_Update_Spr_balsch(Spr_balsch _naim_directory, string User_Id);
        Spr_cen_mater Partial_Update_Spr_cen_mater(Spr_cen_mater _naim_directory, string User_Id);      
        Spr_cex_prizn Partial_Update_Spr_cex_prizn(Spr_cex_prizn _naim_directory, string User_Id);
        Spr_dse Partial_Update_Spr_dse(Spr_dse _naim_directory, string User_Id);
    }
}
