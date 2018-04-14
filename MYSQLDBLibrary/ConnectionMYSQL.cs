using System;
using BaseDBLibrary;
using MySql.Data.MySqlClient;

namespace BaseDBLibrary
{
    class ConnectionMYSQL : ConnectionBase
    {
        private MySqlConnection conn;
        public override void Connect()
        {
            conn = new MySqlConnection(connString);
            conn.Open();
        }
        public override void Disconnect()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            conn.Close();
        }
        public MySqlConnection getConnection() { return conn; }
        public override void setConnString(string connString){base.connString = connString;}
        public override DBBaseClass clone(){return new ConnectionMYSQL();}
    }
}
