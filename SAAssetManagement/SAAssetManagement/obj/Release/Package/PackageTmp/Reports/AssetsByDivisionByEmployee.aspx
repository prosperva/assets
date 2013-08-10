<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true" CodeBehind="AssetsByDivisionByEmployee.aspx.cs" Inherits="SAAssetManagement.Reports.AssetsByDivisionByEmployee" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<form id="form1" runat="server">
    <div>
<asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
            Division Name:
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
         &nbsp;User:
         &nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
        
            <asp:Button ID="Button1" runat="server" Text="Generate Report" 
            onclick="Button1_Click" CssClass="btn btn-primary" />
            &nbsp;&nbsp;
        </div>
        <br /><br />
        <asp:Label ID="Label1" runat="server" Text="Label" CssClass="alert alert-info"></asp:Label>
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
    </rsweb:ReportViewer>
        <br />
       
    </div>
    </form>
</asp:Content>
