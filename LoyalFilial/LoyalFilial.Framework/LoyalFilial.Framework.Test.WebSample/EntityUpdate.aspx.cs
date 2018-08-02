using System.Data;
using System.Diagnostics;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoyalFilial.Framework.Test.WebSample
{
    public partial class EntityUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //LFFK.DataManager.TableQuery<CqsEntity2>()
            //    .Select()
            //    .From()
            //    .Where()
            //    .OrderByAsc(c => c.No, c => c.Age).OrderByDesc(c => c.BoardTime)
            //    .ExecuteList();

            //var entity = LFFK.DataManager.TableQuery<CqsEntity2>().Select().From().Where(t => t.Age == 52).Execute();

            //LFFK.DataManager.DataProvider.GetConnectionString("mysqlTest2");
            //var entity2 = LFFK.DataManager.TableQuery<CqsEntity2>().Select().From().Where(t => t.Age == 52).Execute();
            //entity.Age += 10;
            ////update
            //var r = LFFK.DataManager.Update<CqsEntity2>(entity);
            //Response.Write("update overed...");
            ////delete
            //LFFK.DataManager.Delete<CqsEntity2>(entity);
            //Response.Write("delete overed...");
            //LFFK.DataManager.Insert<CqsEntity2>(entity);
            //Response.Write("Insert overed...");

            //CqsEntity cqs = new CqsEntity();
            //Stopwatch s1 = new Stopwatch();
            //s1.Start();
            //var result = cqs.Age.GetType() == typeof (string);
            //s1.Stop();
            //Response.Write(result.ToString() + " || " + s1.Elapsed.TotalMilliseconds.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.Label1.Text = (StringHelper.IsForbiddenSql(this.tbInput.Text) || StringHelper.HasIdenticallyEqual(this.tbInput.Text)).ToString();
            var list = PrepareData();
            LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, "select * from cqs_test");
            //int totalCount = 0;
            //var list =
            //    LFFK.DataManager.TableQuery<CqsEntity>()
            //        .Select()
            //        .From()
            //        .Where()
            //        .OrderByAsc()
            //        .ExecuteList(Convert.ToInt32(this.tbInput.Text), 1, out totalCount);
            Stopwatch s = new Stopwatch();
            s.Start();
            //var target = LFFK.PermissionManager.GetAccessList<AccessPO>("1001", "kane");
            foreach (var cqsEntity in list)
            {
                //var sql =
                //    string.Format(
                //        "INSERT INTO `cqs_test` (`UpdateTime`, `OP`, `BoardTime`, `Age`, `Description`, `Name`) VALUES ('{0}', '{1}', '{2}', {3}, '{4}', '{5}');",
                //        cqsEntity.UpdateTime,
                //        cqsEntity.OP,
                //        cqsEntity.BoardTime,
                //        cqsEntity.Age,
                //        cqsEntity.Description,
                //        cqsEntity.Name);
                //LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, sql);
                LFFK.DataManager.Insert(cqsEntity);

                //var sql = string.Format("update `cqs_test` set `OP`='{0}' where idx={1} ;", "AAA", cqsEntity.No);
                //LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, sql);
            }
            s.Stop();

            Stopwatch s2 = new Stopwatch();
            s2.Start();
            //string sqlbatch = "INSERT INTO `cqs_test` (`UpdateTime`, `OP`, `BoardTime`, `Age`, `Description`, `Name`) VALUES";
            //for (int i = 0; i < list.Count; i++)
            //{
            //    sqlbatch +=
            //        string.Format(
            //            "('{0}', '{1}', '{2}', {3}, '{4}', '{5}')",
            //            list[i].UpdateTime,
            //            list[i].OP,
            //            list[i].BoardTime,
            //            list[i].Age,
            //            list[i].Description,
            //            list[i].Name);
            //    if (i == list.Count - 1)
            //        sqlbatch += ";";
            //    else
            //    {
            //        sqlbatch += ",";
            //    }
            //}
            //LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, sqlbatch);
            var result = LFFK.DataManager.Insert(list);

            //string sqlbatch = "";
            //foreach (var cqsEntity in list)
            //{
            //    sqlbatch += string.Format("update `cqs_test` set `OP`='{0}' where idx={1} ;", "BBB", cqsEntity.No);
            //}
            //sqlbatch = "START TRANSACTION; " + sqlbatch + " COMMIT;";
            //LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, sqlbatch);
            s2.Stop();

            //Stopwatch s3 = new Stopwatch();
            //s3.Start();
            ////string sqlBatchIU = "START TRANSACTION; INSERT INTO `cqs_test` (`OP`, `idx`) VALUES";
            ////for (int i = 0; i < list.Count; i++)
            ////{
            ////    sqlBatchIU +=
            ////        string.Format(
            ////            "('{0}', {1})",
            ////            "DDD",
            ////            list[i].No);
            ////    if (i < list.Count - 1)
            ////    {
            ////        sqlBatchIU += ",";
            ////    }
            ////}
            ////sqlBatchIU += " ON DUPLICATE KEY UPDATE OP=VALUES(op); COMMIT; ";
            ////LFFK.DataManager.DataProvider.ExecuteNonQuery(CommandType.Text, sqlBatchIU);
            //var result = LFFK.DataManager.Update<CqsEntity>(list);
            //s3.Stop();

            //this.Label1.Text = s.Elapsed.TotalMilliseconds.ToString() + " VS " + s2.Elapsed.TotalMilliseconds.ToString() +
            //                   " VS " + s3.Elapsed.TotalMilliseconds.ToString();

            this.Label1.Text = s.Elapsed.TotalMilliseconds.ToString() + " VS " + s2.Elapsed.TotalMilliseconds.ToString();
            //Response.Write(result.ErrorMsg);
        }

        private List<CqsEntity> PrepareData()
        {
            var result = new List<CqsEntity>();
            for (int i = 0; i < Convert.ToInt32(this.tbInput.Text); i++)
            {
                result.Add(new CqsEntity()
                {
                    Age = i,
                    BoardTime = DateTime.Now,
                    Description = "performence test",
                    Name = "alex",
                    No = i,
                    OP = "System",
                    UpdateTime = DateTime.Now
                });
            }
            return result;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var list =
                LFFK.DataManager.TableQuery<CqsEntity>()
                    .Select()
                    .From()
                    .Where(p => p.Name.Contains("cqs"))
                    .ExecuteList();

            var list2 =
                LFFK.DataManager.TableQuery<CqsEntity>()
                    .Select()
                    .From()
                    .Where(p => p.Age > 18 && p.Name.Contains("cqs"))
                    .ExecuteList();

            Response.Write(list.Count.ToString() + "&&" + list2.Count.ToString());
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var list =
                LFFK.DataManager.TableQuery<TaoUserID>()
                    .Select()
                    .From()
                    .Where(c=>c.VCount>=10)
                    .ExecuteList();
            var result = string.Empty;
            foreach (var user in list)
            {
                //var arr = user.UserId.Trim().Split(" ".ToCharArray());
                //user.VCount = Convert.ToInt32(arr[0]);
                //user.Vid = arr[1];
                //LFFK.DataManager.Update(user);
                result += user.Vid;
                result += "|";
            }
            tbTaoUser.Text = result;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var list =
                LFFK.DataManager.TableQuery<OnlineProduct>()
                    .Select()
                    .From()
                    .ExecuteList();
            var result = string.Empty;
            foreach (var user in list)
            {
                result += user.ProductId;
                result += "|";
            }
            tbTaoUser.Text = result;
        }
    }
}