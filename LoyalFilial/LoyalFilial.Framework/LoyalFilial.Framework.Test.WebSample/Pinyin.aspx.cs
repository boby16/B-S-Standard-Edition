using System;
using System.Diagnostics;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Framework.Test.WebSample
{
    public partial class Pinyin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            //this.tbPinyin.Text = string.Format("{0} || {1}", NameHelper.Convert(this.tbHz.Text), NameHelper.ConvertToInitial(this.tbHz.Text));
            //this.tbPinyin.Text = ConvertCityName(this.tbHz.Text);
            TestForm(Request.Form);
            //LFFK.DataManager.TableQuery<GisCityDO>().Select().From().Where(c => c.id == 1).Execute();
        }

        private void TestForm(System.Collections.Specialized.NameValueCollection collection)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var c = LFFK.DataManager.Map<CityDO>(collection);
            sw.Stop();
            Response.Write(sw.Elapsed.TotalMilliseconds);

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            var cc = new CityDO();
            cc.CityId = Convert.ToInt32(this.CityDO_CityId.Text);
            cc.CityName = this.CityDO_CityName.Text;
            cc.Level = Convert.ToInt32(this.CityDO_Level.Text);
            sw1.Stop();
            Response.Write("<BR/>");
            Response.Write(sw1.Elapsed.TotalMilliseconds);

            if (c.IsSucceed)
                this.tbPinyin.Text = c.Data.CityName;
        }

        protected void btnInitCity_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var c in LFFK.DataManager.TableQuery<GisCityDO>().Select().From().ExecuteList())
                {
                    LFFK.DataManager.Insert<CityDO>(new CityDO()
                    {
                        CityId = c.id,
                        CityName = ConvertCityName(c.name),
                        FullName = c.name,
                        EnglishName=c.e_name,
                        Level = c.level,
                        ParentId = c.upid,
                        Sequence = 1,
                        SingleSpellCode = NameHelper.ConvertToInitial(c.name),
                        SpellCode = NameHelper.Convert(c.name),
                        LngCode=c.lng,
                        LatCode=c.lat
                    }
                        );
                }
                Response.Write("<BR/>完成！"); 
            }
            catch (Exception ex)
            {

                this.tbPinyin.Text = ex.ToString();
            }
        }

        private string ConvertCityName(string name)
        {
            if (name.EndsWith("市"))
                return name.Substring(0, name.Length - 1);
            if (name.EndsWith("省"))
                return name.Substring(0, name.Length - 1);
            return name;
        }
    }

    /// <summary>
    /// 实体类 Common_City, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("biz_dev", "Common_City")]
    public class CityDO
    {

        #region 实体属性

        /// <summary>
        /// 城市Id
        /// </summary>
        [Column("CityId", true)]
        public int CityId { get; set; }

        /// <summary>
        /// 城市简称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 官方全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 英文名字
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 节点层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 首字母拼音
        /// </summary>
        public string SingleSpellCode { get; set; }

        /// <summary>
        /// 全拼
        /// </summary>
        public string SpellCode { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string LngCode { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string LatCode { get; set; }

        #endregion

    }

    /// <summary>
    /// 实体类 Gis_GisCity, 此类请勿动（工具生成）
    /// </summary>
    [Serializable]
    [Table("biz_dev", "Gis_GisCityFull")]
    public class GisCityDO
    {

        #region 实体属性

        /// <summary>
        /// id
        /// </summary>
        [Column("id", true)]
        public int id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// englishname
        /// </summary>
        public string e_name { get; set; }

        /// <summary>
        /// level
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// upid
        /// </summary>
        public int upid { get; set; }

        /// <summary>
        /// upid
        /// </summary>
        public string lat { get; set; }

        /// <summary>
        /// upid
        /// </summary>
        public string lng { get; set; }

        #endregion

    }
}