using System;
using System.Text;
using System.Data.OleDb;
using BaseDBLibrary;

namespace BaseDBLibrary
{
    public class QueryOLE : QueryBase
    {
        private ConnectionOLE connection;
        private OleDbCommand command;
        private OleDbDataReader reader;
        private bool EOF = false;
        private string sql;

        public override DBBaseClass clone() { return new QueryOLE(); }
        public override void setConnection(ConnectionBase connection) { this.connection = (ConnectionOLE)connection; }
        public override void setSQL(string sql) {
            this.sql = sql;
            command = new OleDbCommand(sql, connection.getConnection()); 
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
        public override double getFiedValueDOUBLE(string fieldname)
        {
            string tmp = reader[fieldname].ToString();
            return double.Parse(tmp, System.Globalization.CultureInfo.InvariantCulture);
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
        public override void addParam(string paramName, ParameterType type)
        {
            int index = sql.IndexOf(':');
            StringBuilder sbuilder = new StringBuilder(sql);
            sbuilder.Remove(index, 4);
            sbuilder.Insert(index, "?");
            sql = sbuilder.ToString();
            command.CommandText = sql;

            OleDbParameter parameter = new OleDbParameter();
            parameter.ParameterName = paramName;
            switch (type)
            {
                case ParameterType.ptInteger:
                    parameter.OleDbType = OleDbType.Integer;
                    break;
                case ParameterType.ptDouble:
                    parameter.OleDbType = OleDbType.Double;
                    break;
                default:
                    parameter.OleDbType = OleDbType.VarChar;
                    break;
            }
            
            command.Parameters.Add(parameter);
        }
        public override void setParamValueINT(string paramName, int value)
        {
            command.Parameters[paramName].Value = value;
        }
        public override void setParamValueSTRING(string paramName, string value)
        {
            command.Parameters[paramName].Value = value;
        }
        public override void setParamValueDOUBLE(string paramName, double value)
        {
            command.Parameters[paramName].Value = value;
        }
    }
}
