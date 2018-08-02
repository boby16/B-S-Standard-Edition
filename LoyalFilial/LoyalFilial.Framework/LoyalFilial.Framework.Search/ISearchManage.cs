using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Framework.Search
{
    public interface ISearchManage
    {
        /// <summary>
        /// 保存索引
        /// </summary>
        /// <typeparam name="T">对象实体</typeparam>
        /// <param name="collectionName">查询的集合（solr中的集合名称）</param>
        /// <param name="listObj">保存对的对象集</param>
        /// <param name="serializer">序列方式</param>
        /// <returns>保存结果</returns>
        IndexOperationResponse SaveIndex<T>(string collectionName, List<T> listObj, ISolrObjectSerializer<T> serializer);

        /// <summary>
        /// 原子操作索引
        /// </summary>
        /// <param name="collectionName">操作的集合（solr中的集体名称）</param>
        /// <param name="uniqueKeyName">主键名称（solr中的主键）</param>
        /// <param name="uniqueKeyValue">主键值（solr中的主键值）</param>
        /// <param name="setItem">需要操作的集体（key为solr字段名称，value为值）</param>
        /// <param name="operationType">操作类型</param>
        /// <returns>操作结果</returns>
        IndexOperationResponse AtomOperateIndex(string collectionName, string uniqueKeyName, object uniqueKeyValue, Dictionary<string, object> setItem, IndexOperationType operationType = IndexOperationType.Update);

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="collectionName">删除的集合（solr中的集体名称）</param>
        /// <param name="condition">条件</param>
        /// <param name="deleteType">删除的条件类型</param>
        /// <returns>操作结果</returns>
        IndexOperationResponse Delete(string collectionName, IEnumerable<string> condition, IndexDeleteType deleteType = IndexDeleteType.ById);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="collectionName">搜索集合</param>
        /// <param name="condition">条件</param>
        /// <param name="deserializer">反序列化的对象</param>
        /// <returns>对应的结果集</returns>
        QueryResponse<T> Query<T>(string collectionName, Dictionary<string, ICollection<string>> condition, ISolrObjectDeserializer<T> deserializer);
    }
}
