using System;
using BaseDBLibrary;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

namespace BaseDBLibrary
{
    class QueryMYSQL : QueryBase
    {
        private ConnectionMYSQL connection;
        private MySqlCommand command;
        private MySqlDataReader reader;
        private bool EOF = false;
        private string sql;

        public override void setConnection(ConnectionBase connection)
        {
            this.connection = (ConnectionMYSQL)connection;
        }
        public override void setSQL(string sql)
        {
            this.sql = sql;
            command = new MySqlCommand(sql,connection.getConnection());
        }
        public override void executeQuery()
        {
            reader = command.ExecuteReader();
            command.Cancel();
        }
        public override int executeNonQuery()
        {
            return command.ExecuteNonQuery();
        }

        public override int getFiedValueINT(string fieldname)
        {
            string tmp = reader[fieldname].ToString();
            return Int32.Parse(tmp);
        }
        public override string getFiedValueSTRING(string fieldname)
        {
            return reader[fieldname].ToString();
        }
        public override bool eof() { return EOF; }
        public override void first()
        {
            if (!reader.Read()) EOF = true;
        }
        public override void next()
        {
            if (!reader.Read()) EOF = true;
        }

        public override DBBaseClass clone(){return new QueryMYSQL();}
    }
}
