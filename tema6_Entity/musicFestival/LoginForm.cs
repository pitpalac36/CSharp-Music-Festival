using musicFestival;
using System;
using System.Windows.Forms;

namespace csharpMusicFestival
{
    public partial class LoginForm : Form
    {
        private ClientController controller;
        public LoginForm(ClientController controller)
        {
            InitializeComponent();
            this.controller = controller;
            nameTextBox.Text = "";
            passwordTextBox.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Username or password was incorrectly typed!");
            }
            else
            {
                try
                {
                    controller.Login(nameTextBox.Text, passwordTextBox.Text);
                    this.Hide();
                    MainForm mainForm = new MainForm(controller);
                    mainForm.Show();
                } catch (Exception ex)
                {
                    MessageBox.Show("Wrong credentials! " + ex.Message);
                    nameTextBox.Clear();
                    passwordTextBox.Clear();
                }
            }
        }
    }
}
