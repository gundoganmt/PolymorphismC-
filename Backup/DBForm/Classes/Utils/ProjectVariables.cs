using System.Configuration;
namespace DBForm.Classes.Utils
{
    class ProjectVariables
    {
        private static string dllPathMYSQL = @"C:\Users\Burak GUR\Documents\Visual Studio 2008\Projects\DBForm\MYSQLDBLibrary\bin\Debug\MYSQLDBLibrary.dll";
        private static string dllPathOLEDB = @"C:\Users\Burak GUR\Documents\Visual Studio 2008\Projects\DBForm\OLEDBLibrary\bin\Debug\OLEDBLibrary.dll";
        private static string dbtype;
        private static string dllPath;
        private static string connectionString;

        public static void setDBType(string dbtype) { 
            ProjectVariables.dbtype = dbtype;
            if (dbtype.CompareTo("oledb") == 0)
            {
                dllPath = dllPathOLEDB;
                connectionString = ConfigurationManager.ConnectionStrings["oledb"].ConnectionString;
            }
            else {
                dllPath = dllPathMYSQL;
                connectionString = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            }
        }
        public static string getDllPath() { return dllPath; }
        public static string getConnectionString() { return connectionString; }
    }
}
