using Assignment.Models;

namespace Assignment
{
    public partial class SignInStudent : Form
    {
        private readonly AppDbContext db;
        private string pictureFilePath;
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
                    Languages = (Language)comboBox1.SelectedItem,
                    StudentPicture = SaveStudentPicture()
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

        private string SaveStudentPicture()
        {
            if (string.IsNullOrEmpty(pictureFilePath))
                return null;

            string targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
            Directory.CreateDirectory(targetPath);

            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(pictureFilePath)}";
            string destinationFile = Path.Combine(targetPath, uniqueFileName);

            File.Copy(pictureFilePath, destinationFile, true);
            return uniqueFileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void uploadPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureFilePath = ofd.FileName;
                    MessageBox.Show("Picture uploaded successfully!");
                }
            }
        }
    }
}
