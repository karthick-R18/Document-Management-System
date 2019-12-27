<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fileExplorer.aspx.cs" Inherits="DmsTest.fileExplorer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Go back" />
     <asp:GridView ID="GridView2" runat="server">
        
    </asp:GridView>
  
    <asp:GridView ID="GridView1" runat="server" onrowdatabound="GridView1_RowDataBound" 
            onselectedindexchanging="GridView1_SelectedIndexChanging">
        <Columns>
            <asp:CommandField HeaderText="Open" ShowHeader="True" ShowSelectButton="True" />
        </Columns>
        
    </asp:GridView>
      </div>
    </form>
</body>
</html>
