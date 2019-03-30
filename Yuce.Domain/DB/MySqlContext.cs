using HelpersProject;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using Yuce.Domain.DB.Helpers;

namespace Yuce.Domain.DB
{

    public class MySqlContext : BaseContext, IDatabaseContext
    {
        public MySqlContext(IConfiguration configuration) : base(configuration)
        {

        }
        public DbParameter GetDbParameter(String parameterName, object value, SqlDbType sqlDbType)
        {
            var t = new MySqlParameter();
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
            return MySqlDatabaseUtility.ExecuteDataSet(new MySqlConnection(connectionString), commandText, commandType,
                parameters.Cast<MySqlParameter>().ToArray());
        }


        public int ExecuteScalar(
            string commandText,
            CommandType commandType,
            List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            return MySqlDatabaseUtility.ExecuteScalar(new MySqlConnection(connectionString),
                commandText,
                commandType,
                parameters.Cast<MySqlParameter>().ToArray()).ToInt();
        }
    
        public int SaveOrUpdate(string commandText, CommandType commandType, List<DbParameter> parameterList)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionStringKey);
            commandText = String.Format(@"{0}(@p_Id,@p_Name)", commandText);


            int id = MySqlDatabaseUtility.ExecuteScalar(new MySqlConnection(connectionString), 
                commandText,
                CommandType.Text,
                parameterList.Cast<MySqlParameter>().ToArray()).ToInt();
            return id;
        }

      

    }

}