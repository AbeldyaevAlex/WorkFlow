namespace GoogleShoppingScraper.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Linq;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;

    public class DatabaseContext : DataContext
    {
        private static readonly Random Rnd = new Random();
        private readonly string connectionString;

        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
            this.connectionString = connectionString;
            this.Proxies = this.GetTable<ProxyEntity>();
            this.KeywordQueue = this.GetTable<KeywordQueue>();
            this.Logs = this.GetTable<Log>();
            this.BrandMapping = this.GetTable<BrandMapping>();
        }

        public Table<Log> Logs { get; set; }

        public Table<KeywordQueue> KeywordQueue { get; set; }

        public Table<ProxyEntity> Proxies { get; set; }

        public Table<BrandMapping> BrandMapping { get; set; }

        public bool SaveSearchResults(DataTable results, out string errorMessage, int attempt = 1)
        {
            try
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand("SaveSearchResults", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Results",
                        Value = results,
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "SearchResult"
                    });

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    errorMessage = null;
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (--attempt == 0)
                {
                    errorMessage = ex.Message;
                    return false;
                }

                switch (ex.Number)
                {
                    case 1205:  // SQL_DEADLOCK_ERROR_CODE
                    case -2:    // SQL_TIMEOUT_ERROR_CODE
                        Thread.Sleep(Rnd.Next(5000, 10000));
                        return this.SaveSearchResults(results, out errorMessage, attempt);
                    default:
                        errorMessage = ex.Message;
                        return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool ReduceSearchRank(DataTable keywordRankReducingInfo, out string errorMessage, int attempt = 1)
        {
            try
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand("ReduceKeywordsSearchRank", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 600
                    };

                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@KeywordRankReducingInfo",
                        SqlDbType = SqlDbType.Structured,
                        Value = keywordRankReducingInfo,
                        TypeName = "KeywordRankReducingInfo"
                    });

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                errorMessage = null;
                return true;
            }
            catch (SqlException ex)
            {
                if (--attempt == 0)
                {
                    errorMessage = ex.Message;
                    return false;
                }

                switch (ex.Number)
                {
                    case 1205:  // SQL_DEADLOCK_ERROR_CODE
                    case -2:    // SQL_TIMEOUT_ERROR_CODE
                        Thread.Sleep(Rnd.Next(3000, 10000));
                        return this.ReduceSearchRank(keywordRankReducingInfo, out errorMessage, attempt);
                    default:
                        errorMessage = ex.Message;
                        return false;
                }
            }
        }

        public IEnumerable<KeywordQueue> LoadKeywords(DataTable productIds, int keywordsLoadingBufferSize, int lastSaveFailedPercent, out string errorMessage, int attempt = 1)
        {
            try
            {
                IEnumerable<KeywordQueue> keywords;
                using (var scope = new ConnectionScope(this))
                {
                    var procedure = scope.Connection.CreateProcedure("GetKeywords");
                    procedure.CommandTimeout = 6000;
                    procedure.AddTvp("@ProductIds", "IntArray", productIds);
                    procedure.AddParameter("@KeywordsAmount", SqlDbType.Int, keywordsLoadingBufferSize);
                    procedure.AddParameter("@LastSaveFailedPercent", SqlDbType.Int, lastSaveFailedPercent);
                    var result = this.Translate(procedure.ExecuteReader());
                    keywords = result.GetResult<KeywordQueue>().ToList();
                }

                errorMessage = null;

                return keywords;
            }
            catch (SqlException ex)
            {
                if (--attempt == 0)
                {
                    errorMessage = ex.Message;
                    return null;
                }

                switch (ex.Number)
                {
                    case 1205: // SQL_DEADLOCK_ERROR_CODE
                    case -2: // SQL_TIMEOUT_ERROR_CODE
                        Thread.Sleep(Rnd.Next(300, 1000));
                        return this.LoadKeywords(productIds, keywordsLoadingBufferSize, lastSaveFailedPercent, out errorMessage, attempt);
                    default:
                        errorMessage = ex.Message;
                        return null;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public bool SaveProxiesStatistic(DataTable proxies, out string errorMessage, int attempt = 1)
        {
            try
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand("SaveProxiesStatistic", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Proxies",
                        Value = proxies,
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "Proxy"
                    });

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    errorMessage = null;
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (--attempt == 0)
                {
                    errorMessage = ex.Message;
                    return false;
                }

                switch (ex.Number)
                {
                    case 1205:  // SQL_DEADLOCK_ERROR_CODE
                    case -2:    // SQL_TIMEOUT_ERROR_CODE
                        Thread.Sleep(Rnd.Next(300, 1000));
                        return this.SaveProxiesStatistic(proxies, out errorMessage, attempt);
                    default:
                        errorMessage = ex.Message;
                        return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
