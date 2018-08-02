using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.MailDemo
{
    public partial class SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            // LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Execute();
            string msg = @"<html><!--if you cannot see images below, go here: http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593 -->
<head>
<title>Colonize: 4 Harry Potter books</title>
</head>

<body bgcolor=""#FFFFFF""><!-- colonize header -->
<CENTER>
<TABLE>
<TBODY>
<TR>
<TD>
-- You signed up to receive special offers at Colonize.com -- 

</TD></TR></TBODY></TABLE></CENTER>
<!-- colonize header -->
<center>
<TABLE width=""500"" cellSpacing=""0"" cellPadding=""0"" border=""0"">
<tr>
<td valign=""top"" align=""center""><font face=""arial, helvetica"" size=""4"" color=""#000000""><b>Xingchi, join and get all <font face=""Palatino Linotype"" size=""6"" color=""#FF0000""><b>4<br>Harry Potter</b></font> books </b><font face=""Palatino Linotype"" size=""6"" color=""#FF0000""><b>FREE!</b></font> &#40;&#43; s&#47;h&#41;</font></td>
</tr>
<tr>
<td valign=""top"" align=""center"" height=""1""></td>
</tr>
</Table>
<TABLE width=""450"" borderColor=""#a0b8c8"" cellSpacing=""0"" cellPadding=""5"" border=""0"">
<tr>
<td width=""25%"" align=""center""><FONT face=""arial, helvetica"" size=""-1""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><img src=""http://www.colonize.com/htmlimages/01252bhdx1hptrss2.jpg"" width=""85"" height=""119"" border=""0""></a><br>The Sorcerer's Stone<br></FONT></td>

<td width=""25%"" align=""center""><FONT face=""arial, helvetica"" size=""-1""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><img src=""http://www.colonize.com/htmlimages/030408bmcx/hpotter2-md.jpg"" width=""85"" height=""119"" border=""0""></a><br>The Chamber<br>of Secrets<br></FONT></td>

<td width=""25%"" align=""center""><FONT face=""arial, helvetica"" size=""-1""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><img src=""http://www.colonize.com/htmlimages/030408bmcx/hpotter3-md.jpg"" width=""85"" height=""119"" border=""0""></a><br>The Prisoner<br>of Azkaban<br></FONT></td>

<td width=""25%"" align=""center""><FONT face=""arial, helvetica"" size=""-1""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><img src=""http://www.colonize.com/htmlimages/030408bmcx/hpotter4-md.jpg"" width=""85"" height=""119"" border=""0""></a><br>The Goblet<br>of Fire<br></FONT></td>
</tr>
</table>
<TABLE width=""500"" borderColor=""#a0b8c8"" cellSpacing=""0"" cellPadding=""5"" border=""0"">
<tr>
<td valign=""middle"" align=""center""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><font face=""arial, helvetica"" size=""5"" color=""blue""><b>No Credit Card Needed!</b></font></a><font face=""arial, helvetica"" size=""4"" color=""#000000""> (subject to approval)</font>
<br><br><font face=""arial, helvetica"" size=""5"" color=""#000000""><b>Or</b> pick <u>any</u> other 4 books for</font><font face=""arial, helvetica"" size=""6"" color=""red""><b> FREE!</b></font></td>
</tr>
</Table>
<TABLE width=""500"" borderColor=""#a0b8c8"" cellSpacing=""0"" cellPadding=""0"" border=""0"">
<tr>
<td valign=""middle"" align=""center"" height=""5""></td>
</tr>
<tr>
<td valign=""middle"" align=""center""><a href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><img src=""http://www.colonize.com/htmlimages/02282pvcclick.gif"" width=""143"" height=""40"" border=""0""></a></td>
</tr>
</Table>
<TABLE width=""500"" cellSpacing=""0"" cellPadding=""0"" border=""0"">
<tr>
<td align=""center""><br><A href=""http://www.colonize.com/c.php3?i=bmcx2,05143,x1&e=33668593""><font face=""arial, helvetica"" size=""6"" color=""#FF0000"">Click for membership details!</FONT></a></TD>
</tr>
</Table>
</center>
<!-- bmcx2html.tmp -->
<!-- colonize footer -->
<CENTER>
<TABLE>
<TBODY>
<TR>
<TD>
<HR>
Questions? Comments? Email us at: info@colonize.com
<BR>You are currently subscribed as: xiaoxingchi@cn99.com
<BR>To unsubscribe visit:
<BR>http://www.colonize.com/u.php3?e=xiaoxingchi@cn99.com
<HR>
</TD></TR></TBODY></TABLE></CENTER>

<!-- colonize footer --></body>
</html>";


            LFFK.MailManager.Send(false, "xiaoxcfei@126.com", "h.pei@163.com", "xiaofei@117go.com", "Test1", "测试测试啊", false);

        }
    }
}