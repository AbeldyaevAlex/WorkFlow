using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using Asu.Mapping.Metrology;
using Asu.Mapping.Msi;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Areas.Metrology.Controllers
{
    public class AccuracyClassController : Controller
    {
        private readonly IAccuracyClassService _AccuracyClassService;
        private readonly IRepository<DocumentStatus> _DocumentStatusRepository;
        private readonly IRepository<Spr_klass_tochn> _sprKlassTochnRepository;
        private readonly IWorkContext _workContext;
        public AccuracyClassController(IAccuracyClassService AccuracyClassService, IRepository<DocumentStatus> DocumentStatusRepository,
            IWorkContext workContext, IRepository<Spr_klass_tochn> sprKlassTochnRepository)
        {
            _AccuracyClassService = AccuracyClassService;
            _DocumentStatusRepository = DocumentStatusRepository;
            _workContext = workContext;
            _sprKlassTochnRepository = sprKlassTochnRepository;
        }
        public ActionResult Index()
        {
            ViewBag.DocumentStatus = new SelectList(_DocumentStatusRepository.Table, "Id", "Status");
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartialKlassTochn()
        {
            var AccuracyClass = _AccuracyClassService.GetAllAccuracyClass();
            return PartialView("_GridViewPartialKlassTochn", AccuracyClass);
        }
        [HttpPost]
        public ActionResult Export(Spr_klass_tochn model, bool? isExcludeKlassTochn, bool? isExcludeStatus, bool? isExcludeUser,
                    bool? isFullStatus, string isFindKlassTochn, bool? isFullUser, bool? isUserData)
        {
            var znakKlassTochn = isExcludeKlassTochn == true ? " not " : " ";
            var znakStatus = isExcludeStatus == true ? " != " : " = ";
            var znakUser = isExcludeUser == true ? " != " : " = ";
            var znak_KlassTochn = isExcludeKlassTochn == true ? " != " : " = ";

            string usl_status = isFullStatus == false && model.DocumentStatusId > 0 ? " and (DocumentStatusId " + znakStatus + model.DocumentStatusId.ToString().Trim() + ")" : " ";
            string usl_User = "";
            if (isFullUser == false && _workContext.CurrentCustomer.Id > 0)
            {
                usl_User = " and (link_User " + znakUser + _workContext.CurrentCustomer.Id.ToString().Trim() + ")";
            }

            string usl_KlassTochn = "";
            if (!string.IsNullOrEmpty(model.klass_tochn))
            {
                usl_KlassTochn = isFindKlassTochn == "Частичное совпадение" ? "(klass_tochn " + znakKlassTochn + " like '%" + model.klass_tochn + "%')" : "(klass_tochn " + znak_KlassTochn + " '" + model.klass_tochn + "')";
                if (isExcludeKlassTochn == true)
                {
                    usl_KlassTochn = "(" + usl_KlassTochn + " or (klass_tochn is null))";
                }
            }


            string usl_select = "";
            if (!string.IsNullOrEmpty(model.klass_tochn))
            {
                usl_select = " where " + usl_KlassTochn + " " + usl_status + " " + usl_User;
            }
            else
            {
                if (string.IsNullOrEmpty(usl_status))
                {
                    usl_select = "";
                }
                else
                {
                    usl_select = (string.IsNullOrEmpty(usl_select) ? " where " : " ") + usl_status.Substring(5);
                }
                if (string.IsNullOrEmpty(usl_User))
                {
                    usl_select = "";
                }
                else
                {
                    usl_select = (string.IsNullOrEmpty(usl_select) ? " where " : " ") + usl_User.Substring(5);
                }
            }
            var i = usl_select;

            return View();
        }
        public ActionResult AdaptiveMode_GetProducts([DataSourceRequest] DataSourceRequest request, string name)
        {
            List<DocumentStatus> statusFilter = new List<DocumentStatus>();
            var statusList = _DocumentStatusRepository.Table.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                statusFilter = statusList.Where(p => p.Status.Contains(name)).ToList();
            }
            return Json(statusFilter, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialKlassTochnAddNew(Spr_klass_tochn model)
        {
            var anyKlassTochn = _sprKlassTochnRepository.Table.Where(x => x.klass_tochn.Trim() == model.klass_tochn.Trim()).ToList();
            if (anyKlassTochn.Count > 0)
            {
                ViewData["Success_KM"] = "Данная запись уже существует";
            }
            else
            {
                if(ModelState.IsValid)
                {
                    model.CustomerId = _workContext.CurrentCustomer.Id;
                    model.operation_date = DateTime.Now;
                    model.period_open_date = DateTime.Now;
                    model.link_pvi = (int)PviLevel.Insert;
                    model.DocumentStatusId = _DocumentStatusRepository.Table.Where(p => p.Status.Contains("Действует")).Select(p => p.Id).FirstOrDefault();
                    _sprKlassTochnRepository.Insert(model);
                    ViewData["Success_KM"] = "Запись добавлена";
                }
                return PartialView("_GridViewPartialKlassTochn", _AccuracyClassService.GetAllAccuracyClass());
            }
            return PartialView("_GridViewPartialKlassTochn", _AccuracyClassService.GetAllAccuracyClass());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdateKlassTochn(Spr_klass_tochn model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = _AccuracyClassService.GetAllAccuracyClass().Where(x => x.Id == model.Id).FirstOrDefault();
                    if (modelItem != null)
                    {
                        modelItem.klass_tochn = model.klass_tochn;
                        modelItem.CustomerId = _workContext.CurrentCustomer.Id;
                        modelItem.operation_date = DateTime.Now;
                        modelItem.period_open_date = DateTime.Now;
                        modelItem.link_pvi = (int)PviLevel.Update;
                        modelItem.DocumentStatusId = _DocumentStatusRepository.Table.Where(p => p.Status.Contains("Действует")).Select(p => p.Id).FirstOrDefault();
                        _sprKlassTochnRepository.Update(modelItem);
                        ViewData["Success_KM"] = "Корректировка записи прошла успешно";
                        return PartialView("_GridViewPartialKlassTochn", _AccuracyClassService.GetAllAccuracyClass());
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialKlassTochn", _AccuracyClassService.GetAllAccuracyClass());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialKlassTochnDelete(int Id)
        {
            var model = _sprKlassTochnRepository.Table;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                    {
                        _sprKlassTochnRepository.Delete(item);
                        ViewData["Success_KM"] = "Запись успешно удалена";
                    }                      
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialKlassTochn", _AccuracyClassService.GetAllAccuracyClass());
        }
    }
}