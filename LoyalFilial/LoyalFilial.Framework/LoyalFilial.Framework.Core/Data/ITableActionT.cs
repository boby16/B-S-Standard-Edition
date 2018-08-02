using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LoyalFilial.Framework.Core.Data
{
    public interface ITableDeleteExecute<T> where T : class
    {
        ITableDeleteCommand Command { get; }
        IDataManager DataManager { get; set; }

        ITableActResult Execute();
    }

    public interface ITableDelete<T> where T : class
    {
        ITableDeleteCommand Command { get; }
        IDataManager DataManager { get; set; }

        ITableDeleteExecute<T> Where(Expression<Func<T, bool>> columnNameExp);
    }
}
