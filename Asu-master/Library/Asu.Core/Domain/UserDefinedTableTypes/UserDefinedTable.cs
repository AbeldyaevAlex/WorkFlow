namespace Asu.Core.Domain.UserDefinedTableTypes
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    public class UserDefinedTable
    {
        public static string GetTableTypeName<T>() where T : UserDefinedTable
        {
            var attr = typeof(T).GetCustomAttribute<UserDefinedTableTypeAttribute>();
            return attr == null ? null : attr.Type;
        }

        public static SqlParameter ToSqlParameter<T>(string name) where T : UserDefinedTable
        {
            return new SqlParameter(name, SqlDbType.Structured)
            {
                TypeName = GetTableTypeName<T>(),
                Value = ToDataTable<T>()
            };
        }

        public static SqlParameter ToSqlParameter<T>(string name, T entity) where T : UserDefinedTable
        {
            return new SqlParameter(name, SqlDbType.Structured)
            {
                TypeName = entity.GetTableTypeName(),
                Value = entity.ToDataTable()
            };
        }

        public static SqlParameter ToSqlParameter<T>(string name, IList<T> entities) where T : UserDefinedTable
        {
            return new SqlParameter(name, SqlDbType.Structured)
            {
                TypeName = GetTableTypeName<T>(),
                Value = ToDataTable(entities)
            };
        }

        public static DataTable ToDataTable<T>() where T : UserDefinedTable
        {
            var entity = Activator.CreateInstance<T>();
            var table = entity.GetDataTable();
            return table;
        }

        public static DataTable ToDataTable<T>(IList<T> entities) where T : UserDefinedTable
        {
            if (entities.Count == 0)
            {
               throw new ArgumentException("entities collection is empty");
            }

            var table = entities.First().GetDataTable();
            foreach (var entity in entities)
            {
                var row = table.NewRow();
                FillDataRow(row, entity);
                table.Rows.Add(row);
            }

            return table;
        }

        public string GetTableTypeName()
        {
            var attr = this.GetType().GetCustomAttribute<UserDefinedTableTypeAttribute>();
            return attr == null ? null : attr.Type;
        }

        public DataTable GetDataTable()
        {
            var type = this.GetType();
            var attr = type.GetCustomAttribute<UserDefinedTableTypeAttribute>();
            if (attr == null)
            {
                throw new ArgumentException("type is not marked as UserDefinedTableType");
            }

            if (type.GetProperties().All(i => i.GetCustomAttributes().All(p => !(p is UserDefinedTableTypePropertyAttribute))))
            {
                throw new ArgumentException("No one of type properties is marked as UserDefinedTableTypeProperty");
            }

            var table = new DataTable(attr.Type);
            var properties = type.GetProperties().Where(i => i.GetCustomAttributes().Any(p => p is UserDefinedTableTypePropertyAttribute));
            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            return table;
        }

        public DataTable ToDataTable()
        {
            var table = this.GetDataTable();
            var row = table.NewRow();
            FillDataRow(row, this);
            table.Rows.Add(row);
            return table;
        }

        private static void FillDataRow<T>(DataRow row, T entity) where T : UserDefinedTable
        {
            var properties = entity.GetType().GetProperties().Where(i => i.GetCustomAttributes().Any(p => p is UserDefinedTableTypePropertyAttribute));
            foreach (var property in properties)
            {
                row[property.Name] = property.GetValue(entity) ?? DBNull.Value;
            }
        }
    }
}
