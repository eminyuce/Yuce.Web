using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Yuce.Domain.DB;

namespace Yuce.Domain.Repositories
{
    public abstract class BaseRepository
    {
        public string ConnectionString { get; set; }
        public static string ConnectionStringKey = "DefaultConnection";
        protected IDatabaseUtility DatabaseUtility { get; set; }
        protected IConfiguration Configuration { get; set; }

        public BaseRepository(IDatabaseUtility databaseUtility, IConfiguration configuration)
        {
            DatabaseUtility = databaseUtility;
            Configuration = configuration;
        }
    }
}
