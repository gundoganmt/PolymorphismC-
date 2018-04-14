using System.Configuration;
namespace DBForm.Classes.Utils
{
    class ProjectVariables
    {
        private static string dllPathMYSQL = @"C:\\Users\\mahmut\\Desktop\\DBForm\\MYSQLDBLibrary\\bin\\Debug\\MYSQLDBLibrary.dll";
        private static string dllPathOLEDB = @"C:\\Users\\mahmut\\Desktop\\DBForm\\OLEDBLibrary\\bin\\Debug\\OLEDBLibrary.dll";
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
