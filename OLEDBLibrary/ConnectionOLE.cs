using System.Data;
using System.Data.SqlClient;
using BaseDBLibrary;

namespace BaseDBLibrary
{
    public class ConnectionOLE : ConnectionBase
    {
        private SqlConnection conn;

        public override void Connect()
        {
            conn = new SqlConnection(connString);
            conn.Open();
        }
        public override void Disconnect() { if (conn.State == ConnectionState.Open) conn.Close(); }
        public SqlConnection getConnection() { return conn; }
        public override void setConnString(string connString) { base.connString = connString; }
        public override DBBaseClass clone(){ return new ConnectionOLE(); }
    }
}
