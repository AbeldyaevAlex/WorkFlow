using Asu.Core.Data;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial class TreeProductService : ITreeProductService
    {
        private readonly IRepository<TreeProduct> _treeProductRepository;
        public TreeProductService(IRepository<TreeProduct> treeProductRepository)
        {
            _treeProductRepository = treeProductRepository;
        }
        public List<TreeProduct> GetAllTreeProduct()
        {
            var TreeProctList = _treeProductRepository.Table.ToList();
            return TreeProctList;
        }

        public IQueryable<TreeProduct> GetNomenklature()
        {
            throw new NotImplementedException();
        }

        //public IQueryable<TreeProduct> GetNomenklature()
        //{
        //    IQueryable<TreeProduct> statesList = null;
        //    SqlCommand cmd = new SqlCommand();
        //    string query = "GetProduct";
        //    cmd.Parameters.AddWithValue("@ProductsIds", arr_izd);
        //    cmd.Parameters.AddWithValue("@Y_PKP", yes_pkp);
        //    cmd.Parameters.AddWithValue("@NO_PKP", no_pkp);
        //    cmd.CommandText = query;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    statesList = GetItemsTreeProducts(cmd);
        //}
        //private IQueryable<TreeProduct> GetItemsTreeProducts(SqlCommand cmd)
        //{
        //    List<TreeProduct> listItems = new List<TreeProduct>();
        //    string connetionString = ConfigurationManager.ConnectionStrings["AsuAviaContext"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(connetionString))
        //    {
        //        cmd.Connection = conn;
        //        conn.Open();
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                listItems.Add(new TreeProduct()
        //                {
        //                    Id = sdr.GetInt64(0),
        //                    obozn = sdr.GetString(1),
        //                    link_naim = sdr.GetInt64(2),
        //                    link_pkp = sdr.GetInt64(3),
        //                    link_status = sdr.GetInt32(4),
        //                    link_user = sdr.GetString(5),
        //                    obozn_p = sdr.GetString(6),
        //                    obozn_dos = sdr.GetString(7),
        //                    period_open_date = sdr.IsDBNull(9) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(9),
        //                    period_close_date = sdr.IsDBNull(8) ? (DateTime?)null : (DateTime?)sdr.GetDateTime(8),

        //                    operation_date = sdr.GetDateTime(12),
        //                    stsort_kt = sdr.GetString(13),
        //                    stsort_tip = sdr.GetString(14),
        //                    stsort_tr_1 = sdr.GetString(15),
        //                    stsort_tr_2 = sdr.GetString(16),
        //                    stsort_tr_3 = sdr.GetString(17),
        //                    stsort_tr_4 = sdr.GetString(18),
        //                    stsort_tr_5 = sdr.GetString(19),
        //                    stsort_tr_6 = sdr.GetString(20),
        //                    stsort_tr_7 = sdr.GetString(21),
        //                    var = sdr.GetString(22),
        //                    link_pvi = sdr.GetInt32(23)
        //                });
        //            }
        //        }
        //        conn.Close();
        //    }
        //    return listItems.AsQueryable();
        //}
    }
}
