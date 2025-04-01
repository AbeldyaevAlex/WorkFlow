using Asu.Core.Data;
using Asu.Core.Domain.Work;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Asu.Web.Areas.msi.Controllers
{
    public class ExceptionApiController : ApiController
    {
        [ResponseType(typeof(IEnumerable<ExceptionForWork>))]
        [Route("api/GetException")]
        [HttpGet]
        public IEnumerable<ExceptionForWork> GetException()
        {

            List<ExceptionForWork> exceptionForWork = new List<ExceptionForWork>();
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            using (var connection = new SqlConnection(settings.DataConnectionString))
            {
                using (var cmd = new SqlCommand("GettingExceptionForWork", connection))
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            exceptionForWork.Add(new ExceptionForWork()
                            {
                                Id = sdr.IsDBNull(0) ? 0 : sdr.GetInt32(0),
                                ShortName = sdr.IsDBNull(1) ? "" : sdr.GetString(1),
                                FullName = sdr.IsDBNull(2) ? "" : sdr.GetString(2),
                            });
                        }
                    }
                    connection.Close();
                }
            }
            return exceptionForWork;
        }
    }
}
