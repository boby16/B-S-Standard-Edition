using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyNet.Solr;
using EasyNet.Solr.Commons;
using EasyNet.Solr.Impl;

namespace LoyalFilial.Framework.Search
{
    internal class SearchManager : ISearchManage
    {

        static readonly OptimizeOptions optimizeOptions;
        private static readonly ISolrResponseParser<NamedList, ResponseHeader> binaryResponseHeaderParser;
        private static readonly ISolrUpdateOperations<NamedList> updateOperations;
        private static readonly ISolrQueryOperations<NamedList> operations;
        private static readonly SearchConfig searchConfig;
        static SearchManager()
        {
            searchConfig = new SearchConfig();
            optimizeOptions = new OptimizeOptions();
            binaryResponseHeaderParser = new BinaryResponseHeaderParser();
            IUpdateParametersConvert<NamedList> updateParametersConvert = new BinaryUpdateParametersConvert();
            ISolrUpdateConnection<NamedList, NamedList> solrUpdateConnection = new SolrUpdateConnection<NamedList, NamedList>() { ServerUrl = searchConfig.ServerUrl };
            updateOperations = new SolrUpdateOperations<NamedList, NamedList>(solrUpdateConnection, updateParametersConvert) { ResponseWriter = "javabin" };
            ISolrQueryConnection<NamedList> connection = new SolrQueryConnection<NamedList>() { ServerUrl = searchConfig.ServerUrl };
            operations = new SolrQueryOperations<NamedList>(connection) { ResponseWriter = "javabin" };
        }


        /// <summary>
        /// 保存索引
        /// </summary>
        /// <typeparam name="T">对象实体</typeparam>
        /// <param name="collectionName">查询的集合（solr中的集体名称）</param>
        /// <param name="listObj">保存对的对象集</param>
        /// <param name="serializer">序列方式</param>
        /// <returns>保存结果</returns>
        public IndexOperationResponse SaveIndex<T>(string collectionName, List<T> listObj, ISolrObjectSerializer<T> serializer)
        {
            try
            {
                collectionName = string.IsNullOrEmpty(collectionName) ? searchConfig.Collection : collectionName;
                IList<SolrInputDocument> docs = (IList<SolrInputDocument>)serializer.Serialize(listObj);
                var result = updateOperations.Update(collectionName, "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, Docs = docs });
                var header = binaryResponseHeaderParser.Parse(result);
                return new IndexOperationResponse
                {
                    IsSuccess = header.Status == 0,
                    Message = string.Empty,
                    QTime = header.QTime,
                    Status = header.Status
                };
            }
            catch (Exception ex)
            {
                return new IndexOperationResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    QTime = -1,
                    Status = -1
                };
            }
        }

        /// <summary>
        /// 原子操作索引
        /// </summary>
        /// <param name="collectionName">操作的集合（solr中的集体名称）</param>
        /// <param name="uniqueKeyName">主键名称（solr中的主键）</param>
        /// <param name="uniqueKeyValue">主键值（solr中的主键值）</param>
        /// <param name="setItem">需要操作的集体（key为字段名称，value为值）</param>
        /// <param name="operationType">操作类型</param>
        /// <returns>操作结果</returns>
        public IndexOperationResponse AtomOperateIndex(string collectionName, string uniqueKeyName, object uniqueKeyValue, Dictionary<string, object> setItem, IndexOperationType operationType = IndexOperationType.Update)
        {
            var docs = new List<SolrInputDocument>();
            var doc = new SolrInputDocument();
            try
            {
                doc.Add(uniqueKeyName, new SolrInputField(uniqueKeyName, uniqueKeyValue));
                foreach (var item in setItem)
                {
                    var setOper = new Hashtable();
                    switch (operationType)
                    {
                        case IndexOperationType.Add:
                            setOper.Add("add", item.Value);
                            break;
                        case IndexOperationType.Update:
                            setOper.Add("set", item.Value);
                            break;
                        default:
                            setOper.Add("set", item.Value);
                            break;
                    }
                    doc.Add(item.Key, new SolrInputField(item.Key, setOper));
                    docs.Add(doc);
                }

                collectionName = string.IsNullOrEmpty(collectionName) ? searchConfig.Collection : collectionName;
                var result = updateOperations.Update(collectionName, "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, Docs = docs });
                var header = binaryResponseHeaderParser.Parse(result);
                return new IndexOperationResponse
                {
                    IsSuccess = header.Status == 0,
                    Message = string.Empty,
                    QTime = header.QTime,
                    Status = header.Status
                };
            }
            catch (Exception ex)
            {
                return new IndexOperationResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    QTime = -1,
                    Status = -1
                };
            }
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="collectionName">删除的集合（solr中的集体名称）</param>
        /// <param name="condition">条件</param>
        /// <param name="deleteType">删除的条件类型</param>
        /// <returns>操作结果</returns>
        public IndexOperationResponse Delete(string collectionName, IEnumerable<string> condition, IndexDeleteType deleteType = IndexDeleteType.ById)
        {
            try
            {
                NamedList result;
                collectionName = string.IsNullOrEmpty(collectionName) ? searchConfig.Collection : collectionName;
                switch (deleteType)
                {
                    case IndexDeleteType.ById:
                        result = updateOperations.Update(collectionName, "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, DelById = condition });
                        break;
                    case IndexDeleteType.ByQuqery:
                        result = updateOperations.Update(collectionName, "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, DelByQ = condition });
                        break;
                    default:
                        result = updateOperations.Update(collectionName, "/update", new UpdateOptions() { OptimizeOptions = optimizeOptions, DelById = condition });
                        break;
                }
                if (result != null)
                {
                    var header = binaryResponseHeaderParser.Parse(result);
                    return new IndexOperationResponse
                    {
                        IsSuccess = header.Status == 0,
                        Message = string.Empty,
                        QTime = header.QTime,
                        Status = header.Status
                    };
                }
                else
                {
                    return new IndexOperationResponse
                    {
                        IsSuccess = false,
                        Message = "unknown error",
                        QTime = -1,
                        Status = -1
                    };
                }
            }
            catch (Exception ex)
            {

                return new IndexOperationResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    QTime = -1,
                    Status = -1
                };
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="collectionName">搜索集合</param>
        /// <param name="condition">条件</param>
        /// <param name="deserializer">反序列化的对象</param>
        /// <returns>对应的结果集</returns>
        public QueryResponse<T> Query<T>(string collectionName, Dictionary<string, ICollection<string>> condition, ISolrObjectDeserializer<T> deserializer)
        {
            try
            {
                collectionName = string.IsNullOrEmpty(collectionName) ? searchConfig.Collection : collectionName;
                ISolrResponseParser<NamedList, QueryResults<T>> binaryQueryResultsParser = new BinaryQueryResultsParser<T>(deserializer);
                var result = operations.Query(collectionName, "/select", null, condition);
                var header = binaryResponseHeaderParser.Parse(result);
                var products = binaryQueryResultsParser.Parse(result);
                return new QueryResponse<T>
                {
                    IsSuccess = header.Status == 0,
                    Message = string.Empty,
                    QTime = header.QTime,
                    Status = header.Status,
                    Data = default(T),
                    DataList = products.ToList()
                };
            }
            catch (Exception ex)
            {
                return new QueryResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    QTime = -1,
                    Status = -1,
                    Data = default(T)
                };
            }
        }
    }


}
