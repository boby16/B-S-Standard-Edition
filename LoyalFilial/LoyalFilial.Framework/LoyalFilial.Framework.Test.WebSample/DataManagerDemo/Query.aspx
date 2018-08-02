<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Query.aspx.cs" Inherits="LoyalFilial.Framework.Test.WebSample.DataManagerDemo.Query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <style type="text/css">
        body {
            background: #f5faff;
        }

        .demo_con {
            width: 960px;
            margin: 40px auto 0;
        }

        .button {
            width: 140px;
            line-height: 38px;
            text-align: center;
            font-weight: bold;
            color: #fff;
            text-shadow: 1px 1px 1px #333;
            border-radius: 5px;
            margin: 0 20px 20px 0;
            position: relative;
            overflow: hidden;
        }

            .button:nth-child(6n) {
                margin-right: 0;
            }

            .button.gray {
                color: #8c96a0;
                text-shadow: 1px 1px 1px #fff;
                border: 1px solid #dce1e6;
                box-shadow: 0 1px 2px #fff inset,0 -1px 0 #a8abae inset;
                background: -webkit-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: -moz-linear-gradient(top,#f2f3f7,#e4e8ec);
                background: linear-gradient(top,#f2f3f7,#e4e8ec);
            }



        .gray.round:after {
            box-shadow: 1px 0 1px rgba(255,255,255,1) inset,1px 0 1px rgba(0,0,0,.2);
            background: -webkit-linear-gradient(top,#dce1e6,#dde2e7);
            background: -moz-linear-gradient(top,#dce1e6,#dde2e7);
            background: linear-gradient(top,#dce1e6,#dde2e7);
            text-shadow: -2px 0 1px #fff;
        }




        .gray.tags:after {
            background: #8c96a0;
            border: 2px solid #fff;
        }



        .gray.rarrow:before {
            background: #d6dbe0;
        }

        .gray.rarrow:after {
            box-shadow: 0 1px 0 #fff inset,-1px 0 0 #b7babd inset;
            background: -webkit-linear-gradient(top left,#f2f3f7,#e4e8ec);
            background: -moz-linear-gradient(top left,#f2f3f7,#e4e8ec);
            background: linear-gradient(top left,#f2f3f7,#e4e8ec);
        }


        .gray.larrow:before {
            background: #d6dbe0;
        }

        .gray.larrow:after {
            box-shadow: 0 -1px 0 #b7babd inset,1px 0 0 #fff inset;
            background: -webkit-linear-gradient(top left,#f2f3f7,#e4e8ec);
            background: -moz-linear-gradient(top left,#f2f3f7,#e4e8ec);
            background: linear-gradient(top left,#f2f3f7,#e4e8ec);
        }



        .gray:hover {
            background: -webkit-linear-gradient(top,#fefefe,#ebeced);
            background: -moz-linear-gradient(top,#f2f3f7,#ebeced);
            background: linear-gradient(top,#f2f3f7,#ebeced);
        }



        .gray:active {
            top: 1px;
            box-shadow: 0 1px 3px #a8abae inset,0 3px 0 #fff;
            background: -webkit-linear-gradient(top,#e4e8ec,#e4e8ec);
            background: -moz-linear-gradient(top,#e4e8ec,#e4e8ec);
            background: linear-gradient(top,#e4e8ec,#e4e8ec);
        }



        .gray.side:hover:after {
            background: -webkit-linear-gradient(right,#e7ebee,#f8f8f8 60%);
            background: -moz-linear-gradient(right,#e7ebee,#f8f8f8 60%);
            background: linear-gradient(right,#e7ebee,#f8f8f8 60%);
        }



        .gray.side:active:after {
            top: 4px;
            border-top: 1px solid #9fa6ab;
            box-shadow: -1px 0 1px #a8abae inset;
            background: -webkit-linear-gradient(right,#e4e8ec,#e4e8ec 60%);
            background: -moz-linear-gradient(right,#e4e8ec,#e4e8ec 60%);
            background: linear-gradient(right,#e4e8ec,#e4e8ec 60%);
        }



        .gray.rarrow:hover:after,
        .gray.rarrow:hover:after {
            background: -webkit-linear-gradient(top left,#fefefe,#ebeced);
            background: -moz-linear-gradient(top left,#fefefe,#ebeced);
            background: linear-gradient(top left,#fefefe,#ebeced);
        }


        .gray.rarrow:active:after,
        .gray.larrow:active:after {
            background: -webkit-linear-gradient(top left,#e4e8ec,#e4e8ec);
            background: -moz-linear-gradient(top left,#e4e8ec,#e4e8ec);
            background: linear-gradient(top left,#e4e8ec,#e4e8ec);
        }


        .gray.rarrow:active:after {
            box-shadow: 0 1px 0 #b7babd inset,-1px 0 0 #b7babd inset;
        }

        .gray.larrow:active:after {
            box-shadow: 0 -1px 0 #b7babd inset,1px 0 0 #b7babd inset;
        }

       
    </style>
</head>



<body>
    <form id="form1" runat="server">
        <div class="page">
            <section class="demo">
                <asp:Button ID="btnQueryAll" runat="server" Text="QueryAll" OnClick="btnQueryAll_Click" class="" />
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                <br />
                Name:<asp:TextBox ID="txtName" runat="server" Width="108px" CssClass=""></asp:TextBox>
                <asp:Button ID="btnQueryByName" runat="server" Text="QueryByName" OnClick="btnQueryByName_Click" class="" />
                <asp:Label ID="lblResult1" runat="server" Text=""></asp:Label>
                <br />
                PageIndex:<asp:TextBox ID="txtPageIndex" runat="server" Width="57px"></asp:TextBox>
                PageSize:<asp:TextBox ID="txtPageSize" runat="server" Width="58px"></asp:TextBox>
                <asp:Button ID="btnQueryPage" runat="server" Text="QueryPage" OnClick="btnQueryPage_Click" class="" />
                <asp:Label ID="lblResult2" runat="server" Text=""></asp:Label>
            </section>
        </div>
    </form>
</body>
</html>
