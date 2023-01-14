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
    public partial class frmCustomerViewer : Form
    {

        private bool forSelection = false;
        private Customer customer = null;

        public Customer CustomerInfo
        {
            get
            {
                return this.customer;
            }
        }

        public frmCustomerViewer(bool fs)
        {
            InitializeComponent();
            forSelection = fs;
            Preparing();
        }

        public frmCustomerViewer()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            Display(Customer.GetALL());
            return true;
        }

        private void Display(List<Customer> customers)
        {
            listView1.Items.Clear();

            for(int i=0;i < customers.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(customers[i].Number.ToString());
                lvi.SubItems.Add(customers[i].Name);
                listView1.Items.Add(lvi);
            }

            toolStripStatusLabel2.Text = customers.Count.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmCustomerMgr fcm = new frmCustomerMgr();
            fcm.ShowDialog();
            Display(Customer.GetALL());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Display(Customer.Search(toolStripTextBox1.Text));
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(forSelection)
            {
                if(listView1.SelectedItems.Count >0)
                {
                    this.customer = new Customer(int.Parse(listView1.Items[listView1.SelectedItems[0].Index].Text));
                    this.Close();
                }
            }
        }
    }
}
