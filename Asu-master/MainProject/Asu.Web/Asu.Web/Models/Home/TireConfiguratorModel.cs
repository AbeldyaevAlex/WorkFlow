namespace Asu.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class TireConfiguratorModel
    {
        public TireConfiguratorModel()
        {
            this.SectionValues = new List<SelectListItem>();
            this.AspectValues = new List<SelectListItem>();
            this.RimValues = new List<SelectListItem>();
        }

        public IList<SelectListItem> SectionValues { get; set; }

        public IList<SelectListItem> AspectValues { get; set; }

        public IList<SelectListItem> RimValues { get; set; }

        public string Section { get; set; }

        public string Aspect { get; set; }

        public string Rim { get; set; }
    }
}