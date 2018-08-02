using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Common
{
    /// <summary>
    /// 动态拼装Lambda
    /// </summary>
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, object>>[] AssembleColumns<T>(params Expression<Func<T, object>>[] columnNameFileterExps)
        {
            return columnNameFileterExps;
        }

        public static Expression<Func<T, bool>> AssembleColumn<T>(Expression<Func<T, bool>> columnNameFileterExps)
        {
            return columnNameFileterExps;
        }
    }
}
