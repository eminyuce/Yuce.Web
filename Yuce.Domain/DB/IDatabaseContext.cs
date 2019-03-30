using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Yuce.Domain.DB
{
    public interface IDatabaseContext
    {
    
        int SqlCommandTimeout { get; set; }
        DataSet ExecuteDataSet(string commandText,CommandType commandType, List<DbParameter> parameters);
        DbParameter GetDbParameter(string parameterName, object value, SqlDbType sqlDbType);
        int ExecuteScalar(string commandText, CommandType commandType, List<DbParameter> dbParameter);
        int SaveOrUpdate(string commandText, CommandType commandType, List<DbParameter> dbParameter);
    }
}