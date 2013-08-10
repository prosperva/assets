<%@ Page Title="Workstation Assets" Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true" CodeBehind="WorkstationAsset.aspx.cs" Inherits="SAAssetManagement.Reports.WorkstationAsset" %>
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
         &nbsp;<asp:Button ID="Button1" runat="server" Text="Generate Report" 
            onclick="Button1_Click" CssClass="btn btn-primary" />
            &nbsp;&nbsp;
        </div>
        <br /><br />
        <asp:Label ID="Label1" runat="server" Text="Label" CssClass="alert alert-info"></asp:Label>
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
            Font-Size="8pt" Height="750px" InteractiveDeviceInfos="(Collection)"
            ShowBackButton="true" ShowCredentialPrompts="False"
            ShowDocumentMapButton="False" ShowFindControls="true"
            ShowParameterPrompts="False" ShowPromptAreaButton="False"
            ShowRefreshButton="true" WaitMessageFont-Names="Verdana"
            WaitMessageFont-Size="14pt" Width="800px"
            ShowWaitControlCancelLink="False" ShowPageNavigationControls="true"
            ShowZoomControl="true">
        </rsweb:ReportViewer>
        <br />
    </div>
    </form>
</asp:Content>
