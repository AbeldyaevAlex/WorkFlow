namespace GoogleShoppingScraper.Repository
{
    using System;
    using System.Data;
    using System.Data.Linq;
    using System.Data.SqlClient;

    public static class Extensions
    {
        public static ConnectionScope GetConnectionScope(this DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return new ConnectionScope(context);
        }

        public static SqlCommand CreateProcedure(this SqlConnection connection, string procedureName)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            return new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure };
        }

        public static SqlParameter AddTvp(this SqlCommand command, string parameterName, string typeName, DataTable value)
        {
            return command.AddParameter(parameterName, SqlDbType.Structured, typeName, value);
        }

        public static SqlParameter AddParameter(this SqlCommand command, string parameterName, SqlDbType type, object value)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var parameter = command.Parameters.AddWithValue(parameterName, value);
            parameter.SqlDbType = type;
            return parameter;
        }

        public static SqlParameter AddParameter(this SqlCommand command, string parameterName, SqlDbType type, string typeName, object value)
        {
            var parameter = command.AddParameter(parameterName, type, value);
            parameter.TypeName = typeName;
            return parameter;
        }

        public static SqlParameter AddOutParameter(this SqlCommand command, string parameterName, SqlDbType type)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var parameter = command.Parameters.Add(parameterName, type);
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static void Truncate<TEntity>(this Table<TEntity> table) where TEntity : class
        {
            var sqlCommand = $"TRUNCATE TABLE {table.GetTableName()}";
            table.Context.ExecuteCommand(sqlCommand);
        }

        public static void DeleteAll<TEntity>(this Table<TEntity> table) where TEntity : class
        {
            var sqlCommand = $"DELETE FROM {table.GetTableName()}";
            table.Context.ExecuteCommand(sqlCommand);
        }

        public static string GetTableName<TEntity>(this Table<TEntity> table) where TEntity : class
        {
            var rowType = table.GetType().GetGenericArguments()[0];
            return table.Context.Mapping.GetTable(rowType).TableName;
        }
    }
}
