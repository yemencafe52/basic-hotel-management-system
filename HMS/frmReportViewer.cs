using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace HMS
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer(Microsoft.Reporting.WinForms.ReportDataSource rds,string rptName)
        {
            InitializeComponent();

            //reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.ReportEmbeddedResource = rptName;

            reportViewer1.LocalReport.DataSources.Add(rds);



        }

        public frmReportViewer(Microsoft.Reporting.WinForms.ReportDataSource rds, string rptName,string desc)
        {
            InitializeComponent();

            //reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.ReportEmbeddedResource = rptName;

            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("p1",Param.HotelName), new ReportParameter("p2", Param.HotelAddress), new ReportParameter("p3", Param.HotelPhone), new ReportParameter("p4", desc) });
            reportViewer1.LocalReport.DataSources.Add(rds);

        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
