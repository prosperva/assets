using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using SAAssetManagement.Models;

namespace SAAssetManagement.Reports
{
    public partial class AssetsByDivisionByType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AssetContext db = new SAAssetManagement.Models.AssetContext();
                var divisions = db.Divisions;

                foreach (var div in divisions) {
                    DropDownList1.Items.Add(new ListItem(div.DivisionName,div.DivisionID.ToString()));
                }
                DropDownList1.Items.Insert(0, new ListItem("---All Divisions---", ""));
                DropDownList1.SelectedIndex = 0;

                var assetTypes = db.AssetTypes;

                foreach (var assetType in assetTypes)
                {
                    DropDownList2.Items.Add(new ListItem(assetType.AssetTypeName, assetType.AssetTypeID.ToString()));
                }
                DropDownList2.Items.Insert(0, new ListItem("---All Asset Types---", ""));
                DropDownList2.SelectedIndex = 0;

                Label1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ReportParam[] reportParamsArray = new ReportParam[2]
            //{
            //  new ReportParam("divisionid",DropDownList1.SelectedValue),
            //  new ReportParam("assettypeid",DropDownList2.SelectedValue),
            //};
            //Report rpt = new Report();

            //if (rpt.ShowReport(ReportViewer1, "DataSet1", "sp_getAssetsByDivision", "Reports/Report Files/AssetsByDivision.rdl", reportParamsArray))
            //{
            //    Label1.Visible = false;
            //}
            //else {
            //    Label1.Text = "There were no records found for the search criteria you provided...";
            //    Label1.Visible = true;
            //}
            ReportParam[] reportParamsArray = new ReportParam[2]
            {
              new ReportParam("divisionid",DropDownList1.SelectedValue),
              new ReportParam("assettypeid",DropDownList2.SelectedValue),
            };
            Report rpt = new Report();
            ReportViewer rptViewer = ReportViewer1;
            Connections.Connections conn = new Connections.Connections();
            string commandText = "sp_getAssetsByDivision";
            bool r = rpt.ShowReport(rptViewer, "DataSet1", "Reports/Report Files/AssetsByDivision.rdl", commandText, 2, conn.MainConnection(), reportParamsArray);

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