using System.Collections.Generic;

namespace DBForm
{
    class StudentCatalog
    {
        private List<Student> list;
        public StudentCatalog()
        {
            list = new List<Student>();
        }
        public void add(Student student) {
            list.Add(student);
        }
        public List<Student> getList() { return list; }
        public Student getByIndex(int id) { return list[id]; }
        public void deleteByIndex(int id) { list.RemoveAt(id); }
    }
}
