using System;

namespace BaseDBLibrary
{
    public abstract class QueryBase : DBBaseClass
    {
        public abstract void setSQL(string sql);
        public abstract void executeQuery();
        public abstract int executeNonQuery();
        public abstract bool eof();
        public abstract void first();
        public abstract void next();
        public abstract int getFiedValueINT(string fieldname);
        public abstract string getFiedValueSTRING(string fieldname);
        public abstract void setConnection(ConnectionBase connection);
    }
}
