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
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {

            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dateTimePicker2.Value = DateTime.Now;

            comboBox1.Items.Add("تفصيلي");
            comboBox1.Items.Add("إجمالي");
            comboBox1.SelectedIndex = 1;
            comboBox1.Enabled = false;

            comboBox2.Items.Add(User.ActiveUser.UserName);
            comboBox2.SelectedIndex = 0;
            comboBox2.Enabled = false;
            return true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        DataTable dt = (DataTable)Report.Report1(dateTimePicker1.Value, dateTimePicker2.Value);

                        if(!(dt.Rows.Count > 0 ))
                        {
                            MessageBox.Show("لا توجد نتائج لعرضها.");
                            return;
                        }

                        string desc = "للفترة تبدأ من " + dateTimePicker1.Value.ToString("yyyy/MM/dd") + " وتنتهي إلى " + dateTimePicker2.Value.ToString("yyyy/MM/dd");
                        frmReportViewer fr = new frmReportViewer(new Microsoft.Reporting.WinForms.ReportDataSource("tblTempReservations",dt) , "HMS.rptTotalTransction.rdlc",desc);
                        fr.Show();
                        break;
                    }
            }
        }
    }
}
