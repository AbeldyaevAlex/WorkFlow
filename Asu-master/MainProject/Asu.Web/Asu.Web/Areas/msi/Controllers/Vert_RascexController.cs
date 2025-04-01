using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.msi.Controllers
{
    public class Vert_RascexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        Asu.Web.Models.ASU_AVIAEntities12 db = new Asu.Web.Models.ASU_AVIAEntities12();

        [ValidateInput(false)]
        public ActionResult VerticalGridPartial()
        {
            var model = db.Spr_rasc_vert;
            return PartialView("_VerticalGridPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VerticalGridPartialUpdate(MVCxGridBatchUpdateValues<Asu.Web.Models.Spr_rasc_vert, System.Int64> updateValues)
        {
            foreach (var record in updateValues.Insert)
            {
                if (updateValues.IsValid(record))
                    VerticalGridPartialInsertRecord(record, updateValues);
            }
            foreach (var record in updateValues.Update)
            {
                if (updateValues.IsValid(record))
                    VerticalGridPartialUpdateRecord(record, updateValues);
            }
            foreach (var key in updateValues.DeleteKeys)
            {
                VerticalGridPartialDeleteRecord(key, updateValues);
            }

            var model = db.Spr_rasc_vert;
            return PartialView("_VerticalGridPartial", model.ToList());
        }
        protected void VerticalGridPartialInsertRecord(Asu.Web.Models.Spr_rasc_vert record, MVCxGridBatchUpdateValues<Asu.Web.Models.Spr_rasc_vert, System.Int64> updateValues)
        {
            var model = db.Spr_rasc_vert;
            try
            {
                model.Add(record);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(record, e.Message);
            }
        }
        protected void VerticalGridPartialUpdateRecord(Asu.Web.Models.Spr_rasc_vert record, MVCxGridBatchUpdateValues<Asu.Web.Models.Spr_rasc_vert, System.Int64> updateValues)
        {
            var model = db.Spr_rasc_vert;
            try
            {
                var modelItem = model.FirstOrDefault(it => it.Id == record.Id);
                db.Entry(modelItem).CurrentValues.SetValues(record);
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(record, e.Message);
            }
        }
        protected void VerticalGridPartialDeleteRecord(System.Int64 Id, MVCxGridBatchUpdateValues<Asu.Web.Models.Spr_rasc_vert, System.Int64> updateValues)
        {
            var model = db.Spr_rasc_vert;
            try
            {
                var item = model.FirstOrDefault(it => it.Id == Id);
                if (item != null)
                    model.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(Id, e.Message);
            }
        }
    }
}