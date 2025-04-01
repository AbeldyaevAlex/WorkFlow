using Asu.Core.Data;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Asu.Web.Validators.Skm
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        private readonly IRepository<GostMater> _GostMaterRepository;
        public ValidationController(IRepository<GostMater> GostMaterRepository)
        {
            _GostMaterRepository = GostMaterRepository;
        }

        public JsonResult IsProductName_Available(string gost)
        {

            GostMater existingProduct = _GostMaterRepository.Table.FirstOrDefault(product => product.Gost == gost);

            if (existingProduct == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            string suggestedName = String.Format(CultureInfo.InvariantCulture,
                "{0} is not available.", gost);

            for (int i = 1; i < 100; i++)
            {
                string altName = gost + i.ToString();

                if (_GostMaterRepository.Table.FirstOrDefault(product => product.Gost == altName) == null)
                {
                    suggestedName = String.Format(CultureInfo.InvariantCulture,
                   "{0} is not available. Try {1}.", gost, altName);
                    break;
                }
            }
            return Json(suggestedName, JsonRequestBehavior.AllowGet);
        }
    }
}