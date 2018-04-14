using System;
using System.Collections.Generic;
using System.Reflection;
using BaseDBLibrary;
using DBForm.Classes.Utils;

namespace DBForm
{
    class ObjectFactory
    {
        private static ObjectFactory instance;
        private StudentCatalog catalog;
        private FileOperator f_operator;
        private IMainForm mainForm;
        private Dictionary<string, DBBaseClass> list;

        private ObjectFactory() { 
            list = new Dictionary<string,DBBaseClass>();
        }
        public static ObjectFactory getInstance()
        {
            if (instance == null) instance = new ObjectFactory();
            return instance;
        }
        public void loadDLL()
        {
            Assembly dll = Assembly.LoadFile(ProjectVariables.getDllPath());
            list = (Dictionary<string, DBBaseClass>)dll.GetType("BaseDBLibrary.DllMain").GetMethod("getRegisterList").Invoke(null, null);
        }
        public DBBaseClass create(string classname) {
            return list[classname].clone();
        }
        public StudentCatalog getCatalog()
        {
            if (catalog == null) catalog = new StudentCatalog();
            return catalog;
        }
        public void setMainForm(IMainForm mainform)
        {
            this.mainForm = mainform;
        }
        public IMainForm getMainForm() { return mainForm; }
        public FileOperator getFileOperator()
        {
            if (f_operator == null) f_operator = new FileOperator();
            return f_operator;
        }
    }
}
