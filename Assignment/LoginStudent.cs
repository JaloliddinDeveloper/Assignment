using Assignment.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Assignment
{
    public partial class LoginStudent : Form
    {
        private readonly AppDbContext db;

        public LoginStudent()
        {
            InitializeComponent();
            db = new AppDbContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainForm = new StudentMain();
            mainForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string enteredEmail = textBox1.Text;
                string enteredPassword = textBox2.Text;

                var student = db.Students.FirstOrDefault(s => s.Email == enteredEmail && s.Password == enteredPassword);

                if (student != null)
                {
                    MessageBox.Show("Login successful!");
                    var studentForm = new StudentForm(student);
                    studentForm.Show();
                    this.Hide(); 
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
