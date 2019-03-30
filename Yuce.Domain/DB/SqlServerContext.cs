using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using HelpersProject;
using Microsoft.Extensions.Configuration;

namespace Yuce.Domain.DB
{

    public class SqlServerContext : BaseContext, IDatabaseContext
    {
        public SqlServerContext(IConfiguration configuration) : base(configuration)
        {

        }
        public DbParameter GetDbParameter(String parameterName, object value, SqlDbType sqlDbType)
        {
            var t = new SqlParameter();
            t.SqlDbType = sqlDbType;
            t.ParameterName = parameterName;
            t.Value = value;
            return t;
        }
        public DataSet ExecuteDataSet(String connectionStringKey, 
            string commandText, 
            CommandType commandType,
            List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(connectionStringKey);
            return DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType,
                 parameters.Cast<SqlParameter>().ToArray());
        }

        public int ExecuteScalar(string connectionStringKey, string commandText, CommandType commandType, List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(connectionStringKey);
            return DatabaseUtility.ExecuteScalar(connectionString, commandText, commandType, parameters.Cast<SqlParameter>().ToArray()).ToInt();
        }

        public int SaveOrUpdate(string connectionStringKey, string commandText, CommandType commandType, List<DbParameter> parameterList)
        {
            string connectionString = Configuration.GetConnectionString(connectionStringKey);
            commandText = String.Format(@"{0}", commandText);
            int id = ExecuteScalar(connectionStringKey,
                commandText, commandType,
                parameterList).ToInt();
            return id;
        }
    }

}
