using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SAAssetManagement.Reports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Report rpt = new Report();
                ReportViewer rptViewer = ReportViewer1;
                Connections.Connections conn = new Connections.Connections();
                string commandText = "GetAssetSoftwares";
                bool r = rpt.ShowReport(rptViewer, "DataSet1", "Reports/Report Files/Report1.rdlc", commandText, 2, conn.MainConnection());

                if (r)
                {
                    rptViewer.Visible = true;
                    rptViewer.SizeToReportContent = true;
                    rptViewer.ZoomMode = ZoomMode.FullPage;
                }
                else
                {
                    rptViewer.Visible = false;
                    Response.Write("No records were found.");
                }

            }
        }
    }
}