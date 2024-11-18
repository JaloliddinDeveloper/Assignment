namespace Assignment
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginAdmin loginAdmin = new LoginAdmin();
            loginAdmin.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentMain studentMain= new StudentMain();
            studentMain.Show();
            this.Hide();
        }
    }
}
