using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LoyalFilial.Common
{
    public class EntityHelper
    {
        /// <summary>
        /// 两个实体中相同属性赋值
        /// </summary>
        /// <typeparam name="SEntity">源实体类型</typeparam>
        /// <typeparam name="TEntity">目标实体类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static TEntity EntityTransfer<SEntity, TEntity>(SEntity source)
            where TEntity : class,new()
            where SEntity : class,new()
        {
            var sDict = new Dictionary<string, object>();
            foreach (var sp in source.GetType().GetProperties())
            {
                sDict.Add(sp.Name, sp.GetValue(source, null));
            }

            //var target = (TEntity)Activator.CreateInstance(tType);
            var target = new TEntity();
            foreach (var tp in typeof(TEntity).GetProperties())
            {
                if (sDict.ContainsKey(tp.Name))
                    tp.SetValue(target, sDict[tp.Name],null);
            }
            return target;
        }

        /// <summary>
        /// 两个实体中相同属性赋值
        /// </summary>
        /// <typeparam name="SEntity">源实体类型</typeparam>
        /// <typeparam name="TEntity">目标实体类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static List<TEntity> EntityTransfer<SEntity, TEntity>(List<SEntity> sourceList)
            where TEntity : class,new()
            where SEntity : class,new()
        {
            var result = new List<TEntity>();

            var sType = typeof(SEntity);
            var sProps = sType.GetProperties();
            var tType = typeof(TEntity);
            var tProps = tType.GetProperties();

            foreach (var source in sourceList)
            {
                var sDict = new Dictionary<string, object>();
                foreach (var sp in sProps)
                {
                    sDict.Add(sp.Name, sp.GetValue(source, null));
                }

                //var target = (TEntity)Activator.CreateInstance(tType);
                var target = new TEntity();
                foreach (var tp in tProps)
                {
                    if (sDict.ContainsKey(tp.Name))
                        tp.SetValue(target, sDict[tp.Name], null);
                }
                result.Add(target);
            }
            return result;
        }

        /// <summary>
        /// 判断两个实体是否相等
        /// </summary>
        /// <param name="t1">实体1</param>
        /// <param name="t2">实体2</param>
        /// <param name="columns">比较的字段值</param>
        /// <returns>true：不同；false：相同</returns>
        public static List<string> CompareEntity<TEntity>(TEntity t1, TEntity t2, List<string> columns)
            where TEntity : class
        {
            var changedColums = new List<string>();
            if (columns != null && columns.Count > 0)
            {
                columns = columns.ConvertAll<string>((p) => { return p.ToLower(); });
            }

            if (t1 != null && t2 != null)
            {
                var sDict = new Dictionary<string, object>();
                foreach (var sp in t1.GetType().GetProperties())
                {
                    if (columns != null && columns.Count > 0)
                    {
                        if (columns.Contains(sp.Name.ToLower()))
                            sDict.Add(sp.Name, sp.GetValue(t1, null));
                    }
                    else
                        sDict.Add(sp.Name, sp.GetValue(t1, null));
                }

                var tDict = new Dictionary<string, object>();
                foreach (var sp in t2.GetType().GetProperties())
                {
                    if (columns != null && columns.Count > 0)
                    {
                        if (columns.Contains(sp.Name.ToLower()))
                            tDict.Add(sp.Name, sp.GetValue(t2, null));
                    }
                    else
                        tDict.Add(sp.Name, sp.GetValue(t2, null));
                }
                foreach (var p in sDict)
                {
                    if (p.Value != null)
                    {
                        if (!p.Value.Equals(tDict[p.Key]))
                        {
                            changedColums.Add(p.Key);
                        }
                    }
                    else if (tDict[p.Key] != null)
                        changedColums.Add(p.Key);
                }
            }
            else if ((t1 != null && t2 == null) || (t1 == null && t2 != null))
                changedColums = columns;

            return changedColums;
        }
        /// <summary>
        /// 获取操作实体的修改表达式字段数组
        /// </summary>
        /// <typeparam name="TEntity">操作的实体</typeparam>
        /// <param name="exps">修改表达式列表</param>
        /// <returns>字段数组</returns>
        public static string[] GetPropertyMapNamesFromExps<TEntity>(Expression<Func<TEntity, object>>[] exps) where TEntity : class
        {
            string[] list = new string[exps.Length];
            for (int i = 0; i < exps.Length; i++)
            {
                Expression<Func<TEntity, object>> exp = exps[i];
                string propertyName = string.Empty;
                ParameterExpression parameterEx = (ParameterExpression)exp.Parameters[0];
                if (parameterEx == null)
                {
                    throw new Exception(string.Format("No Parameter Expression In Exp:{0}", exp.ToString()));
                }

                MemberExpression memberEx = exp.Body as MemberExpression;
                if (memberEx == null)
                {
                    UnaryExpression unaryExp = exp.Body as UnaryExpression;
                    if (unaryExp == null)
                    {
                        throw new Exception("value is invalud PropertyExpression.");
                    }
                    else
                    {
                        string unaryExpString = unaryExp.ToString();
                        int startPos = unaryExpString.IndexOf(".") + 1;
                        propertyName = unaryExpString.Substring(startPos, unaryExpString.Length - startPos - 1).Replace('.', '_');
                    }
                }
                else
                {
                    propertyName = memberEx.ToString().Substring(parameterEx.Name.Length + 1).Replace('.', '_'); ;
                }
                list[i] = propertyName;
            }

            return list;
        }

        /// <summary>
        /// 比较两个值是否相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool CompareValue<T>(T t1, T t2)
        {
            var isChanged = false;
            if (t1 != null)
            {
                if (t2 != null)
                {
                    if (!t1.Equals(t2))
                        isChanged = true;
                }
                else isChanged = true;
            }
            else if (t2 != null)
                isChanged = true;
            return isChanged;
        }

        /// <summary>
        /// 实体复制
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TEntity CopyEntity<TEntity>(TEntity t)
        {
            var source = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            var target = source;
            return SerializeHelper.JsonToObject<TEntity>(target);
        }
    }
}
