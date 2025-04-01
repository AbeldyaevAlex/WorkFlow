using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core
{
    using Asu.Core.Data;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class DbHelper : IDbHelper
    {
        private readonly string connectionString;

        public DbHelper()
        {
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            this.connectionString = settings.DataConnectionString;
        }

        public T CallStoredProcedure<T>(string name, params object[] parameters)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(name, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("ProductId", (int)parameters[0]));
                        command.Parameters.Add(new SqlParameter("ChannelId", (int)parameters[1]));
                        command.Parameters.Add(new SqlParameter("ShipInDays", SqlDbType.Int) { Direction = ParameterDirection.Output });


                        connection.Open();
                        command.ExecuteScalar();
                        connection.Close();

                        T result = command.Parameters["ShipInDays"].Value != null ? (T)command.Parameters["ShipInDays"].Value : default(T);

                        return result;
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }
            }

            return default(T);
        }
    }
}
