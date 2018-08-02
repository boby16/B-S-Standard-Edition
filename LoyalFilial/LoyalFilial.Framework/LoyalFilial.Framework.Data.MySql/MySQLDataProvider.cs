using System.Diagnostics;
using MySql.Data.MySqlClient;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Core.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using LoyalFilial.Framework.Data.Database;

namespace LoyalFilial.Framework.Data.MySql
{
    public class MySQLDataProvider : DatabaseProvider
    {
        public override DbConnection GetDbConnection()
        {
            return new MySqlConnection();
        }

        public override DbCommand GetDbCommand()
        {
            return new MySqlCommand();
        }

        public override DbDataAdapter GetDbDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        public override DbParameter GenerateParam(object value, string parameterName)
        {
            // Support passed in parameters
            var idbParam = value as IDbDataParameter;
            if (idbParam != null)
            {
                idbParam.ParameterName = string.Format("{0}{1}", "@", parameterName);
                return idbParam as DbParameter;
            }

            // Create the parameter
            var p = new MySqlParameter();
            p.ParameterName = string.Format("{0}{1}", "@", parameterName);

            // Assign the parmeter value
            if (value == null)
            {
                p.Value = DBNull.Value;
            }
            else
            {
                // Give the database type first crack at converting to DB required type
                if (value.GetType() == typeof (bool))
                {
                    value = ((bool) value) ? 1 : 0;
                }

                var t = value.GetType();
                if (t.IsEnum) // PostgreSQL .NET driver wont cast enum to int
                {
                    p.Value = (int) value;
                }
                else if (t == typeof (Guid))
                {
                    p.Value = value.ToString();
                    p.DbType = DbType.String;
                    p.Size = 40;
                }
                else if (t == typeof (string))
                {
                    // out of memory exception occurs if trying to save more than 4000 characters to SQL Server CE NText column. Set before attempting to set Size, or Size will always max out at 4000
                    if ((value as string).Length + 1 > 4000 && p.GetType().Name == "SqlCeParameter")
                        p.GetType().GetProperty("SqlDbType").SetValue(p, SqlDbType.NText, null);

                    p.Size = Math.Max((value as string).Length + 1, 4000);
                    // Help query plan caching by using common size
                    p.Value = value;
                }
                    //else if (t == typeof(AnsiString))
                    //{
                    //    // Thanks @DataChomp for pointing out the SQL Server indexing performance hit of using wrong string type on varchar
                    //    p.Size = Math.Max((value as AnsiString).Value.Length + 1, 4000);
                    //    p.Value = (value as AnsiString).Value;
                    //    p.DbType = DbType.AnsiString;
                    //}
                else if (value.GetType().Name == "SqlGeography") //SqlGeography is a CLR Type
                {
                    p.GetType().GetProperty("UdtTypeName").SetValue(p, "geography", null);
                    //geography is the equivalent SQL Server Type
                    p.Value = value;
                }

                else if (value.GetType().Name == "SqlGeometry") //SqlGeometry is a CLR Type
                {
                    p.GetType().GetProperty("UdtTypeName").SetValue(p, "geometry", null);
                    //geography is the equivalent SQL Server Type
                    p.Value = value;
                }
                else
                {
                    p.Value = value;
                }
            }

            return p;
        }
    }
}