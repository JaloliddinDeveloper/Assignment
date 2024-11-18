using Assignment.Models;

namespace Assignment
{
    public partial class LoginAdmin : Form
    {
        private readonly AppDbContext db;
        public LoginAdmin()
        {
            InitializeComponent();
            db = new AppDbContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string enteredEmail = textBox1.Text;
                string enteredPassword = textBox2.Text;

                var student = db.Admins.FirstOrDefault(s => s.Name == enteredEmail && s.Password == enteredPassword);

                if (student != null)
                {
                    MessageBox.Show("Login successful!");
                    var adminstudent = new StudentsAdmin();
                    adminstudent.Show();
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
