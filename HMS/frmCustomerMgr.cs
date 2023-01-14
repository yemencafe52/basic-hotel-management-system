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
    public partial class frmCustomerMgr : Form
    {
        public frmCustomerMgr()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;
            numericUpDown1.Value = Customer.GenerateNewCustomerNumber();
            comboBox1.SelectedIndex = 0;
            return res;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                return;
            }

            Customer customer = new Customer((byte)numericUpDown1.Value, textBox2.Text, (CardType)comboBox1.SelectedIndex, textBox3.Text, textBox3.Text);

            if(!(Customer.Add(customer)))
            {
                MessageBox.Show("تعذر تنفيذ العملية.");
                return;
            }

            this.Close();
        }
    }
}
