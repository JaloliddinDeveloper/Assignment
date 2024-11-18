using Assignment.Models;

namespace Assignment
{
    public partial class SignInStudent : Form
    {
        private readonly AppDbContext db;
        public SignInStudent()
        {
            InitializeComponent();
            db = new AppDbContext();
            db.Database.EnsureCreated();
        }

        private void SignInStudent_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(Language));
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedGender = radioButton1.Checked ? Gender.Male : Gender.Female;

                var student = new Student
                {
                    FirstName = textBox1.Text,
                    LastName = textBox2.Text,
                    Age = int.Parse(textBox3.Text),
                    Email = textBox4.Text,
                    Password = textBox5.Text,
                    GenderType = selectedGender,
                    Languages = (Language)comboBox1.SelectedItem
                };

                db.Students.Add(student);
                db.SaveChanges();

                MessageBox.Show("Student saved successfully!");
                var nextForm = new LoginStudent();
                nextForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }
    }
}
