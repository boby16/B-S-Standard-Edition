<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="LoyalFilial.Framework.Test.WebSample.MailDemo.SendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnSendMail" runat="server" Text="SendMail" OnClick="btnSendMail_Click" />
    </div>
    </form>
</body>
</html>
