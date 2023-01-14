using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                return;
            }

            if(textBox1.Text != User.ActiveUser.Password)
            {
                textBox1.Focus();
                return;
            }

            if(textBox2.Text != textBox3.Text)
            {
                textBox3.Focus();
                return;
            }

            User user = new User(User.ActiveUser.Number, User.ActiveUser.UserName, textBox3.Text);

            if (!(User.Update(user)))
            {
                MessageBox.Show("تعذر تنفيذ العملية.");
                return;
            }

            User.Login(user);

            this.Close();

        }
    }
}
