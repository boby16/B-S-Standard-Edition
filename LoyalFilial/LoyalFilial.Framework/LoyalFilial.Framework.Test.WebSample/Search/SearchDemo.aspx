<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchDemo.aspx.cs" Inherits="LoyalFilial.Framework.Test.WebSample.Search.SearchDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TextBox ID="txtId" runat="server" Width="517px"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="AddIndex" OnClick="btnAdd_Click" />
            <br />
            <asp:TextBox ID="txtProductDescription" runat="server" Width="517px"></asp:TextBox>
            <asp:Button ID="btnProductDescription" runat="server" Text="UpdateProductDescription" OnClick="btnProductDescription_Click" />
            <br />
            <asp:TextBox ID="txtCondition" runat="server" Width="517px"></asp:TextBox>
            <asp:Button ID="btnDelte" runat="server" Text="Delete" OnClick="btnDelte_Click" />
            <br />

        </div>
    </form>
</body>
</html>
