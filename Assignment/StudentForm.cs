using Assignment.Models;
using System.Windows.Forms;

namespace Assignment
{
    public partial class StudentForm : Form
    {
        private readonly Student _loggedInStudent;
        private readonly AppDbContext _db;

        public StudentForm(Student student)
        {
            InitializeComponent();
            _db = new AppDbContext();
            _loggedInStudent = student;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            textBox1.Text = _loggedInStudent.FirstName;
            textBox2.Text = _loggedInStudent.LastName;
            textBox3.Text = _loggedInStudent.Age.ToString();
            textBox4.Text = _loggedInStudent.Email;
            textBox5.Text = _loggedInStudent.Password;

            if (_loggedInStudent.GenderType == Gender.Male)
            {
                radioButton1.Checked = true;
            }
            else if (_loggedInStudent.GenderType == Gender.Female)
            {
                radioButton2.Checked = true;
            }
            comboBox1.DataSource = Enum.GetValues(typeof(Language));
            comboBox1.SelectedItem = _loggedInStudent.Languages;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var student = _db.Students.FirstOrDefault(s => s.Id == _loggedInStudent.Id);

                if (student != null)
                {
                    student.FirstName = textBox1.Text;
                    student.LastName = textBox2.Text;
                    student.Age = int.Parse(textBox3.Text);
                    student.Email = textBox4.Text;
                    student.Password = textBox5.Text;
                    student.GenderType = radioButton1.Checked ? Gender.Male : Gender.Female;
                    student.Languages = (Language)comboBox1.SelectedItem;

                    _db.SaveChanges();

                    MessageBox.Show("Student details updated successfully!");
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var student = _db.Students.FirstOrDefault(s => s.Id == _loggedInStudent.Id);

                if (student != null)
                {
                    _db.Students.Remove(student);
                    _db.SaveChanges();

                    MessageBox.Show("Student account deleted successfully!");

                    var loginForm = new LoginStudent();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the account: {ex.Message}");
            }
        }
    }
}
