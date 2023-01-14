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
    public partial class frmLogin : Form
    {

        private bool sucess = false;

        internal bool Sucess
        {
            get
            { return sucess; }
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

            User user = new User(0, textBox1.Text, textBox2.Text);

            if (User.Login(user))
            {
                Event.Add(new Event("تسجيل دخول " + user.UserName, DateTime.Now));
                sucess = true;
            }
            else
            {
                Event.Add(new Event(" تعذر تسجيل دخول " + user.UserName, DateTime.Now));
            }
            this.Close();
        }
    }
}
