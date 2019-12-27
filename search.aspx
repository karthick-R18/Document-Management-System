<%@ Page Title="Search | DMS" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="dms.search" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
    <h2>Search here!</h2>
    <table>
    <tr>
    <td>Search name</td>
    <td>
        <asp:TextBox ID="searchTextBox" runat="server" Height="30px" required="" class="form-control"></asp:TextBox></td></tr>
        
        <tr>        
        <td colspan="2">
            <asp:RadioButtonList ID="typeradiobutton" runat="server"
                DataValueField="answer" >
                <asp:ListItem class="form-control">File</asp:ListItem>  
                <asp:ListItem class="form-control">Folder</asp:ListItem> 
            </asp:RadioButtonList>
        </td></tr>
        <tr>
        <td colspan="2"><center>
            <asp:Button ID="searchbtn" runat="server" Text="Search" Height="36px" 
                onclick="searchbtn_Click" Width="76px" class="btn"/></center></td></tr>
        </table>
        <br />
    <asp:Panel ID="Panel1" runat="server">
    <hr />
    </asp:Panel>
    <br />
    </center>
</asp:Content>
