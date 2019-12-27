<%@ Page Title="File explorer | DMS" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="explorer.aspx.cs" Inherits="dms.explorer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
<h3>Explore your files here!</h3>
</center>
    <asp:Button ID="backbtn" runat="server" Text="back" class="btn"
    style="margin-left:60px;" onclick="backbtn_Click"/>
    <center>
<div ID="createfolder" style="text-align:right;margin-right:20px;">
    <asp:TextBox ID="newfoldertextbox" runat="server" class="" Height="29px" 
        Width="171px" Placeholder="New folder name"></asp:TextBox>
    &nbsp;
    <asp:Button ID="createfolderbtn" runat="server" Text="Create new folder" 
        onclick="createfolderbtn_Click" class="btn"/>
</div> 

<br />
    <asp:Table ID="Table1" runat="server" BorderStyle="Solid" BorderWidth="2px" 
        CellPadding="10" CellSpacing="10" GridLines="Both" Height="400px" 
        Width="700px" class="table table-hover">
       
    </asp:Table>
    <br /><br />
</center>
</asp:Content>
