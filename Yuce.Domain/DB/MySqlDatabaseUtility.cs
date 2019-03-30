﻿using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Yuce.Domain.DB
{

    public class MySqlDatabaseUtility : DatabaseUtility, IDatabaseUtility
    {
        public MySqlDatabaseUtility(IConfiguration configuration) : base(configuration)
        {

        }

        public DataSet ExecuteDataSet(String connectionStringKey, 
            string commandText, 
            CommandType commandType, 
            List<DbParameter> parameters)
        {
            string connectionString = Configuration.GetConnectionString(connectionStringKey);
            return ExecuteDataSet(new MySqlConnection(connectionString), null, commandText, commandType,
                parameters.Cast<MySqlParameter>());
        }

        public static DataSet ExecuteDataSet(MySqlConnection connection, string database, string commandText,
            CommandType commandType,
            IEnumerable<MySqlParameter> parameters)
        {
            if (connection == null) throw new Exception("Connection must be established before query can be run.");
            ConnectionState state = connection.State;
            var value = new DataSet();

            // Build Adapter
            var adapter = new MySqlDataAdapter(BuildCommand(commandText, connection, commandType, parameters));

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
        private static MySqlCommand BuildCommand(string commandText, 
            MySqlConnection connection,
            CommandType commandType, 
            IEnumerable<MySqlParameter> parameters)
        {
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (MySqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
    }

}