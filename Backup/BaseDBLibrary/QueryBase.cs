using System;

namespace BaseDBLibrary
{
    public enum ParameterType
    {
        ptVarChar, ptChar, ptTinyInt, ptSmallInt, ptInteger,
        ptBigInt, ptDouble, ptDateTime
    };

    public abstract class QueryBase : DBBaseClass
    {
        public abstract void setSQL(string sql);
        public abstract void addParam(string paramName, ParameterType type);
        public abstract void setParamValueINT(string paramName, int value);
        public abstract void setParamValueSTRING(string paramName, string value);
        public abstract void setParamValueDOUBLE(string paramName, double value);
        public abstract void executeQuery();
        public abstract int executeNonQuery();
        public abstract bool eof();
        public abstract void first();
        public abstract void next();
        public abstract int getFiedValueINT(string fieldname);
        public abstract string getFiedValueSTRING(string fieldname);
        public abstract double getFiedValueDOUBLE(string fieldname);
        public abstract void setConnection(ConnectionBase connection);
    }
}
