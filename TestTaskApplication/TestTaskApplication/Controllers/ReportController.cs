using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainCore;
using EFCore;
using EFCore.Model;

namespace TestTaskApplication.Controllers
{
    public class ReportController : ApiController
    {
        ShopDBEntities2 db;
        public ReportController()
        {
            db = new ShopDBEntities2();
        }
        public IEnumerable<Order> GetTask()
        {
            List<Order> order = new List<Order>();
            var companies = from person in db.Task
                            group person by person.RoleId into g
                            select new
                            {
                                Name = g.Key,
                                Count = g.Count(),
                                OpenDate = g.Select(c => c.OpenDate).FirstOrDefault(),
                                ClosedDate = g.Select(c => c.ClosedDate)
                            };

            foreach (var company in companies)
            {
                Order modelOrder = new Order();
                modelOrder.Role = db.CustomerRole.Where(x => x.Id == company.Name).Select(g => g.Name).FirstOrDefault();
                modelOrder.CountOrder = company.Count;
                order.Add(modelOrder);
            }
            return order;
        }
        //public IHttpActionResult GetReport(DateTime? OpenDate, DateTime? CloseDate)
        //{
        //    List<Order> order = new List<Order>();
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ShopDBEntities1"].ConnectionString;
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        using (var cmd = new SqlCommand("GetAllOrder", connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter OpDate = new SqlParameter("@OpenDate", OpenDate);
        //            OpDate.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(OpDate);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter ClDate = new SqlParameter("@CloseDate", CloseDate);
        //            ClDate.Direction = ParameterDirection.Input;
        //            cmd.Parameters.Add(ClDate);

        //            connection.Open();
        //            cmd.CommandTimeout = 60;

        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    order.Add(new Order
        //                    {
        //                        Role = sdr.IsDBNull(0) ? "" : sdr.GetString(0),
        //                        CountOrder = sdr.IsDBNull(1) ? 0 : sdr.GetInt32(1),
        //                        OpenDate = sdr.IsDBNull(2) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(2),
        //                        CloseDate = sdr.IsDBNull(3) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(3)
        //                    });
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //    return Ok(order);
        //}
    }
}
