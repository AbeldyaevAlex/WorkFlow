using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
//using DocumentFormat.OpenXml;
using Asu.Web.Models;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;

namespace Asu.Web.Areas.Admin.Controllers
{
    public class ImportFromTeamCenterController : Controller
    {
        private int count_row;
        ASU_AVIAEntities12 db = new ASU_AVIAEntities12();
        //INTEGRATION integration = new INTEGRATION();
        //Temp_Spr_Eizm temp_spr_eizm = new Temp_Spr_Eizm();
        public ActionResult ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase myExcelData, string sprav)
        {
            var userId = (Int32)Session["UserId"];
            if (myExcelData.ContentLength == 0 || myExcelData == null)
            {
                ViewBag.Error = "Выберите файл EXCEL";
            }
            else
            {
                var sw = new Stopwatch();
                sw.Start();
                if (myExcelData.FileName.EndsWith("xls") || myExcelData.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Upload_Excel/" + myExcelData.FileName);
                    //if (System.IO.File.Exists(path))
                    //    System.IO.File.Delete(path);
                    //myExcelData.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    app.Visible = true;
                    app.ShowSelectionFloaties = true;

                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    workbook.InactiveListBorderVisible = true;
                    workbook.ShowPivotChartActiveFields = true;

                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    switch (sprav)
                    {
                        case "Spr_Eizm":
                            //var temp_spr_eizm = integration.Temp_Spr_eizm;
                            var temp_spr_eizm = db.Spr_eizm;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Spr_eizm eizm = new Spr_eizm();
                                //Temp_Spr_eizm eizm = new Temp_Spr_eizm();
                                eizm.krat_naim_eizm = ((Excel.Range)range.Cells[row, 1]).Text;
                                eizm.poln_naim_eizm = ((Excel.Range)range.Cells[row, 2]).Text;
                                eizm.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                eizm.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                eizm.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                eizm.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                eizm.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                temp_spr_eizm.Add(eizm);
                                db.SaveChanges();
                            }
                            break;
                        case "Predpr_Postav":
                            var model_Predpr_Postav = db.Predpr_Postav;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Predpr_Postav postav = new Predpr_Postav();
                                postav.Predpr = ((Excel.Range)range.Cells[row, 1]).Text;
                                postav.Address = ((Excel.Range)range.Cells[row, 2]).Text;
                                postav.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                postav.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                postav.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                postav.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                postav.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Predpr_Postav.Add(postav);
                                db.SaveChanges();
                            }
                            break;
                        case "Mark_Mater":
                            var model_Mark_mater = db.Mark_mater;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Mark_mater mark_mater = new Mark_mater();
                                mark_mater.marka_mater = ((Excel.Range)range.Cells[row, 1]).Text;
                                mark_mater.link_status = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                mark_mater.link_user = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                mark_mater.operation = ((Excel.Range)range.Cells[row, 4]).Text;
                                mark_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                mark_mater.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                model_Mark_mater.Add(mark_mater);
                                db.SaveChanges();
                            }
                            break;
                        case "Nm_mater":
                            var model_Nm_mater = db.Nm_mater;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Nm_mater nm_mater = new Nm_mater();
                                nm_mater.nm_mater1 = ((Excel.Range)range.Cells[row, 1]).Text;
                                nm_mater.link_status = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                nm_mater.link_user = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                nm_mater.operation = ((Excel.Range)range.Cells[row, 4]).Text;
                                nm_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                nm_mater.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                model_Nm_mater.Add(nm_mater);
                                db.SaveChanges();
                            }
                            break;
                        case "Dokum_Obosnov":
                            var model_Dokum_Obosnov = db.Dokum_Obosnov;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Dokum_Obosnov docum = new Dokum_Obosnov();
                                docum.Obosnov = ((Excel.Range)range.Cells[row, 1]).Text;
                                docum.link_status = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                docum.link_user = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                docum.operation = ((Excel.Range)range.Cells[row, 4]).Text;
                                docum.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                docum.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                model_Dokum_Obosnov.Add(docum);
                                db.SaveChanges();
                            }
                            break;
                        case "GOST_mater":
                            var model_GOST_mater = db.GOST_mater;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                GOST_mater gost_mater = new GOST_mater();
                                gost_mater.gost = ((Excel.Range)range.Cells[row, 1]).Text;
                                gost_mater.link_status = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                gost_mater.link_user = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                gost_mater.operation = ((Excel.Range)range.Cells[row, 4]).Text;
                                gost_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                gost_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                model_GOST_mater.Add(gost_mater);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_skm":
                            var model_Spr_skm = db.Spr_skm;
                            Spr_skm skm = new Spr_skm();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                                
                                skm.km = ((Excel.Range)range.Cells[row, 1]).Text;
                                skm.dbt = ((Excel.Range)range.Cells[row, 2]).Text;
                                skm.dsh = ((Excel.Range)range.Cells[row, 3]).Text;
                                skm.ves = decimal.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                skm.nomenkl_nomer = ((Excel.Range)range.Cells[row, 5]).Text;
                                skm.link_nm_skm = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                skm.link_marka = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                skm.link_gost = int.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                skm.link_kgr = int.Parse(((Excel.Range)range.Cells[row, 9]).Text);
                                skm.link_ogt = int.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                                skm.link_GR_Mater = int.Parse(((Excel.Range)range.Cells[row, 11]).Text);
                                skm.link_ots = int.Parse(((Excel.Range)range.Cells[row, 12]).Text);
                                skm.link_prkm = int.Parse(((Excel.Range)range.Cells[row, 13]).Text);
                                skm.link_eizm = int.Parse(((Excel.Range)range.Cells[row, 14]).Text);
                                skm.link_balsch = int.Parse(((Excel.Range)range.Cells[row, 15]).Text);
                                skm.sort_OGT = int.Parse(((Excel.Range)range.Cells[row, 16]).Text);
                                skm.link_status = int.Parse(((Excel.Range)range.Cells[row, 17]).Text);
                                skm.link_user = int.Parse(((Excel.Range)range.Cells[row, 18]).Text);
                                skm.operation = ((Excel.Range)range.Cells[row, 19]).Text;
                                skm.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 20]).Text);
                                skm.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 21]).Text);
                                model_Spr_skm.Add(skm);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_cex":
                            var model_Spr_cex = db.Spr_cex;
                            Spr_cex spr_cex = new Spr_cex();
                            for (int row = 2; row <= range.Rows.Count; row++)                                
                            {                               
                                spr_cex.cex = ((Excel.Range)range.Cells[row, 1]).Text;
                                spr_cex.naim_cex = ((Excel.Range)range.Cells[row, 3]).Text;
                                spr_cex.nm_cex_krat = ((Excel.Range)range.Cells[row, 2]).Text;
                                spr_cex.link_cex_real = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                if (spr_cex.link_cex_real == 0)
                                {
                                    spr_cex.link_cex_real = null;
                                }
                                else
                                {
                                    spr_cex.link_cex_real = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                }
                                spr_cex.link_status = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                spr_cex.link_user = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                spr_cex.operation = ((Excel.Range)range.Cells[row, 7]).Text;
                                spr_cex.operation_date = DateTime.Now;
                                spr_cex.period_open_date = DateTime.Now;
                                model_Spr_cex.Add(spr_cex);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_prpokr":
                            var model_Spr_prpokr = db.Spr_prpokr;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Spr_prpokr spr_prpokr = new Spr_prpokr();
                                spr_prpokr.prpokr = ((Excel.Range)range.Cells[row, 1]).Text;
                                spr_prpokr.nm_prpokr = ((Excel.Range)range.Cells[row, 2]).Text;
                                spr_prpokr.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                spr_prpokr.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                spr_prpokr.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                spr_prpokr.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                spr_prpokr.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Spr_prpokr.Add(spr_prpokr);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_balsch":
                            var model_Spr_balsch = db.Spr_balsch;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Spr_balsch balsch = new Spr_balsch();
                                balsch.bal_schet = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                balsch.opis = ((Excel.Range)range.Cells[row, 2]).Text;
                                balsch.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                balsch.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                balsch.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                balsch.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                balsch.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Spr_balsch.Add(balsch);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_kgr":
                            var model_Spr_kgr = db.Spr_kgr;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                Spr_kgr kgr = new Spr_kgr();
                                kgr.kgr = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                kgr.fio = ((Excel.Range)range.Cells[row, 2]).Text;
                                kgr.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                kgr.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                kgr.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                kgr.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                kgr.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Spr_kgr.Add(kgr);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_OTS":
                            var model_Spr_OTS = db.SPR_OTS;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                SPR_OTS ots = new SPR_OTS();
                                ots.kod_sklad = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                ots.per = ((Excel.Range)range.Cells[row, 2]).Text;
                                ots.ots = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                ots.nomer_sklad = ((Excel.Range)range.Cells[row, 4]).Text;
                                ots.link_status = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                ots.link_user = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                ots.operation = ((Excel.Range)range.Cells[row, 7]).Text;
                                ots.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                ots.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 9]).Text);
                                model_Spr_OTS.Add(ots);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_OGT":
                            var model_Spr_OGT = db.SPR_OGT;
                            
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                SPR_OGT ogt = new SPR_OGT();
                                ogt.OGT = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                ogt.naim_ogt = ((Excel.Range)range.Cells[row, 2]).Text;
                                ogt.link_gr_mater = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                ogt.link_prkm = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                ogt.link_sort = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                ogt.ksim_km = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                ogt.link_status = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                ogt.link_user = int.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                ogt.operation = ((Excel.Range)range.Cells[row, 9]).Text;
                                ogt.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                                ogt.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 11]).Text);
                                model_Spr_OGT.Add(ogt);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_tto":
                            var model_Spr_tto = db.Spr_tto;
                            Spr_tto tto = new Spr_tto();
                            count_row = range.Rows.Count - 1;
                            TempData["row_count"] = count_row;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                                
                                tto.link_kod_TTO = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                tto.link_kod_komp = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                tto.link_cizg = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                tto.link_prkm = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                tto.link_prpokr = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                try
                                {
                                    tto.nrm = decimal.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                }
                                catch (Exception)
                                {
                                    tto.nrm = decimal.Parse(((Excel.Range)range.Cells[row, 6]).Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                                }
                                tto.vpost = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                try
                                {
                                    tto.nrvp = decimal.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                }
                                catch (Exception)
                                {
                                    tto.nrvp = decimal.Parse(((Excel.Range)range.Cells[row, 8]).Text, NumberStyles.Any, CultureInfo.InvariantCulture);
                                }
                                tto.krat = int.Parse(((Excel.Range)range.Cells[row, 9]).Text);
                                tto.vpost_sh = int.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                                tto.sort_kod_TTO = int.Parse(((Excel.Range)range.Cells[row, 11]).Text);
                                tto.sort_kod_komp = int.Parse(((Excel.Range)range.Cells[row, 12]).Text);
                                tto.link_status = int.Parse(((Excel.Range)range.Cells[row, 13]).Text);
                                tto.link_user = int.Parse(((Excel.Range)range.Cells[row, 14]).Text);
                                tto.operation = ((Excel.Range)range.Cells[row, 15]).Text;
                                tto.operation_date = DateTime.Now;
                                tto.period_open_date = DateTime.Now;
                                model_Spr_tto.Add(tto);
                                db.SaveChanges();
                            }
                            break;
                        case "Sort_Ogt":
                            var model_Sort_OGT = db.Sort_Mater;
                            Sort_Mater sort_ogt = new Sort_Mater();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                               
                                sort_ogt.usl_ru = ((Excel.Range)range.Cells[row, 1]).Text;
                                sort_ogt.sort_usl = ((Excel.Range)range.Cells[row, 2]).Text;
                                sort_ogt.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                sort_ogt.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                sort_ogt.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                sort_ogt.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                sort_ogt.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Sort_OGT.Add(sort_ogt);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_GR_Mater":
                            var model_Spr_GR_Mater = db.Spr_GR_Mater;
                            Spr_GR_Mater gr_mater = new Spr_GR_Mater();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                              
                                gr_mater.nomer_gr_mater = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                gr_mater.nm_gr_mater = ((Excel.Range)range.Cells[row, 2]).Text;
                                gr_mater.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                gr_mater.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                gr_mater.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                gr_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                gr_mater.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Spr_GR_Mater.Add(gr_mater);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_prkm":
                            var model_Spr_prkm = db.SPR_PRKM;
                            SPR_PRKM prkm = new SPR_PRKM();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                                
                                prkm.prkm = ((Excel.Range)range.Cells[row, 1]).Text;
                                prkm.nm_prkm = ((Excel.Range)range.Cells[row, 2]).Text;
                                prkm.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                prkm.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                prkm.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                prkm.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                prkm.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                model_Spr_prkm.Add(prkm);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_tem":
                            var model_Spr_tem = db.Spr_tem;
                            Spr_tem tem = new Spr_tem();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                tem.nm_tem_p = ((Excel.Range)range.Cells[row, 1]).Text;
                                tem.nm_tem_k = ((Excel.Range)range.Cells[row, 2]).Text;
                                tem.prim = ((Excel.Range)range.Cells[row, 3]).Text;
                                tem.link_status = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                tem.link_user = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                tem.operation = ((Excel.Range)range.Cells[row, 6]).Text;
                                tem.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                tem.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                model_Spr_tem.Add(tem);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_tematik":
                            var model_Spr_tematik = db.Spr_tematik;
                            Spr_tematik tematik = new Spr_tematik();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                tematik.nm_tematik_p = ((Excel.Range)range.Cells[row, 1]).Text;
                                tematik.nm_tematik_k = ((Excel.Range)range.Cells[row, 2]).Text;
                                tematik.prim = ((Excel.Range)range.Cells[row, 3]).Text;
                                tematik.link_status = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                tematik.link_user = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                tematik.operation = ((Excel.Range)range.Cells[row, 6]).Text;
                                tematik.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                tematik.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                model_Spr_tematik.Add(tematik);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_tehn_izgotov":
                            var model_spr_tehnol_izgot_proizv = db.Spr_tehnol_izgot_proizv;
                            Spr_tehnol_izgot_proizv tehnol_izgot_proizv = new Spr_tehnol_izgot_proizv();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                tehnol_izgot_proizv.nm_GR_tematik_p = ((Excel.Range)range.Cells[row, 1]).Text;
                                tehnol_izgot_proizv.nm_GR_tematik_k = ((Excel.Range)range.Cells[row, 2]).Text;
                                tehnol_izgot_proizv.prim = ((Excel.Range)range.Cells[row, 3]).Text;
                                tehnol_izgot_proizv.link_status = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                tehnol_izgot_proizv.link_user = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                tehnol_izgot_proizv.operation = ((Excel.Range)range.Cells[row, 6]).Text;
                                tehnol_izgot_proizv.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                tehnol_izgot_proizv.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                model_spr_tehnol_izgot_proizv.Add(tehnol_izgot_proizv);
                                db.SaveChanges();
                            }
                            break;
                        case "Raz_det":
                            var model_raz_det = db.Raz_det;
                            Raz_det raz_det = new Raz_det();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                raz_det.razd = ((Excel.Range)range.Cells[row, 1]).Text;
                                raz_det.naim_razd = ((Excel.Range)range.Cells[row, 2]).Text;
                                raz_det.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                raz_det.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                raz_det.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                raz_det.operation_date = DateTime.Now;
                                raz_det.period_open_date = DateTime.Now;
                                model_raz_det.Add(raz_det);
                                db.SaveChanges();
                            }
                            break;
                        case "PKP":
                            var model_pkp = db.Spr_PKP;
                            Spr_PKP _pkp = new Spr_PKP();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                _pkp.pkp = ((Excel.Range)range.Cells[row, 1]).Text;
                                _pkp.nm_pkp = ((Excel.Range)range.Cells[row, 2]).Text;
                                _pkp.link_razd = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                _pkp.link_razd_dse = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                _pkp.pkp_dos = ((Excel.Range)range.Cells[row, 5]).Text;
                                _pkp.link_status = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                _pkp.link_user = int.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                _pkp.operation = ((Excel.Range)range.Cells[row, 9]).Text;
                                _pkp.operation_date = DateTime.Now;
                                _pkp.period_open_date = DateTime.Now;
                                _pkp.ImageFileName = ((Excel.Range)range.Cells[row, 6]).Text;
                                model_pkp.Add(_pkp);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_agr":
                            var model_agr = db.Spr_agr;
                            Spr_agr _agr = new Spr_agr();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                _agr.gr_konstr = ((Excel.Range)range.Cells[row, 1]).Text;
                                _agr.agrk_k = ((Excel.Range)range.Cells[row, 2]).Text;
                                _agr.agrk_p = ((Excel.Range)range.Cells[row, 3]).Text;
                                _agr.link_agrgr = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                _agr.link_status = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                _agr.link_user = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                _agr.operation = ((Excel.Range)range.Cells[row, 7]).Text;
                                _agr.operation_date = DateTime.Now;
                                _agr.period_open_date = DateTime.Now;
                                model_agr.Add(_agr);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_kdan":
                            var model_kdan = db.Spr_kdan;
                            Spr_kdan _kdan = new Spr_kdan();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                _kdan.kdan = ((Excel.Range)range.Cells[row, 1]).Text;
                                _kdan.nm_kdan = ((Excel.Range)range.Cells[row, 2]).Text;
                                _kdan.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                _kdan.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                _kdan.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                _kdan.operation_date = DateTime.Now;
                                _kdan.period_open_date = DateTime.Now;
                                model_kdan.Add(_kdan);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_kompl":
                            var model_kompl = db.Spr_kompl;
                            Spr_kompl _kompl = new Spr_kompl();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                _kompl.komplekt = ((Excel.Range)range.Cells[row, 1]).Text;
                                _kompl.na_kompl = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                _kompl.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                _kompl.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                _kompl.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                _kompl.operation_date = DateTime.Now;
                                _kompl.period_open_date = DateTime.Now;
                                model_kompl.Add(_kompl);
                                db.SaveChanges();
                            }
                            break;
                        case "Razd_dse":
                            var model_razd_dse = db.Spr_Razd_DSE;
                            Spr_Razd_DSE razd_dse = new Spr_Razd_DSE();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                razd_dse.nm_razd_k = ((Excel.Range)range.Cells[row, 1]).Text;
                                razd_dse.nm_razd_p = ((Excel.Range)range.Cells[row, 2]).Text;
                                razd_dse.link_status = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                razd_dse.link_user = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                razd_dse.operation = ((Excel.Range)range.Cells[row, 5]).Text;
                                razd_dse.operation_date = DateTime.Now;
                                razd_dse.period_open_date = DateTime.Now;
                                model_razd_dse.Add(razd_dse);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_perizd":
                            var model_spr_perizd = db.Spr_Perizd;
                            Spr_Perizd spr_perizd = new Spr_Perizd();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                spr_perizd.izdelie = ((Excel.Range)range.Cells[row, 1]).Text;
                                spr_perizd.kod_izd = ((Excel.Range)range.Cells[row, 2]).Text;
                                spr_perizd.nm_izd = ((Excel.Range)range.Cells[row, 3]).Text;
                                spr_perizd.kgk1_1 = ((Excel.Range)range.Cells[row, 6]).Text;
                                spr_perizd.kgk1_n = ((Excel.Range)range.Cells[row, 7]).Text;
                                spr_perizd.kgk1_m = ((Excel.Range)range.Cells[row, 8]).Text;
                                spr_perizd.tek_ser_s = ((Excel.Range)range.Cells[row, 9]).Text;
                                spr_perizd.tek_ser_po = ((Excel.Range)range.Cells[row, 10]).Text;
                                spr_perizd.link_tema = int.Parse(((Excel.Range)range.Cells[row, 11]).Text);
                                spr_perizd.Model_tehnol_podgot_proizv = long.Parse(((Excel.Range)range.Cells[row, 12]).Text);                                                               
                                spr_perizd.prim = ((Excel.Range)range.Cells[row, 13]).Text;
                                spr_perizd.link_tematik = int.Parse(((Excel.Range)range.Cells[row, 14]).Text);
                                spr_perizd.pr_komplektov = ((Excel.Range)range.Cells[row, 15]).Text;
                                spr_perizd.soot_ss = ((Excel.Range)range.Cells[row, 16]).Text;
                                spr_perizd.soot_spo = ((Excel.Range)range.Cells[row, 17]).Text;
                                spr_perizd.link_status = int.Parse(((Excel.Range)range.Cells[row, 20]).Text);
                                spr_perizd.link_user = int.Parse(((Excel.Range)range.Cells[row, 21]).Text);
                                spr_perizd.operation = ((Excel.Range)range.Cells[row, 22]).Text;
                                spr_perizd.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 23]).Text);
                                spr_perizd.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 24]).Text);
                                model_spr_perizd.Add(spr_perizd);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_Sort":
                            var model_Spr_Sort = db.SPR_sortam;
                            SPR_sortam sortam = new SPR_sortam();
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {                               
                                sortam.sortament = ((Excel.Range)range.Cells[row, 1]).Text;
                                sortam.link_status = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                sortam.link_user = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                sortam.operation = ((Excel.Range)range.Cells[row, 4]).Text;
                                sortam.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                                sortam.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                model_Spr_Sort.Add(sortam);
                                db.SaveChanges();
                            }
                            break;

                        case "User":
                            var model_User = db.User;
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                count_row = range.Rows.Count - 1;
                                TempData["row_count"] = count_row;
                                User user = new User();
                                user.Last_Name = ((Excel.Range)range.Cells[row, 1]).Text;
                                user.First_Name = ((Excel.Range)range.Cells[row, 1]).Text;
                                user.Middle_Name = ((Excel.Range)range.Cells[row, 1]).Text;
                                model_User.Add(user);
                                db.SaveChanges();
                            }
                            break;
                        case "Spr_cen_mater":
                            Spr_cen_mater cen_mater = new Spr_cen_mater();
                            var model_Spr_cen_mater = db.Spr_cen_mater;                           
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                cen_mater.link_SKM = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                                cen_mater.cmat = decimal.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                                cen_mater.link_Predpr = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                                cen_mater.link_Obosnov = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                                cen_mater.god_prim_cen = ((Excel.Range)range.Cells[row, 5]).Text;
                                cen_mater.link_Valuta = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                                cen_mater.link_status = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                cen_mater.link_user = int.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                                cen_mater.operation = ((Excel.Range)range.Cells[row, 9]).Text;
                                cen_mater.operation_date = DateTime.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                                cen_mater.period_open_date = DateTime.Parse(((Excel.Range)range.Cells[row, 11]).Text);
                                model_Spr_cen_mater.Add(cen_mater);
                                db.SaveChanges();
                            }
                            break;
                        default:
                            TempData["msg"] = "<script>alert('Не выбрана База Данных!!!!!');</script>";
                            break;
                    }
                    if (sprav != "Spr_skm")
                    {
                        //db.SaveChanges();
                    }

                    app.Workbooks.Close();
                    app.Quit();

                    //string connetionString = null;
                    //SqlConnection connection;
                    //SqlParameter param;
                    //SqlParameter param_userId;
                    //var database = sprav;
                    //connetionString = "data source = i7-860; initial catalog = ASU_AVIA; user id = k6; password = jnltk35";
                    //connection = new SqlConnection(connetionString);
                    //try
                    //{
                    //    connection.Open();
                    //    SqlCommand cmd = new SqlCommand("Merge_temp", connection);
                    //    cmd.CommandType = CommandType.StoredProcedure;

                    //    param = new SqlParameter("@param", database);
                    //    param.Direction = ParameterDirection.Input;
                    //    cmd.Parameters.Add(param);

                    //    param_userId = new SqlParameter("@userId", userId);
                    //    param.Direction = ParameterDirection.Input;
                    //    cmd.Parameters.Add(param_userId);

                    //    cmd.ExecuteNonQuery();
                    //    connection.Close();
                    //}
                    //catch (Exception)
                    //{
                    //    TempData["msg"] = "<script>alert('Нет подключения к серверу Баз Данных!!!!!');</script>";
                    //}
                    sw.Stop();
                    TempData["timer"] = sw.Elapsed;

                    return View();
                }
                else
                {
                    ViewBag.Error = "Не корректный тип файла<br>";
                    return View();
                }
            }
            return View();
        }
    }
}

