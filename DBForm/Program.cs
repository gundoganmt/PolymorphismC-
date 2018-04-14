using System;
using System.Windows.Forms;
using DBForm.Classes.Utils;

namespace DBForm
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            ObjectFactory factory = ObjectFactory.getInstance();
            FileOperator f_operator = factory.getFileOperator();
            ProjectVariables.setDBType(f_operator.readConfig());
            factory.loadDLL();
            Application.Run(new Form1());
        }
    }
}
