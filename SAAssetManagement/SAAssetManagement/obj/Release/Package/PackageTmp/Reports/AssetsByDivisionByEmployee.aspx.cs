using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAAssetManagement.Models;
using Microsoft.Reporting.WebForms;
using SAAssetManagement.Reports.Connections;

namespace SAAssetManagement.Reports
{
    public partial class AssetsByDivisionByEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AssetContext db = new SAAssetManagement.Models.AssetContext();
                var divisions = db.Divisions;

                foreach (var div in divisions)
                {
                    DropDownList1.Items.Add(new ListItem(div.DivisionName, div.DivisionID.ToString()));
                }
                DropDownList1.Items.Insert(0, new ListItem("---All Divisions---", ""));
                DropDownList1.SelectedIndex = 0;

                var users = from c in db.Employees select new {employeeid = c.EmployeeID,FullNames = c.EmployeeFirstName + " " + c.EmployeeLastName };

                foreach (var user in users)
                {
                    DropDownList2.Items.Add(new ListItem(user.FullNames, user.employeeid.ToString()));
                }
                DropDownList2.Items.Insert(0, new ListItem("---All Users---", ""));
                DropDownList2.SelectedIndex = 0;

                Label1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ReportParam[] reportParamsArray = new ReportParam[2]
            //{
            //  new ReportParam("divisionid",DropDownList1.SelectedValue),
            //  new ReportParam("employeeid",DropDownList2.SelectedValue),
            //};
            //Report rpt = new Report();

            //if (rpt.ShowReport(ReportViewer1, "DataSet1", "sp_getAssetsByEmployee", "Reports/Report Files/UserAssetsDivision.rdl", reportParamsArray))
            //{
            //    Label1.Visible = false;
            //}
            //else
            //{
            //    Label1.Text = "There were no records found for the search criteria you provided...";
            //    Label1.Visible = true;
            //}
            ReportParam[] reportParamsArray = new ReportParam[2]
            {
              new ReportParam("divisionid",DropDownList1.SelectedValue),
              new ReportParam("employeeid",DropDownList2.SelectedValue),
            };
            Report rpt = new Report();
            ReportViewer rptViewer = ReportViewer1;
            Connections.Connections conn = new Connections.Connections();
            string commandText = "sp_getAssetsByEmployee";
            bool r = rpt.ShowReport(rptViewer, "DataSet1", "Reports/Report Files/UserAssetsDivision.rdl", commandText, 2, conn.MainConnection(),reportParamsArray);

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