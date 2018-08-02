using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyNet.Solr;
using EasyNet.Solr.Commons;
using LoyalFilial.Framework.Search;

namespace LoyalFilial.Framework.Test.WebSample.Search
{
    public partial class SearchDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtId.Text))
            {
                List<Product> productList = new List<Product>
                {
                    new Product
                    {
                        Coords = new[] { "31.111,121.222", "41.222,121.111" }, 
                        EffectDate = DateTime.Now.AddDays(-20),
                        ExpireDate = DateTime.Now.AddMonths(2),
                        Id = Convert.ToInt32(this.txtId.Text), 
                        ProductAttributeId = 1,
                        ProductDescription = "测试产品描述"+this.txtId.Text,
                        ProductName = "产品测试名称"+this.txtId.Text, 
                        ProductTypeId = 1, 
                        Rank = 1,
                        TourSpotName = new[] { "西湖", "杭州" } , 
                        SalesPrice = 20.1
                    }
                };
                Framework.Search.Search.SearchManager.SaveIndex<Product>("core_prod", productList, new ProductSerializer());
            }
        }

        protected void btnProductDescription_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtProductDescription.Text))
            {
                Framework.Search.Search.SearchManager.AtomOperateIndex("core_prod", "id", 1, new Dictionary<string, object> { { "ProductDescription", this.txtProductDescription.Text } });
            }
        }

        protected void btnDelte_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCondition.Text))
            {
                Framework.Search.Search.SearchManager.Delete("core_prod", new string[] { this.txtCondition.Text }, IndexDeleteType.ByQuqery);
            }
        }
    }


    /// <summary>
    /// 实体类
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductAttributeId { get; set; }

        public int Rank { get; set; }

        public DateTime EffectDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public string[] Coords { get; set; }

        public string ProductDescription { get; set; }

        public string[] TourSpotName { get; set; }

        public double SalesPrice { get; set; }
    }

    /// <summary>
    /// 反序列化器
    /// </summary>
    public class ProductDeserialize : ISolrObjectDeserializer<Product>
    {
        public IEnumerable<Product> Deserialize(SolrDocumentList result)
        {
            foreach (SolrDocument doc in result)
            {
                yield return new Product()
                {
                    Id = Convert.ToInt32(doc["id"]),
                    ProductName = doc["ProductName"].ToString(),
                    ProductTypeId = Convert.ToInt32(doc["ProductTypeId"]),
                    ProductAttributeId = Convert.ToInt32(doc["ProductAttributeId"]),
                    Rank = Convert.ToInt32(doc["Rank"]),
                    EffectDate = Convert.ToDateTime(doc["EffectDate"]),
                    ExpireDate = Convert.ToDateTime(doc["ExpireDate"]),
                    Coords = (string[])((ArrayList)doc["Coords"]).ToArray(typeof(string)),
                    ProductDescription = doc["ProductDescription"].ToString(),
                    TourSpotName = (string[])((ArrayList)doc["TourSpotName"]).ToArray(typeof(string)),
                    SalesPrice = Convert.ToDouble(doc["SalesPrice"])
                };
            }
        }
    }

    /// <summary>
    /// 序列化器
    /// </summary>
    public class ProductSerializer : ISolrObjectSerializer<Product>
    {

        public IEnumerable<SolrInputDocument> Serialize(IEnumerable<Product> objs)
        {
            IList<SolrInputDocument> docs = new List<SolrInputDocument>();

            foreach (var obj in objs)
            {
                SolrInputDocument doc = new SolrInputDocument();

                doc.Add("id", new SolrInputField("id", obj.Id));
                if (!string.IsNullOrEmpty(obj.ProductName))
                    doc.Add("ProductName", new SolrInputField("ProductName", obj.ProductName));
                if (obj.ProductTypeId > 0)
                    doc.Add("ProductTypeId", new SolrInputField("ProductTypeId", obj.ProductTypeId));
                if (obj.ProductAttributeId > 0)
                    doc.Add("ProductAttributeId", new SolrInputField("ProductAttributeId", obj.ProductAttributeId));
                if (obj.Rank > 0)
                    doc.Add("Rank", new SolrInputField("Rank", obj.Rank));
                if (obj.EffectDate > DateTime.MinValue)
                    doc.Add("EffectDate", new SolrInputField("EffectDate", obj.EffectDate));
                if (obj.ExpireDate > DateTime.MinValue)
                    doc.Add("ExpireDate", new SolrInputField("ExpireDate", obj.ExpireDate));
                if (obj.Coords != null && obj.Coords.Length > 0)
                    doc.Add("Coords", new SolrInputField("Coords", obj.Coords));
                if (!string.IsNullOrEmpty(obj.ProductDescription))
                    doc.Add("ProductDescription", new SolrInputField("ProductDescription", obj.ProductDescription));
                if (obj.TourSpotName != null && obj.TourSpotName.Length > 0)
                    doc.Add("TourSpotName", new SolrInputField("TourSpotName", obj.TourSpotName));
                if (obj.SalesPrice > 0)
                    doc.Add("SalesPrice", new SolrInputField("SalesPrice", obj.SalesPrice));

                docs.Add(doc);
            }

            return docs;

        }

    }
}