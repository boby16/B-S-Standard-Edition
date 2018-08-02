using System;
using System.Linq.Expressions;

namespace LoyalFilial.Framework.Data.ScriptExpression
{
    internal class ExpressionHelper
    {
        public static string[] GetPropertyMapNamesFromExps<TTable>(Expression<Func<TTable, object>>[] exps) where TTable : class
        {
            if (exps == null || exps.Length == 0)
            {
                return new string[] { };
            }

            string[] list = new string[exps.Length];
            for (int i = 0; i < exps.Length; i++)
            {
                Expression<Func<TTable, object>> exp = exps[i];
                list[i] = GetPropertyMapNameFromExp<TTable, object>(exp);
            }

            return list;
        }

        public static string GetPropertyMapNameFromExp<TTable, TValue>(Expression<Func<TTable, TValue>> exp) where TTable : class
        {
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

            return propertyName;
        }
    }
}
