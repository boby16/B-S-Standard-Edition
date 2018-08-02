<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityUpdate.aspx.cs" Inherits="LoyalFilial.Framework.Test.WebSample.EntityUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="tbInput" runat="server" Width="449px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Click to see if injected:" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Contain Test" />
        
        <asp:Button ID="Button3" runat="server" Text="TaoUser" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="OnlineProduct" />
        <br />
        <asp:TextBox ID="tbTaoUser" runat="server" Height="698px" TextMode="MultiLine" Width="442px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
