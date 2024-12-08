using Assignment.Models;

namespace Assignment
{
    public partial class StudentsAdmin : Form
    {
        private readonly AppDbContext db;
        private int? selectedStudentId = null;
        private string pictureFilePath;

        public StudentsAdmin()
        {
            InitializeComponent();
            db = new AppDbContext();
            db.Database.EnsureCreated();
        }


        private void StudentsAdmin_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(Language));

            comboBox1.SelectedIndex = -1;

            LoadStudentData();
        }


        private void LoadStudentData()
        {
            try
            {
                var students = db.Students.ToList();

                dataGridView1.DataSource = students;

                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["FirstName"].HeaderText = "First Name";
                dataGridView1.Columns["LastName"].HeaderText = "Last Name";
                dataGridView1.Columns["Age"].HeaderText = "Age";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Password"].Visible = false;
                dataGridView1.Columns["GenderType"].HeaderText = "Gender";
                dataGridView1.Columns["Languages"].HeaderText = "Language";
                dataGridView1.Columns["StudentPicture"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading student data: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                var selectedStudent = selectedRow.DataBoundItem as Student;

                if (selectedStudent != null)
                {
                    selectedStudentId = selectedStudent.Id;
                    textBox1.Text = selectedStudent.FirstName;
                    textBox2.Text = selectedStudent.LastName;
                    textBox3.Text = selectedStudent.Age.ToString();
                    textBox4.Text = selectedStudent.Email;
                    textBox5.Text = selectedStudent.Password;

                    if (selectedStudent.GenderType == Gender.Male)
                    {
                        radioButton1.Checked = true;
                        radioButton2.Checked = false;
                    }
                    else if (selectedStudent.GenderType == Gender.Female)
                    {
                        radioButton2.Checked = true;
                        radioButton1.Checked = false;
                    }

                    comboBox1.SelectedItem = selectedStudent.Languages;
                    pictureFilePath = selectedStudent.StudentPicture;
                }
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedGender = radioButton1.Checked ? Assignment.Models.Gender.Male : Assignment.Models.Gender.Female;

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

                ClearForm();
                LoadStudentData();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStudentId == null)
                {
                    MessageBox.Show("Please select a student to update.");
                    return;
                }

                var selectedGender = radioButton1.Checked ? Assignment.Models.Gender.Male : Assignment.Models.Gender.Female;

                var student = db.Students.Find(selectedStudentId);

                if (student != null)
                {
                    student.FirstName = textBox1.Text;
                    student.LastName = textBox2.Text;
                    student.Age = int.Parse(textBox3.Text);
                    student.Email = textBox4.Text;
                    student.Password = textBox5.Text;
                    student.GenderType = selectedGender;
                    student.Languages = (Language)comboBox1.SelectedItem;

                    db.Students.Update(student);
                    db.SaveChanges();

                    MessageBox.Show("Student updated successfully!");

                    ClearForm();
                    LoadStudentData();
                }
                else
                {
                    MessageBox.Show("Student not found in the database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStudentId == null)
                {
                    MessageBox.Show("Please select a student to delete.");
                    return;
                }

                var student = db.Students.Find(selectedStudentId);

                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();

                    MessageBox.Show("Student deleted successfully!");

                    ClearForm();
                    LoadStudentData();
                }
                else
                {
                    MessageBox.Show("Student not found in the database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            comboBox1.SelectedIndex = -1;

            textBox1.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

      
    }
}
