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
        public DataSet ExecuteDataSet(
            string commandText, 
            CommandType commandType,
            List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            return DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType,
                 parameters.Cast<SqlParameter>().ToArray());
        }

        public int ExecuteScalar(string commandText, CommandType commandType, List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            return DatabaseUtility.ExecuteScalar(new SqlConnection(connectionString), commandText, commandType, parameters.Cast<SqlParameter>().ToArray()).ToInt();
        }

        public int SaveOrUpdate(string commandText, CommandType commandType, List<DbParameter> parameterList)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            commandText = String.Format(@"{0}", commandText);
            int id = ExecuteScalar(
                commandText,
                commandType,
                parameterList).ToInt();
            return id;
        }
    }

}
