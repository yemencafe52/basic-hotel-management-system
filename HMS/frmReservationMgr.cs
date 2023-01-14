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
    public partial class frmReservationMgr : Form
    {

        private Room room;
        public frmReservationMgr(Room room)
        {
            InitializeComponent();
            this.room = room;
            Preparing();
        }

        private bool Preparing()
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;

            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCustomerViewer fcv = new frmCustomerViewer(true);
            fcv.ShowDialog();

            if(fcv.CustomerInfo != null)
            {
                if(fcv.CustomerInfo.Number > 0)
                {
                    numericUpDown1.Value = fcv.CustomerInfo.Number;
                }
            }

            
        }

        private void comboBox1_TabIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(double.Parse(comboBox1.Text));
            double amount = double.Parse(comboBox1.Text) * Param.Price;
            numericUpDown2.Value = (decimal)amount;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer c = new Customer((byte)numericUpDown1.Value);

            if(!(c.Number > 0))
            {
                numericUpDown1.Focus();
                return;
            }

            if(numericUpDown3.Value != numericUpDown2.Value)
            {
                numericUpDown3.Focus();
                return;
            }

            Resevation r = new Resevation(0, dateTimePicker1.Value, dateTimePicker2.Value, c, (double)numericUpDown3.Value, "", User.ActiveUser, State.State2);
            
            if (!(ReservationsManager.ChangeRoomState(this.room, r)))
            {
                MessageBox.Show("تعذر تنفيذ العملية");
                return;
            }

            this.Close();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Customer c = new Customer((byte)numericUpDown1.Value);

            if(c.Number > 0)
            {
                textBox2.Text = c.Name;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;

            }
            else
            {
                textBox2.Text = "";
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
            }


        }
    }
}
