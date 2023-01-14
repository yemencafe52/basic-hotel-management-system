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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            Preparing();
        }


        private bool Preparing()
        {
            bool res = false;

            textBox1.Text = Param.HotelName;
            textBox2.Text = Param.HotelAddress;
            textBox3.Text = Param.HotelPhone;

            numericUpDown1.Value = Param.RoomCount;
            numericUpDown2.Value = (decimal)Param.Price;

            return res;
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {

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


            if(!Param.Update(textBox1.Text, textBox2.Text, textBox3.Text, (byte)numericUpDown1.Value, (double)numericUpDown2.Value))
            {
                MessageBox.Show("تعذر تنفيذ العملية");
                return;
            }

            MessageBox.Show("يجب إعادة تشغيل النظام.");
            this.Close();
        }
    }
}
