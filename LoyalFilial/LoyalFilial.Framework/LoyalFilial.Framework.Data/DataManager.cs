using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.Table;
using LoyalFilial.Framework.Data.DataMap.Map;
using LoyalFilial.Framework.Data.ScriptExpression;
using LoyalFilial.Framework.Data.DataMap;
using System.Data.Common;
using System.Data;
using LoyalFilial.Framework.Data.Database;
using LoyalFilial.Framework.Data.DataMap.Source;
using LoyalFilial.Framework.Data.Table.Action;
using System.Text;

namespace LoyalFilial.Framework.Data
{
    public class DataManager : IDataManager
    {
        #region IDataManager

        private static Dictionary<Type, dynamic> _maps = new Dictionary<Type, dynamic>();
        private static readonly object _syncObject = new object();

        public bool IsMultiOperation { get; set; }

        public IDataProvider DataProvider { get; set; }

        #region TableQuery

        public ITableQuerySelect<T> TableQuery<T>() where T : class
        {
            return this.TableQuery<T>(null, IsMultiOperation);
        }

        public ITableQuerySelect<T> TableQuery<T>(string connectionString) where T : class
        {
            return this.TableQuery<T>(connectionString, IsMultiOperation);
        }

        public ITableQuerySelect<T> TableQuery<T>(string connectionString, bool isMultiOperation) where T : class
        {
            if (isMultiOperation)
            {
                return new TableQuery<T>(connectionString) {DataManager = this};
            }
            else
            {
                dynamic tableQuery = null;
                lock (_syncObject)
                {
                    var type = typeof (T);
                    _maps.TryGetValue(type, out tableQuery);
                    if (tableQuery == null)
                    {
                        tableQuery = new TableQuery<T>(connectionString) {DataManager = this};
                        _maps.Add(type, tableQuery);
                    }
                    if (!StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                    {
                        tableQuery = new TableQuery<T>(connectionString) {DataManager = this};
                    }
                }
                return tableQuery;
            }
        }

        public ITableQuerySelect<T> TableQuery<T>(bool isMultiOperation) where T : class
        {
            return this.TableQuery<T>(null, IsMultiOperation);
        }

        #endregion

        #region SqlCommand Query

        public T Execute<T>(string sql) where T : class
        {
            return Execute<T>(null, sql);
        }

        public List<T> ExecuteList<T>(string sql, out int totalCount) where T : class
        {
            return ExecuteList<T>(null, sql, out totalCount);
        }

        public List<T> ExecuteList<T>(string connectionString, string sql, out int totalCount) where T : class
        {
            totalCount = 0;
            if (StringHelper.IsNullOrEmptyOrBlankString(sql) || StringHelper.IsForbiddenSql(sql) ||
                StringHelper.HasIdenticallyEqual(sql))
                return null;
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.DataProvider.ConnectionString;
            var dataSet = this.DataProvider.ExecuteDataset(connectionString, System.Data.CommandType.Text, sql);
            var resultList = DataMapper.MapList<T>(dataSet);
            if (dataSet != null && dataSet.Tables.Count > 1 && dataSet.Tables[1] != null &&
                dataSet.Tables[1].Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            }
            return resultList;
        }

        public T Execute<T>(string connectionString, string sql) where T : class
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(sql) || StringHelper.IsForbiddenSql(sql) ||
                StringHelper.HasIdenticallyEqual(sql))
                return default(T);
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.DataProvider.ConnectionString;
            var dataSet = this.DataProvider.ExecuteDataset(connectionString, System.Data.CommandType.Text, sql);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0] != null &&
                dataSet.Tables[0].Rows.Count > 0)
            {
                var result = DataMapper.Map<T>(new DataRowSource(dataSet.Tables[0].Rows[0]));
                if (result.IsSucceed)
                    return result.Data;
                else
                    return default(T);
            }
            else
            {
                return default(T);
            }
        }

        public List<T> ExecuteList<T>(string sql) where T : class
        {
            return ExecuteList<T>(null, sql);
        }

        public List<T> ExecuteList<T>(string connectionString, string sql) where T : class
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(sql) || StringHelper.IsForbiddenSql(sql) ||
                StringHelper.HasIdenticallyEqual(sql))
                return null;
            if (StringHelper.IsNullOrEmptyOrBlankString(connectionString))
                connectionString = this.DataProvider.ConnectionString;
            var dataSet = this.DataProvider.ExecuteDataset(connectionString, System.Data.CommandType.Text, sql);
            return DataMapper.MapList<T>(dataSet);
        }

        #endregion

        #region Insert

        public ITableActResult Insert<T>(T entity, string tableAlias) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entity, tableAlias);
        }

        public ITableActResult Insert<T>(T entity) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entity, null);
        }

        public ITableActResult Insert<T>(string connectionString, T entity, string tableAlias) where T : class
        {
            return Insert(connectionString, entity, tableAlias, false);
        }

        public ITableActResult Insert<T>(string connectionString, T entity) where T : class
        {
            return Insert<T>(connectionString, entity, null);
        }

        public ITableActResult Insert<T>(T entity, string tableAlias, bool includeIdAutoIncrease) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entity, tableAlias, includeIdAutoIncrease);
        }

        public ITableActResult Insert<T>(T entity, bool includeIdAutoIncrease) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entity, null, includeIdAutoIncrease);
        }

        public ITableActResult Insert<T>(string connectionString, T entity, string tableAlias,
            bool includeIdAutoIncrease = false) where T : class
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);

            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }
            List<DbParameter> pList = new List<DbParameter>();
            List<string> fc = new List<string>();
            foreach (
                var pm in
                    em.PropertyMapList.Where(
                        p =>
                            p.Column == null || (p.Column != null && p.Column.IsIdAutoIncrease == includeIdAutoIncrease))
                )
            {
                var wName = pm.Column != null ? pm.Column.ColumnName : pm.Property.PropertyName;
                fc.Add(wName);

                var p = this.DataProvider.GenerateParam(pm.Property.GetValue(entity), wName);
                if (p != null && !pList.Contains(p))
                    pList.Add(p);

            }

            ITableInsertCommand command = new TableInsertCommand();
            command.CommandText_Insert = this.DataProvider.TableCommandGenerator.Insert(tableAlias);
            command.CommandText_IntoValue = this.DataProvider.TableCommandGenerator.IntoValue(fc.ToArray());
            var commandText = this.DataProvider.TableCommandGenerator.CommandText(command);

            try
            {
                var result = new TableActionResult();
                //result.EffectedRowNo = this.DataProvider.ExecuteNonQuery(commandText, pList.ToArray());
                var ds = this.DataProvider.ExecuteDataset(connectionString, CommandType.Text, commandText,
                    pList.ToArray());
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result.IdentityRowNo = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                }
                return result;
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public ITableActResult Insert<T>(string connectionString, T entity, bool includeIdAutoIncrease) where T : class
        {
            return Insert<T>(connectionString, entity, null, includeIdAutoIncrease);
        }

        #region List

        public ITableActResult Insert<T>(List<T> entityList, string tableAlias) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entityList, tableAlias);
        }

        public ITableActResult Insert<T>(List<T> entityList) where T : class
        {
            return Insert<T>(this.DataProvider.ConnectionString, entityList, null);
        }

        public ITableActResult Insert<T>(string connectionString, List<T> entityList, string tableAlias) where T : class
        {
            var pList = new List<DbParameter>();
            var totalSql = this.DataProvider.TableCommandGenerator.IntoValue(entityList, tableAlias, out pList);
            try
            {
                var rowNo = this.DataProvider.ExecuteNonQuery(connectionString, CommandType.Text, totalSql,
                    pList.ToArray());
                return new TableActionResult(rowNo);
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public ITableActResult Insert<T>(string connectionString, List<T> entityList) where T : class
        {
            return Insert<T>(connectionString, entityList, null);
        }

        #endregion

        #endregion

        #region Update

        public ITableActResult Update<T>(T entity, string tableAlias,
            params System.Linq.Expressions.Expression<Func<T, object>>[]
                columnNameFileterExps) where T : class
        {
            return Update<T>(this.DataProvider.ConnectionString, entity, tableAlias, columnNameFileterExps);
        }

        public ITableActResult Update<T>(T entity,
            params System.Linq.Expressions.Expression<Func<T, object>>[]
                columnNameFileterExps) where T : class
        {
            return Update<T>(entity, null, columnNameFileterExps);
        }

        public ITableActResult Update<T>(string connectionString, T entity, string tableAlias,
            params System.Linq.Expressions.Expression<Func<T, object>>[]
                columnNameFileterExps) where T : class
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);

            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }

            List<PropertyMap> setFields = new List<PropertyMap>();
            if (columnNameFileterExps != null && columnNameFileterExps.Length > 0)
            {
                var fields = ExpressionHelper.GetPropertyMapNamesFromExps(columnNameFileterExps);
                for (var i = 0; i < fields.Length; i++)
                {
                    var p = DataMapper.FindColumnByPropertyName<T>(fields[i], em.PropertyMapList);
                    if (p != null && (p.Column == null || (p.Column != null && p.Column.IsIdAutoIncrease == false)))
                        setFields.Add(p);
                }
            }

            var whereFields =
                em.PropertyMapList.Where(p => p.Column != null && p.Column.IsPrimaryKey == true).ToList<PropertyMap>();

            ITableUpdateCommand updateCommand = new TableUpdateCommand();
            updateCommand.CommandText_Update = this.DataProvider.TableCommandGenerator.Update(tableAlias);
            List<DbParameter> pList = new List<DbParameter>();

            PropertyMap[] pmList;
            if (setFields != null && setFields.Count > 0)
            {
                pmList = setFields.ToArray();
            }
            else
            {
                pmList =
                    em.PropertyMapList.Where(
                        p => p.Column == null || (p.Column != null && p.Column.IsIdAutoIncrease == false))
                        .ToArray<PropertyMap>();
            }
            List<string> fc = new List<string>();
            List<string> fcParaed = new List<string>();
            for (var i = 0; i < pmList.Length; i++)
            {
                var cName = pmList[i].Column != null ? pmList[i].Column.ColumnName : pmList[i].Property.PropertyName;
                fc.Add(cName);
                var p = this.DataProvider.GenerateParam(pmList[i].Property.GetValue(entity), cName);
                if (p != null && !pList.Contains(p))
                {
                    pList.Add(p);
                    fcParaed.Add(cName);
                }
            }
            updateCommand.CommandText_Set = this.DataProvider.TableCommandGenerator.Set(fc.ToArray());
            fc.Clear();
            foreach (var wp in whereFields)
            {
                var wName = wp.Column != null ? wp.Column.ColumnName : wp.Property.PropertyName;
                fc.Add(wName);
                if (!fcParaed.Contains(wName))
                {
                    var p = this.DataProvider.GenerateParam(wp.Property.GetValue(entity), wName);
                    if (p != null)
                        pList.Add(p);
                }
            }
            updateCommand.CommandText_Where = this.DataProvider.TableCommandGenerator.Where(fc.ToArray());

            var commandText = this.DataProvider.TableCommandGenerator.CommandText(updateCommand);

            try
            {
                this.DataProvider.ExecuteNonQuery(connectionString, CommandType.Text, commandText, pList.ToArray());
                return new TableActionResult();
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public ITableActResult Update<T>(string connectionString, T entity,
            params System.Linq.Expressions.Expression<Func<T, object>>[]
                columnNameFileterExps) where T : class
        {
            return Update<T>(connectionString, entity, null, columnNameFileterExps);
        }

        public ITableActResult Update<T>(List<T> entityList, string tableAlias,
            params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameFileterExps) where T : class
        {
            return Update(this.DataProvider.ConnectionString, entityList, tableAlias, columnNameFileterExps);
        }

        public ITableActResult Update<T>(List<T> entityList,
            params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameFileterExps) where T : class
        {
            return Update(this.DataProvider.ConnectionString, entityList, null, columnNameFileterExps);
        }

        public ITableActResult Update<T>(string connectionString, List<T> entityList, string tableAlias,
            params System.Linq.Expressions.Expression<Func<T, object>>[]
                columnNameFileterExps) where T : class
        {
            var pList = new List<DbParameter>();
            var totalSql = this.DataProvider.TableCommandGenerator.Update<T>(entityList, tableAlias, out pList,
                columnNameFileterExps);

            try
            {
                this.DataProvider.ExecuteNonQuery(connectionString, CommandType.Text, totalSql, pList.ToArray());
                return new TableActionResult();
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public ITableActResult Update<T>(string connectionString, List<T> entityList,
            params System.Linq.Expressions.Expression<Func<T, object>>[] columnNameFileterExps) where T : class
        {
            return Update(connectionString, entityList, null, columnNameFileterExps);
        }

        #endregion

        #region Delete

        public ITableActResult Delete<T>(T entity, string tableAlias) where T : class
        {
            return Delete<T>(this.DataProvider.ConnectionString, entity, tableAlias);
        }

        public ITableActResult Delete<T>(T entity) where T : class
        {
            return Delete<T>(entity, null);
        }

        public ITableActResult Delete<T>(string connectionString, T entity, string tableAlias) where T : class
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);
            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }
            var whereFields =
                em.PropertyMapList.Where(p => p.Column != null && p.Column.IsPrimaryKey == true).ToList<PropertyMap>();
            List<DbParameter> pList = new List<DbParameter>();
            List<string> fc = new List<string>();
            foreach (var wp in whereFields)
            {
                var wName = wp.Column != null ? wp.Column.ColumnName : wp.Property.PropertyName;
                fc.Add(wName);

                var p = this.DataProvider.GenerateParam(wp.Property.GetValue(entity), wName);
                if (p != null && !pList.Contains(p))
                    pList.Add(p);

            }

            ITableDeleteCommand command = new TableDeleteCommand();
            command.CommandTextDelete = this.DataProvider.TableCommandGenerator.Delete(tableAlias);
            command.CommandText_Where = this.DataProvider.TableCommandGenerator.Where(fc.ToArray());
            var commandText = this.DataProvider.TableCommandGenerator.CommandText(command);

            try
            {
                this.DataProvider.ExecuteNonQuery(connectionString, CommandType.Text, commandText, pList.ToArray());
                return new TableActionResult();
            }
            catch (Exception ex)
            {
                return new TableActionResult(ex.Message);
            }
        }

        public ITableActResult Delete<T>(string connectionString, T entity) where T : class
        {
            return Delete<T>(connectionString, entity, null);
        }

        public ITableDelete<T> Delete<T>() where T : class
        {
            return Delete<T>(this.DataProvider.ConnectionString, string.Empty);
        }

        public ITableDelete<T> Delete<T>(string connectionString) where T : class
        {
            return Delete<T>(connectionString, string.Empty);
        }

        public ITableDelete<T> Delete<T>(string connectionString, string tableAlias) where T : class
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);
            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }
            ITableDeleteCommand command = new TableDeleteCommand();
            command.CommandTextDelete = this.DataProvider.TableCommandGenerator.Delete(tableAlias);
            command.ConnectionString = connectionString;
            return new TableDelete<T>(this, command);
        }

        #endregion

        public IActTResult<T> Map<T>(NameValueCollection collection) where T : class
        {
            if (collection == null || collection.Count == 0)
                return new ActTResult<T>(-1);
            else
            {
                return DataMap.DataMapper.Map<T>(collection);
            }
        }

        #endregion

        #region IModule

        public string Name { get; set; }

        public IFramework Framework { get; private set; }

        public bool Config(IFramework framework, IConfigElement configElement)
        {
            return this.Config(framework, configElement, true);
        }

        public bool RefreshCache()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IConfigurable

        public bool ConfigInitialized { get; private set; }

        public bool Config(IConfigElement configElement)
        {
            return this.Config(this.Framework, configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            return this.Config(this.Framework, configElement, isForce);
        }

        #endregion

        #region Config

        protected bool Config(IFramework framework, IConfigElement configElement, bool isForce)
        {
            this.Framework = framework;
            if (!this.ConfigInitialized || isForce)
            {
                Init(configElement.XmlElement);
                this.ConfigInitialized = true;
            }
            return true;
        }

        private bool Init(XmlElement xmlElement)
        {
            if (xmlElement == null)
                throw new Exception(string.Format(Constants.Error_NoConfigForModule, Constants.Module_Log_Name));
            try
            {
                XElement xElement = XmlHelper.ToXElement(xmlElement);
                var typeConfig =
                    xElement.Element(Constants.Config_Database_DataProvider_Name)
                        .Attribute(Constants.Config_Module_Type);
                var typeAssembly = typeConfig.Value.Split(new char[] {','});
                var file = string.Format(Constants.Config_Module_Assembly,
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    typeAssembly[1].Trim());
                if (!File.Exists(file))
                {
                    file = string.Format(Constants.Config_Module_Assembly_Web,
                        AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                        typeAssembly[1].Trim());
                }
                this.DataProvider = TypeHelper.CreateObject(file, typeAssembly[0].Trim(), false) as IDataProvider;

                typeConfig =
                    xElement.Element(Constants.Config_Database_DataProvider_Name)
                        .Element(DataConstants.Data_TableCommandGenerator_Name)
                        .Attribute(Constants.Config_Module_Type);
                typeAssembly = typeConfig.Value.Split(new char[] {','});
                file = string.Format(Constants.Config_Module_Assembly,
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase, typeAssembly[1].Trim());
                if (!File.Exists(file))
                {
                    file = string.Format(Constants.Config_Module_Assembly_Web,
                        AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                        typeAssembly[1].Trim());
                }
                this.DataProvider.TableCommandGenerator =
                    TypeHelper.CreateObject(file, typeAssembly[0].Trim(), false) as ITableCommandGenerate;

                var csList = new List<IDatabaseConnectionString>();
                foreach (
                    var cs in
                        xElement.Element(Constants.Config_Database_DataProvider_Name)
                            .Element(Constants.Config_Database_ConnectionStrings)
                            .Elements(Constants.Config_Database_ConnectionString))
                {
                    var defaultAtt = cs.Attribute(Constants.Config_Node_IsDefault);
                    var isDefault = defaultAtt == null ? false : Convert.ToBoolean(defaultAtt.Value);
                    var cString = cs.Attribute(Constants.Config_Database_ConnectionString).Value;
                    var maxPoolsAtt = cs.Attribute(Constants.Config_Node_MaxPools);
                    var maxPools = maxPoolsAtt == null ? 0 : Convert.ToInt32(maxPoolsAtt.Value);
                    if (isDefault)
                    {
                        this.DataProvider.ConnectionString = cString;
                    }
                    csList.Add(new DatabaseConnectionString()
                    {
                        Key = cs.Attribute(Constants.Config_Node_Key).Value,
                        ConnectionString = cString,
                        IsDefault = isDefault,
                        MaxPools = maxPools
                    });
                }

                this.DataProvider.ConnectionStrings = csList;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Data_InitConfigFailed, ex);
            }
        }

        #endregion
    }
}