using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Yuce.Domain.DB
{

    public class SqlServerDatabaseUtility : DatabaseUtility, IDatabaseUtility
    {
        public SqlServerDatabaseUtility(IConfiguration configuration) : base(configuration)
        {

        }

        public DataSet ExecuteDataSet(String connectionStringKey, 
            string commandText, 
            CommandType commandType,
            List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(connectionStringKey);
            return ExecuteDataSet(new SqlConnection(connectionString), null, commandText, commandType,
                 parameters.Cast<SqlParameter>());
        }

        private DataSet ExecuteDataSet(SqlConnection connection, string database, string commandText, CommandType commandType,
             IEnumerable<SqlParameter> parameters)
        {
            if (connection == null) throw new Exception("Connection must be established before query can be run.");
            ConnectionState state = connection.State;
            var value = new DataSet();

            // Build Adapter
            var adapter = new SqlDataAdapter(BuildCommand(commandText, connection, commandType, parameters));

            // Open the database connection if it isn't already opened
            if (state == ConnectionState.Closed) connection.Open();

            // Change Database - ONLY if 'database' is not null.
            if (database != null) connection.ChangeDatabase(database);

            // Fill DataTable
            adapter.Fill(value);

            // If the database connection was closed before the method call, close it again
            if (state == ConnectionState.Closed) connection.Close();

            return value;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //// PRIVATE METHODS ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        private SqlCommand BuildCommand(string commandText, SqlConnection connection, 
            CommandType commandType,
            IEnumerable<SqlParameter> parameters)
        {
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            command.CommandTimeout = SqlCommandTimeout;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
   
      
    }

}
