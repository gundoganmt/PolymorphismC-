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
            Console.WriteLine("execute quey : " + command.CommandText);
            reader = command.ExecuteReader();
            command.Cancel();
        }
        public override int executeNonQuery()
        {
            Console.WriteLine("execute nonquey : " + command.CommandText);
            return command.ExecuteNonQuery();
        }
        public override double getFiedValueDOUBLE(string fieldname)
        {
            string tmp = reader[fieldname].ToString();
            return double.Parse(tmp, System.Globalization.CultureInfo.InvariantCulture);
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
        public override void addParam(string paramName, ParameterType type)
        {
            paramName = "?" + paramName;
            int index = sql.IndexOf(':');
            StringBuilder sbuilder = new StringBuilder(sql);
            sbuilder.Remove(index, 4);
            sbuilder.Insert(index, paramName);
            sql = sbuilder.ToString();
            command.CommandText = sql;

            MySqlParameter parameter = new MySqlParameter();
            switch (type)
            {
                case ParameterType.ptInteger:
                    parameter.MySqlDbType = MySqlDbType.Int32;
                    break;
                case ParameterType.ptDouble:
                    parameter.MySqlDbType = MySqlDbType.Double;
                    break;
                default:
                    parameter.MySqlDbType = MySqlDbType.VarChar;
                    break;
            }
            parameter.ParameterName = paramName;
            command.Parameters.Add(parameter);

        }
        public override void setParamValueDOUBLE(string paramName, double value)
        {
            paramName = "?" + paramName;
            command.Parameters[paramName].Value = value;
        }
        public override void setParamValueINT(string paramName, int value)
        {
            paramName = "?" + paramName;
            command.Parameters[paramName].Value = value;
        }
        public override void setParamValueSTRING(string paramName, string value)
        {
            paramName = "?" + paramName;
            command.Parameters[paramName].Value = value;
        }

        public override DBBaseClass clone(){return new QueryMYSQL();}
    }
}
