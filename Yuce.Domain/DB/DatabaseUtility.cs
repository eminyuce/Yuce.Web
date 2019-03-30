using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Yuce.Domain.DB
{
    public abstract class DatabaseUtility
    {
        protected IConfiguration Configuration { get; set; }

        public DatabaseUtility(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int SqlCommandTimeout { get; set; }
        public DbParameter GetDbParameter(String parameterName, object value, SqlDbType sqlDbType)
        {
            var t = new SqlParameter();
            t.SqlDbType = sqlDbType;
            t.ParameterName = parameterName;
            t.Value = value;
            return t;
        }

        private SqlDbType GetDBType(System.Type type)
        {
            SqlParameter param;
            System.ComponentModel.TypeConverter tc;
            param = new SqlParameter();
            tc = System.ComponentModel.TypeDescriptor.GetConverter(param.DbType);
            if (tc.CanConvertFrom(type))
            {
                param.DbType = (DbType)tc.ConvertFrom(type.Name);
            }
            else
            {
                switch (type.Name)
                {
                    case "Char":
                        param.SqlDbType = SqlDbType.Char;
                        break;
                    case "SByte":
                        param.SqlDbType = SqlDbType.SmallInt;
                        break;
                    case "UInt16":
                        param.SqlDbType = SqlDbType.SmallInt;
                        break;
                    case "UInt32":
                        param.SqlDbType = SqlDbType.Int;
                        break;
                    case "UInt64":
                        param.SqlDbType = SqlDbType.Decimal;
                        break;
                    case "Byte[]":
                        param.SqlDbType = SqlDbType.Binary;
                        break;

                    default:
                        try
                        {
                            param.DbType = (DbType)tc.ConvertFrom(type.Name);
                        }
                        catch
                        {
                            // Some error handling
                        }
                        break;
                }
            }
            return param.SqlDbType;
        }

    }
}
