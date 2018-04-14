using System.Collections.Generic;
using System.IO;

namespace DBForm
{
    class FileOperator
    {
        private string studentslist = "students.txt";
        private string config = "config.txt";

        public void writeStudents(StudentCatalog catalog) {
            using (StreamWriter file = new StreamWriter(studentslist)) {
                List<Student> list = catalog.getList();
                foreach (Student student in list)
                    file.WriteLine(student.Id+" "+student.Name+" "+student.Surname+" "+student.BirthDate);
            }
        }

        public string readConfig() {
            string dllType = "";
            using (StreamReader file = new StreamReader(config))
            {
                dllType = file.ReadLine();
            }
            return dllType;
        }
    }
}
