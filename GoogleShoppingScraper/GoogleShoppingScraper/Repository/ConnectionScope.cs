namespace GoogleShoppingScraper.Repository
{
    using System;
    using System.Data;
    using System.Data.Linq;
    using System.Data.SqlClient;
    using System.Transactions;

    public class ConnectionScope : IDisposable
    {
        private readonly DataContext context;
        private readonly bool dispose;

        public ConnectionScope(DataContext context, bool dispose = false)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
            this.dispose = dispose;

            this.UtilizeConnection();
        }

        public SqlConnection Connection => this.context.Connection as SqlConnection;

        public void Dispose()
        {
            if (this.dispose)
            {
                this.Connection.Close();
            }
        }

        private void UtilizeConnection()
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                this.Dispose();
            }

            if (this.Connection.State == ConnectionState.Open && Transaction.Current != null)
            {
                this.Connection.EnlistTransaction(Transaction.Current);
            }

            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.Open();
            }
        }
    }
}
