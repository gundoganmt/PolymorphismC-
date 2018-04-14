using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDBLibrary;

namespace BaseDBLibrary
{
    class DllMain 
    {
        private static Dictionary<string, DBBaseClass> list;
        static DllMain() {
            list = new Dictionary<string, DBBaseClass>();
            list.Add("connection", new ConnectionMYSQL());
            list.Add("query", new QueryMYSQL());
        }
        public static Dictionary<string, DBBaseClass> getRegisterList()
        {
            return list;
        }
    }
}
