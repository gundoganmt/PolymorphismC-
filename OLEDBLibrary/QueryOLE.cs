using System;
using System.Text;
using System.Data.SqlClient;
using BaseDBLibrary;

namespace BaseDBLibrary
{
    public class QueryOLE : QueryBase
    {
        private ConnectionOLE connection;
        private SqlCommand command;
        private SqlDataReader reader;
        private bool EOF = false;
        private string sql;

        public override DBBaseClass clone() { return new QueryOLE(); }
        public override void setConnection(ConnectionBase connection) { this.connection = (ConnectionOLE)connection; }
        public override void setSQL(string sql) {
            this.sql = sql;
            command = new SqlCommand(sql, connection.getConnection()); 
        }
        public override void executeQuery() { reader = command.ExecuteReader(); }
        public override int executeNonQuery() {
            Console.WriteLine(sql);
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
    }
}
