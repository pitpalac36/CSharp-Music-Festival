using csharpMusicFestival.service;
using csharpMusicFestival.repository;
using System;
using System.Windows.Forms;

namespace csharpMusicFestival
{
    public partial class LoginForm : Form
    {
        private UserService service;
        public LoginForm()
        {
            InitializeComponent();
            service = new UserService(new UserDbRepository());
            nameTextBox.Text = "";
            passwordTextBox.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Username or password was incorrectly typed!");
            }
            else
            {
                if (service.Login(nameTextBox.Text, passwordTextBox.Text))
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Wrong credentials!");
                    nameTextBox.Clear();
                    passwordTextBox.Clear();
                }
            }
        }
    }
}
