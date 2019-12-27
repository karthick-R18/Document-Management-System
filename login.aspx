<%@ Page Title="Login page | DMS" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="dms.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <h3>
        Login page</h3>
    <asp:Label ID="errorlabel" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
    <table cellpadding="20">
        <tr>
            <td>
                Username
            </td>
            <td>
                <asp:TextBox ID="usernameTextBox" runat="server" required="" Height="24px" Width="169px" class="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Password
            </td>
            <td>
                <asp:TextBox ID="passwordTextBox" runat="server" required="" TextMode="Password"
                    Height="24px" Width="169px" class="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <center>
                    <asp:Button ID="loginbtn" runat="server" Text="Login" Height="33px" Width="76px"
                        OnClick="loginbtn_Click" class="btn"/></center>
            </td>
            <td>
                <center>
                    <input type="reset" value="Clear" style="height: 33px; width: 76px;" class="btn"/></center>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
