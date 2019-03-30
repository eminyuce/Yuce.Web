using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Yuce.Domain.DB
{
    public abstract class BaseContext
    {
        protected IConfiguration Configuration { get; set; }
        public static string ConnectionStringKey = "DefaultConnection";
        public BaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int SqlCommandTimeout { get; set; }
       

       

    }
}
