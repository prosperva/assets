using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SAAssetManagement.Reports
{
    public class Report
    {
        //public bool ShowReport(ReportViewer rptViewer,
        //                        string dsName,
        //                        string strStoredProcedure,
        //                        string strReportPath,
        //                        ReportParam[] reportParamsArray)
        //{

        //    rptViewer.Visible = true;
        //    rptViewer.SizeToReportContent = true;
        //    rptViewer.ZoomMode = ZoomMode.PageWidth;

        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataAdapter sqla = new SqlDataAdapter();
        //    DataTable dt = new DataTable();
        //    ReportDataSource rds = new ReportDataSource();
        //    List<ReportParameter> rptParams = new List<ReportParameter>();

        //    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SAAssetManagement.Models.AssetContextConnectionString"].ConnectionString);
           
        //    cmd.Connection = myConnection;
        //    cmd.CommandText = strStoredProcedure;
        //    cmd.CommandType = CommandType.StoredProcedure;
            
        //    foreach (var p in reportParamsArray)
        //    {
        //        if (p.ParamValue.ToString() == "")
        //        {
        //            cmd.Parameters.AddWithValue(p.ParamName, System.DBNull.Value);
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue(p.ParamName, p.ParamValue);
        //        }
        //    }

        //    sqla.SelectCommand = cmd;
        //    sqla.Fill(dt);
            
        //    rptViewer.LocalReport.ReportPath = strReportPath;
        //    rptViewer.LocalReport.DataSources.Clear();
        //    rds.Name = dsName;
        //    rds.Value = dt;
        //    rptViewer.LocalReport.DataSources.Add(rds);

        //    foreach (var p in reportParamsArray)
        //    {
        //        if (p.ParamValue.ToString() == "")
        //        {
        //            rptParams.Add(new ReportParameter(p.ParamName));
        //        }
        //        else
        //        {
        //            rptParams.Add(new ReportParameter(p.ParamName, p.ParamValue.ToString()));
        //        }
        //    }

        //    rptViewer.LocalReport.SetParameters(rptParams);

        //    if (dt.Rows.Count == 0)
        //    {
        //        rptViewer.Visible = false;
        //        return false;
        //    }
        //    else
        //    {
        //        rptViewer.LocalReport.Refresh();
        //        return true;
        //    }

        //}
        public bool ShowReport(ReportViewer rptViewer,
                                string dsName,
                                string strReportPath,
                                string commandText,
                                int commandType,
                                SqlConnection conn,
                                ReportParam[] reportParamsArray = null)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            DataTable dtTable = new DataTable();
            ReportDataSource rds = new ReportDataSource();
            List<ReportParameter> rptParams = new List<ReportParameter>();

            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType == 1 ? CommandType.Text : CommandType.StoredProcedure;

            if (reportParamsArray != null)
            {
                foreach (var p in reportParamsArray)
                {
                    if (p.ParamValue.ToString() == "")
                    {
                        cmd.Parameters.AddWithValue(p.ParamName, System.DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(p.ParamName, p.ParamValue);
                    }
                }
            }

            sqlAdapter.SelectCommand = cmd;
            sqlAdapter.Fill(dtTable);

            rptViewer.LocalReport.ReportPath = strReportPath;
            rptViewer.LocalReport.DataSources.Clear();

            rds.Name = dsName;
            rds.Value = dtTable;
            rptViewer.LocalReport.DataSources.Add(rds);

            if (reportParamsArray != null)
            {
                foreach (var p in reportParamsArray)
                {
                    if (p.ParamValue.ToString() == "")
                    {
                        rptParams.Add(new ReportParameter(p.ParamName));
                    }
                    else
                    {
                        rptParams.Add(new ReportParameter(p.ParamName, p.ParamValue.ToString()));
                    }
                }
            }

            rptViewer.LocalReport.SetParameters(rptParams);

            if (dtTable.Rows.Count == 0)
            {
                rptViewer.Visible = false;
                return false;
            }
            else
            {
                rptViewer.LocalReport.Refresh();
                return true;
            }

        }
    }
}