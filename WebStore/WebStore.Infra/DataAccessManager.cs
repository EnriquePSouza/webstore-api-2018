using System;
using System.Data;
using System.Data.SqlClient;
using WebStore.Shared;

namespace WebStore.Infra
{
    public class DataAccessManager : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public DataAccessManager()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.BeginTransaction();
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}