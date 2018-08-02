using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Data.DataMap;
using LoyalFilial.Framework.Data.DataMap.Map;
using LoyalFilial.Framework.Data.ScriptExpression;

namespace LoyalFilial.Framework.Data.Database
{
    public class SqlTableCommandGenerator : ITableCommandGenerate
    {
        public virtual string ParaPrefix
        {
            get { return "@"; }
        }

        #region Query

        protected virtual string Spell(string[] fields, string fix)
        {
            string fieldString = "";
            if (fields != null && fields.Length > 0)
            {
                for (var i = 0; i < fields.Length; i++)
                {
                    fieldString += fields[i];
                    if (i != (fields.Length - 1))
                    {
                        fieldString += fix;
                    }
                }
            }
            return fieldString;
        }

        public virtual string Select(string[] fields)
        {
            var fieldString = Spell(fields, ",");
            if (StringHelper.IsNullOrEmptyOrBlankString(fieldString))
                fieldString = "*";
            return string.Format("select {0} ", fieldString);
        }

        public virtual string From(string tableName)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(tableName))
                return null;
            return string.Format(" from {0} ", tableName);
        }

        public virtual string Where(string whereConditions)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(whereConditions))
                return "";
            return string.Format(" where {0}", whereConditions);
        }

        //public virtual string OrderBy(string[] fields, bool isAsc)
        //{
        //    var orderbyField = Spell(fields, isAsc ? " asc," : " desc,");
        //    if (StringHelper.IsNullOrEmptyOrBlankString(orderbyField))
        //        return string.Empty;
        //    return string.Format(" order by {0} {1}", orderbyField, isAsc ? "asc" : "desc");
        //}

        public virtual string OrderBy(string[] fields, string orderby)
        {
            var thisOrderby = Spell(fields, ",");
            if (StringHelper.IsNullOrEmptyOrBlankString(thisOrderby))
                return orderby;
            else
            {
                if (StringHelper.IsNullOrEmptyOrBlankString(orderby))
                    return thisOrderby;
                else
                {
                    return string.Format("{0},{1}", orderby, thisOrderby);
                }
            }

        }

        private string GetOrderBy(string ascOrderBy, string descOrderBy)
        {
            if (StringHelper.IsNullOrEmptyOrBlankString(ascOrderBy) &&
                StringHelper.IsNullOrEmptyOrBlankString(descOrderBy))
            {
                return string.Empty;
            }
            else
            {
                var result = "";
                if (!StringHelper.IsNullOrEmptyOrBlankString(ascOrderBy) &&
                    !StringHelper.IsNullOrEmptyOrBlankString(descOrderBy))
                {
                    return string.Format(" order by {0} asc, {1} desc", ascOrderBy, descOrderBy);
                }
                else
                {
                    if (!StringHelper.IsNullOrEmptyOrBlankString(ascOrderBy))
                    {
                        return string.Format(" order by {0} asc", ascOrderBy);
                    }
                    else
                    {
                        return string.Format(" order by {0} desc", descOrderBy);
                    }
                }
            }
        }

        public virtual string Page(int PageSize, int pageIndex)
        {
            //TODO: sql paging logic.
            return "";
        }

        public virtual string CommandText(ITableCommand command)
        {
            if (command == null)
                return string.Empty;
            var querySql = string.Format("{0}{1}{2}{3}", command.CommandText_Select, command.CommandText_From,
                                         command.CommandText_Where,
                                         this.GetOrderBy(command.CommandText_OrderBy_Asc,
                                                         command.CommandText_OrderBy_Desc));
            if (StringHelper.IsNullOrEmptyOrBlankString(command.CommandText_Page))
            {
                return querySql;
            }
            else
            {
                var countSql = string.Format("select count(1) {0} {1} ", command.CommandText_From,
                                             command.CommandText_Where);
                return string.Format("{0}{1} ; {2}", querySql, command.CommandText_Page, countSql);
            }
        }

        #endregion

        #region Update

        public virtual string Update<T>(List<T> entityList, string tableAlias, out List<DbParameter> paraList,
                                        params Expression<Func<T, object>>[] columnNameFileterExps) where T : class
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);

            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }

            List<PropertyMap> propertyList = new List<PropertyMap>();
            if (columnNameFileterExps != null && columnNameFileterExps.Length > 0)
            {
                var fields = ExpressionHelper.GetPropertyMapNamesFromExps(columnNameFileterExps);
                for (var i = 0; i < fields.Length; i++)
                {
                    propertyList.Add(DataMapper.FindColumnByPropertyName<T>(fields[i], em.PropertyMapList));
                }
            }
            else
            {
                propertyList = em.PropertyMapList.ToList();
            }

            var columnSql = new StringBuilder();
            columnSql.Append("(");
            var columnUpdateSql = new StringBuilder();
            var sql = new StringBuilder();

            bool isColumnGenerated = false;
            var pIndex = 0;
            var pList = new List<DbParameter>();

            for (int j = 0; j < entityList.Count; j++)
            {
                sql.Append("(");
                for (int i = 0; i < propertyList.Count; i++)
                {
                    var p = propertyList[i];
                    if (!isColumnGenerated)
                    {
                        var columnName = p.Column != null ? p.Column.ColumnName : p.Property.PropertyName;
                        columnSql.Append(columnName);
                        columnUpdateSql.Append(columnName);
                        columnUpdateSql.Append("=VALUES(");
                        columnUpdateSql.Append(columnName);
                        columnUpdateSql.Append(")");
                        if (i == propertyList.Count - 1)
                        {
                            columnSql.Append(")");
                            columnUpdateSql.Append(";");
                        }
                        else
                        {
                            columnSql.Append(",");
                            columnUpdateSql.Append(",");
                        }
                    }
                    var pName = string.Format("@P{0}", pIndex);
                    sql.Append(pName);
                    pIndex++;

                    pList.Add(LFFK.DataManager.DataProvider.GenerateParam(p.Property.GetValue(entityList[j]), pName));

                    if (i == propertyList.Count - 1)
                    {
                        isColumnGenerated = true;
                    }
                    else
                    {
                        sql.Append(",");
                    }
                }
                sql.Append(")");
                if (j < entityList.Count - 1)
                    sql.Append(",");
            }
            paraList = pList;
            return
                string.Format("START TRANSACTION; INSERT INTO {0}{1} VALUES {2} ON DUPLICATE KEY UPDATE {3} COMMIT;",
                              tableAlias, columnSql.ToString(), sql.ToString(), columnUpdateSql.ToString());
        }

        public virtual string Update(string tableName)
        {
            return string.Format("update {0} ", tableName);
        }

        public virtual string Set(string[] fields)
        {
            return string.Format(" set {0}", ParaValueString(fields,","));
        }

        public virtual string Where(string[] fields)
        {
            if (fields == null || fields.Length == 0)
                return "";
            return string.Format(" where {0}", ParaValueString(fields," and "));
        }

        protected virtual string ParaValueString(string[] fields, string splitString)
        {
            string result = "";
            for (var i = 0; i < fields.Length; i++)
            {
                result += string.Format(" {0} = {1}{0}", fields[i], ParaPrefix);
                if (i != fields.Length - 1)
                    result += splitString;
            }
            return result;
        }

        public virtual string CommandText(ITableUpdateCommand command)
        {
            return string.Format("{0}{1}{2}", command.CommandText_Update, command.CommandText_Set,
                                 command.CommandText_Where);
        }

        #endregion

        #region Insert

        public virtual string Insert(string tableName)
        {
            return string.Format(" insert into {0} ", tableName);
        }

        public virtual string IntoValue(string[] fields)
        {
            string columns = "", values = "";
            for (var i = 0; i < fields.Length; i++)
            {
                columns += fields[i];
                values += string.Format("{0}{1}", ParaPrefix, fields[i]);
                if (i != fields.Length - 1)
                {
                    columns += ",";
                    values += ",";
                }
            }
            return string.Format("({0}) values({1})", columns, values);
        }

        public virtual string IntoValue<T>(List<T> entityList, string tableAlias, out List<DbParameter> paraList)
        {
            var entityType = typeof (T);
            var em = EntityMapCache.GetEntityMap(entityType);

            if (StringHelper.IsNullOrEmptyOrBlankString(tableAlias) && em != null && em.Table != null)
            {
                tableAlias = em.Table.TableName;
            }
            var propertyList = em.PropertyMapList.Where(
                p => p.Column == null || (p.Column != null && p.Column.IsIdAutoIncrease == false)).ToList();

            var columnSql = new StringBuilder();
            columnSql.Append("(");
            var sql = new StringBuilder();

            bool isColumnGenerated = false;
            var pIndex = 0;
            var pList = new List<DbParameter>();

            for (int j = 0; j < entityList.Count; j++)
            {
                sql.Append("(");
                for (int i = 0; i < propertyList.Count; i++)
                {
                    var p = propertyList[i];
                    if (!isColumnGenerated)
                    {
                        var columnName = p.Column != null ? p.Column.ColumnName : p.Property.PropertyName;
                        columnSql.Append(columnName);
                        if (i == propertyList.Count - 1)
                        {
                            columnSql.Append(")");
                        }
                        else
                        {
                            columnSql.Append(",");
                        }
                    }
                    var pName = string.Format("@P{0}", pIndex);
                    sql.Append(pName);
                    pIndex++;
                    pList.Add(LFFK.DataManager.DataProvider.GenerateParam(p.Property.GetValue(entityList[j]), pName));
                    if (i == propertyList.Count - 1)
                    {
                        isColumnGenerated = true;
                    }
                    else
                    {
                        sql.Append(",");
                    }
                }
                sql.Append(")");
                if (j < entityList.Count - 1)
                    sql.Append(",");
                else
                {
                    sql.Append(";");
                }
            }

            paraList = pList;
            return string.Format("START TRANSACTION; INSERT INTO {0}{1} VALUES {2} COMMIT;", tableAlias, columnSql, sql);
        }

        public virtual string CommandText(ITableInsertCommand command)
        {
            return string.Format("{0}{1} {2}", command.CommandText_Insert, command.CommandText_IntoValue,
                                 ";select @@IDENTITY");
        }

        #endregion

        #region Delete

        public virtual string Delete(string tableName)
        {
            return string.Format(" delete from {0}", tableName);
        }

        public virtual string CommandText(ITableDeleteCommand command)
        {
            return string.Format("{0}{1}", command.CommandTextDelete, command.CommandText_Where);
        }

        #endregion
    }
}