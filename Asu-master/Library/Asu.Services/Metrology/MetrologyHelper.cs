using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Asu.Mapping.Metrology
{
    public partial class MetrologyHelper : IMetrologyHelper
    {
        private const string PARAMETER_RODPOVERK_COOKIE_KEY = "WC.Parameter.RodPoverk.Cookie";
        private const string WORKSHOP_METROLOGY_COOKIE_KEY = "WC.Workshop.Metrology.Cookie";
        private const string WORKSHOPID_METROLOGY_COOKIE_KEY = "WC.WorkshopId.Metrology.Cookie";

        private readonly HttpContextBase httpContext;
        private readonly IRepository<Spr_cex> _sprCexRepository;

        public MetrologyHelper(HttpContextBase httpContext, IRepository<Spr_cex> sprCexRepository)
        {
            this.httpContext = httpContext;
            _sprCexRepository = sprCexRepository;
        }
        public void SetParametrRodPoverkToCookies(string NmRodPoverk)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var NmRodPoverkCookie = this.httpContext.Request.Cookies.Get(PARAMETER_RODPOVERK_COOKIE_KEY);
            if (NmRodPoverkCookie != null)
            {
                httpContext.Response.Cookies.Remove(PARAMETER_RODPOVERK_COOKIE_KEY);
            }

            NmRodPoverkCookie = new HttpCookie(PARAMETER_RODPOVERK_COOKIE_KEY);
            NmRodPoverkCookie.Value = string.Format(NmRodPoverk);
            NmRodPoverkCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(NmRodPoverkCookie);
        }

        public void SetWorkshopIdToCookies(int WorkshopId)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }
            var NameWorksopIdCookie = this.httpContext.Request.Cookies.Get(WORKSHOPID_METROLOGY_COOKIE_KEY);
            if (NameWorksopIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(WORKSHOPID_METROLOGY_COOKIE_KEY);
            }
            NameWorksopIdCookie = new HttpCookie(WORKSHOPID_METROLOGY_COOKIE_KEY);
            NameWorksopIdCookie.Value = WorkshopId.ToString();
            NameWorksopIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(NameWorksopIdCookie);
        }

        public void SetWorkshopToCookies(int WorkshopId)
        {
            var shortNameWorksop = _sprCexRepository.Table.Where(x => x.Id == WorkshopId).Select(c => c.NmCexKrat).FirstOrDefault();
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }
            var shortNameWorksopCookie = this.httpContext.Request.Cookies.Get(WORKSHOP_METROLOGY_COOKIE_KEY);
            if (shortNameWorksopCookie != null)
            {
                httpContext.Response.Cookies.Remove(WORKSHOP_METROLOGY_COOKIE_KEY);
            }
            shortNameWorksopCookie = new HttpCookie(WORKSHOP_METROLOGY_COOKIE_KEY);
            shortNameWorksopCookie.Value = string.Format(shortNameWorksop);
            shortNameWorksopCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(shortNameWorksopCookie);
        }
    }
}
