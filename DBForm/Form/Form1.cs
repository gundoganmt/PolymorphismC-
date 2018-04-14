using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BaseDBLibrary;
using DBForm.Classes.Utils;

namespace DBForm
{
    public partial class Form1 : Form, IMainForm
    {
        private ConnectionBase conn;
        private QueryBase query;
        private StudentCatalog catalog;
        private int nonselected = -1;
        private int selected_item_id;

        public Form1()
        {
            selected_item_id = nonselected;
            InitializeComponent();
            setup();
            setListView();

        }
        private void setup()
        {
            ObjectFactory factory = ObjectFactory.getInstance();
            factory.setMainForm(this);
            catalog = factory.getCatalog();
            query = (QueryBase)factory.create("query");
            conn = (ConnectionBase)factory.create("connection");
            conn.setConnString(ProjectVariables.getConnectionString());
            query.setConnection(conn);

            try
            {
                conn.Connect();
                string selectQ = "SELECT * FROM students";
                query.setSQL(selectQ);
                query.executeQuery();
                int i = 0;
                query.first();
                while (!query.eof())
                {
                    Student student = new Student();
                    student.Id = query.getFiedValueINT("ID");
                    student.Name = query.getFiedValueSTRING("NAME");
                    student.Surname = query.getFiedValueSTRING("SURNAME");
                    student.BirthDate = query.getFiedValueSTRING("BIRTH_DATE");
                    catalog.add(student);
                    Console.WriteLine(i); i++;
                    query.next();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Setup error:" + ex.ToString());
            }
            finally
            {
                conn.Disconnect();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Add_UpdateForm addForm = new Add_UpdateForm(nonselected);
            addForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (selected_item_id == nonselected)
                System.Windows.Forms.MessageBox.Show("Firstly, select a student id to update.");
            else
            {
                Add_UpdateForm addForm = new Add_UpdateForm(selected_item_id);
                addForm.Show();
                selected_item_id = nonselected;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (selected_item_id == nonselected)
                System.Windows.Forms.MessageBox.Show("Firstly, select a student id to delete.");
            else
            {
                Student student = catalog.getByIndex(selected_item_id);
                if (MessageBox.Show("Are you sure you want to delete " + student.Name + " ? ", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {
                        conn.Connect();
                        string selectQ = "DELETE FROM students WHERE ID = " + selected_item_id;
                        query.setSQL(selectQ);
                        query.executeNonQuery();
                        catalog.deleteByIndex(selected_item_id);
                        setListView();
                        selected_item_id = nonselected;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete error:" + ex.ToString());
                    }
                    finally
                    {
                        conn.Disconnect();
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to write student list? ", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FileOperator f_operator = ObjectFactory.getInstance().getFileOperator();
                f_operator.writeStudents(catalog);
                MessageBox.Show("Student list is wrote to students.txt file.");
            }

        }
        private void listviewItemSelectListener(object sender, ListViewItemSelectionChangedEventArgs args)
        {
            Console.WriteLine("selected item:" + args.ItemIndex);
            selected_item_id = args.ItemIndex;
        }
        private void mainFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show("Are you sure you want to really exit ? ", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
        }
        public void setListView()
        {
            List<Student> list = catalog.getList();
            listView1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Student student = list[i];
                listView1.Items.Add(student.Id.ToString());
                listView1.Items[i].SubItems.Add(student.Name);
                listView1.Items[i].SubItems.Add(student.Surname);
                listView1.Items[i].SubItems.Add(student.BirthDate);
            }
        }
    }
}


