using Asu.Core.Data;
using Asu.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Asu.Web.Areas.msi.Controllers
{
    public class DirectiveWorkApiController : ApiController
    {
        [ResponseType(typeof(IEnumerable<WorkshopPortfolioViewModel>))]
        [Route("api/GetWorkshopPortfolio")]
        [HttpGet]
        public IEnumerable<WorkshopPortfolioViewModel> GetWorkshopPortfolio()
        {
            List<WorkshopPortfolioViewModel> workshopPortfolio = new List<WorkshopPortfolioViewModel>();
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            using (var connection = new SqlConnection(settings.DataConnectionString))
            {
                using (var cmd = new SqlCommand("TestGettingWorkshopPortfolioFromTreeProduct", connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            workshopPortfolio.Add(new WorkshopPortfolioViewModel()
                            {
                                PotrtfolioObozn = sdr.IsDBNull(0) ? "" : sdr.GetString(0),
                                Pkp = sdr.IsDBNull(1) ? "" : sdr.GetString(1),
                                Ss = sdr.IsDBNull(2) ? "" : sdr.GetString(2),
                                Spo = sdr.IsDBNull(3) ? "" : sdr.GetString(3),
                                Kizd = sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4),
                                Name = sdr.IsDBNull(5) ? "" : sdr.GetString(5),
                                Mas1sh = sdr.IsDBNull(7) ? 0 : sdr.GetDecimal(7),
                                MasIzd = sdr.IsDBNull(8) ? 0 : sdr.GetDecimal(8),
                                //Kp1 = sdr.IsDBNull(9) ? 0 : sdr.GetInt32(9),
                                //Kp2 = sdr.IsDBNull(10) ? 0 : sdr.GetInt32(10),
                                //Kp3 = sdr.IsDBNull(11) ? 0 : sdr.GetInt32(11),
                                RascexPoln = sdr.IsDBNull(12) ? "" : sdr.GetString(12),
                                NameIzdel = sdr.IsDBNull(14) ? "" : sdr.GetString(14),
                                NameRazdIzd = sdr.IsDBNull(15) ? "" : sdr.GetString(15),
                                NameGroup = sdr.IsDBNull(16) ? "" : sdr.GetString(16),
                                Komplekt = sdr.IsDBNull(17) ? "" : sdr.GetString(17),
                                Status = sdr.IsDBNull(18) ? "" : sdr.GetString(18),
                                Condition = sdr.IsDBNull(19) ? "" : sdr.GetString(19),
                                Workshop = sdr.IsDBNull(24) ? "" : sdr.GetString(24),
                                ExeptionOfWork = sdr.IsDBNull(25) ? "" : sdr.GetString(25),
                                DirectiveWorkSdelnIzgOnUnit = sdr.IsDBNull(22) ? 0 : sdr.GetDecimal(22),
                                DirectiveWorkSdelnUslOnUnit = sdr.IsDBNull(23) ? 0 : sdr.GetDecimal(23),
                                DirectiveWorkPovrIzgOnUnit = sdr.IsDBNull(20) ? 0 : sdr.GetDecimal(20),
                                DirectiveWorkPovrUslOnUnit = sdr.IsDBNull(21) ? 0 : sdr.GetDecimal(21),
                                DirectiveWorkSdelnIzgOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(22) ? 0 : sdr.GetDecimal(22)),
                                DirectiveWorkSdelnUslOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(23) ? 0 : sdr.GetDecimal(23)),
                                DirectiveWorkPovrIzgOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(20) ? 0 : sdr.GetDecimal(20)),
                                DirectiveWorkPovrUslOnProduct = (sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4)) * (sdr.IsDBNull(21) ? 0 : sdr.GetDecimal(21)),
                                DirectiveWorkId = sdr.IsDBNull(26) ? 0 : sdr.GetInt32(26),
                            });
                        }
                    }
                    connection.Close();
                }
            }
            return workshopPortfolio;
        }
    }
}
