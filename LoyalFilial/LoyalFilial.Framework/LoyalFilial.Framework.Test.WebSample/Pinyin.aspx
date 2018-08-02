<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pinyin.aspx.cs" Inherits="LoyalFilial.Framework.Test.WebSample.Pinyin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="tbHz" runat="server" Height="150px" Width="300px"></asp:TextBox>
        <asp:Button ID="btnConvert" runat="server" Text="Pinyin >>>" OnClick="btnConvert_Click" />
        <asp:TextBox ID="tbPinyin" runat="server" Height="150px" Width="300px"></asp:TextBox>
    
        <br />
    
        <br />
        <asp:Button ID="btnInitCity" runat="server" Text="Here we go init city list（Warning to click）……" OnClick="btnInitCity_Click" Width="710px" />
    
        <br />
        <br />
        Test:<br />
        <asp:TextBox ID="CityDO_CityId" runat="server" Text="100"></asp:TextBox>
        <asp:TextBox ID="CityDO_CityName" runat="server" Text="shanghai"></asp:TextBox>
        <asp:TextBox ID="CityDO_Level" runat="server" Text="5"></asp:TextBox>
        <br />
    
    </div>
    </form>
</body>
</html>
