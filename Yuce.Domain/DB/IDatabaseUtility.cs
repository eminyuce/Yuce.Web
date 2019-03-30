using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Yuce.Domain.DB
{
    public interface IDatabaseUtility
    {
        int SqlCommandTimeout { get; set; }
        DataSet ExecuteDataSet(String connectionStringKey, 
            string commandText, 
            CommandType commandType,
            List<DbParameter> parameters);
        DbParameter GetDbParameter(string parameterName, object value, SqlDbType sqlDbType);
    }
}