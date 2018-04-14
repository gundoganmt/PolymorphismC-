using System;
using System.Windows.Forms;
using BaseDBLibrary;
using DBForm.Classes.Utils;

namespace DBForm
{
    public partial class Add_UpdateForm : Form
    {
        private IMainForm mainform;
        private StudentCatalog catalog;
        private QueryBase query;
        private ConnectionBase conn;

        private int add_arg = -1;
        private int setting;

        public Add_UpdateForm(int arg)
        {
            setting = arg;
            InitializeComponent();
            setup();
        }
        private void setup()
        {
            ObjectFactory factory = ObjectFactory.getInstance();
            mainform = factory.getMainForm();
            catalog = factory.getCatalog();
            query = (QueryBase)factory.create("query");
            conn = (ConnectionBase)factory.create("connection");
            conn.setConnString(ProjectVariables.getConnectionString());
            query.setConnection(conn);

            if (setting == add_arg)//adding operation
            {
                button1.Text = "Add";
            }
            else
            {                 //updating opertaion
                button1.Text = "Update";
                setTextBoxes();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (setting == add_arg)
            {//adding operation
                addOpp();
            }
            else
            {                     //updating operation         
                updateOpp();
            }
        }
        private void addOpp()
        {
            Student student = new Student();
            //student.Id = Int32.Parse(textBox1.Text);
            student.Name = textBox2.Text;
            student.Surname = textBox3.Text;
            student.BirthDate = textBox4.Text;

            try
            {
                conn.Connect();
                string selectQ = "insert into students (name,surname,birthdate) values(:PR1,:PR2,:PR3);";
                query.setSQL(selectQ);
                query.addParam("name", ParameterType.ptVarChar);
                query.setParamValueSTRING("name", student.Name);
                query.addParam("surname", ParameterType.ptVarChar);
                query.setParamValueSTRING("surname", student.Surname);
                query.addParam("birthdate", ParameterType.ptVarChar);
                query.setParamValueSTRING("birthdate", student.BirthDate);
                query.executeNonQuery();

                query.setSQL("select max(id) as last_id from students;");
                query.executeQuery();
                query.first();
                student.Id = query.getFiedValueINT("last_id");

                catalog.add(student);
                mainform.setListView();

                MessageBox.Show("Student is added");

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Setup error:" + ex.ToString());
            }
            finally
            {
                conn.Disconnect();
            }
        }
        private void updateOpp()
        {
            Student student = catalog.getByIndex(setting);
            student.Name = textBox2.Text;
            student.Surname = textBox3.Text;
            student.BirthDate = textBox4.Text;

            try
            {
                conn.Connect();
                string selectQ = "update students set name=:PR1,surname=:PR2,birthdate=:PR3 where id=:PR4";
                query.setSQL(selectQ);
                query.addParam("name", ParameterType.ptVarChar);
                query.setParamValueSTRING("name", student.Name);
                query.addParam("surname", ParameterType.ptVarChar);
                query.setParamValueSTRING("surname", student.Surname);
                query.addParam("birthdate", ParameterType.ptVarChar);
                query.setParamValueSTRING("birthdate", student.BirthDate);
                query.addParam("id", ParameterType.ptInteger);
                query.setParamValueINT("id", student.Id);
                query.executeNonQuery();

                mainform.setListView();

                MessageBox.Show("Student is updated");

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Setup error:" + ex.ToString());
            }
            finally
            {
                conn.Disconnect();
            }
        }
        private void setTextBoxes()
        {
            Student student = catalog.getByIndex(setting);
            textBox2.Text = student.Name;
            textBox3.Text = student.Surname;
            textBox4.Text = student.BirthDate;
        }
    }
}
