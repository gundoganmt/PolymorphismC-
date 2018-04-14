using System;
using System.Windows.Forms;
using DBForm.Classes.Utils;

namespace DBForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ObjectFactory factory = ObjectFactory.getInstance();
            FileOperator f_operator = factory.getFileOperator();
            ProjectVariables.setDBType(f_operator.readConfig());
            factory.loadDLL();
            Application.Run(new Form1());
        }
    }
}
