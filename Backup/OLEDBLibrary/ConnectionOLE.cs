using System.Data;
using System.Data.OleDb;
using BaseDBLibrary;

namespace BaseDBLibrary
{
    public class ConnectionOLE : ConnectionBase
    {
        private OleDbConnection conn;

        public override void Connect()
        {
            conn = new OleDbConnection(connString);
            conn.Open();
        }
        public override void Disconnect() { if (conn.State == ConnectionState.Open) conn.Close(); }
        public OleDbConnection getConnection() { return conn; }
        public override void setConnString(string connString) { base.connString = connString; }
        public override DBBaseClass clone(){ return new ConnectionOLE(); }
    }
}
