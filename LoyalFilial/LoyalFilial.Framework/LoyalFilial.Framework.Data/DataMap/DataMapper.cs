using System.Collections.Specialized;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.DataMap.Map;
using LoyalFilial.Framework.Data.DataMap.Source;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Data.DataMap
{
    internal class DataMapper
    {
        public static string FindColumnNameByPropertyName<TEnttiy>(string propertyName, PropertyMapCollection pmc)
        {
            var p = FindColumnByPropertyName<TEnttiy>(propertyName, pmc);
            if (p != null && p.Column != null)
                return p.Column.ColumnName;
            return propertyName;
        }

        public static PropertyMap FindColumnByPropertyName<TEntity>(string propertyName, PropertyMapCollection pmc)
        {
            if (pmc == null && pmc.Count == 0)
            {
                return null;
            }
            foreach (var p in pmc)
            {
                if (p.Property.PropertyName == propertyName)
                    return p;
            }
            return null;
        }

        public static string FindColumnNameByPropertyName<TEnttiy>(string propertyName)
        {
            return FindColumnNameByPropertyName<TEnttiy>(propertyName, FindPropertyList<TEnttiy>());
        }

        public static PropertyMapCollection FindPropertyList<TEnttiy>()
        {
            var entityType = typeof (TEnttiy);
            return EntityMapCache.GetEntityMap(entityType).PropertyMapList;
        }

        public static IActTResult<TEntity> Map<TEntity>(IDataSource dataSource) where TEntity : class
        {
            var entityType = typeof (TEntity);
            var pmc = EntityMapCache.GetEntityMap(entityType).PropertyMapList;
            if (pmc == null || pmc.Count == 0)
            {
                throw new Exception("no property.");
            }
            var entity = (TEntity) Activator.CreateInstance(entityType);
            foreach (var p in pmc)
            {
                string columnName;
                if (p.Column != null && !StringHelper.IsNullOrEmptyOrBlankString(p.Column.ColumnName))
                    columnName = p.Column.ColumnName;
                else
                    columnName = p.Property.PropertyName;
                if (!dataSource.HasField(columnName))
                    continue;
                var pValue = dataSource.GetFieldValue(columnName);
                p.Property.SetValue(entity, pValue);
            }
            return new ActTResult<TEntity>(entity);
        }

        public static IActTResult<TEntity> Map<TEntity>(NameValueCollection collection) where TEntity : class
        {
            var entityType = typeof (TEntity);
            var pmc = EntityMapCache.GetEntityMap(entityType).PropertyMapList;
            if (pmc == null || pmc.Count == 0)
            {
                throw new Exception("no property.");
            }
            var entity = (TEntity) Activator.CreateInstance(entityType);
            foreach (var p in pmc)
            {
                var pId = string.Format("{0}_{1}", p.TypeName, p.Property.PropertyName);
                var pValue = collection[pId];
                if (pValue != null)
                    p.Property.SetValue(entity, pValue);
            }
            return new ActTResult<TEntity>(entity);
        }

        public static List<TEntity> MapList<TEntity>(DbDataReader dataReader) where TEntity : class
        {
            if (dataReader == null)
                return null;
            var resultList = new List<TEntity>();
            IActTResult<TEntity> mapResult = null;
            using (dataReader)
            {
                while (dataReader.Read())
                {
                    mapResult = Map<TEntity>(new DataReaderSource(dataReader));
                    if (mapResult.IsSucceed)
                    {
                        resultList.Add(mapResult.Data);
                    }
                }
            }

            return resultList;
        }

        public static List<TEntity> MapList<TEntity>(DataSet dataSet) where TEntity : class
        {
            if (dataSet == null)
                return null;
            var resultList = new List<TEntity>();
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0] != null &&
                dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var result = DataMapper.Map<TEntity>(new DataRowSource(dataRow));
                    if (result.IsSucceed)
                        resultList.Add(result.Data);

                }
            }
            return resultList;
        }
    }
}