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
        protected IDatabaseContext DatabaseUtility { get; set; }
        protected IConfiguration Configuration { get; set; }

        public BaseRepository(IDatabaseContext databaseUtility, IConfiguration configuration)
        {
            DatabaseUtility = databaseUtility;
            Configuration = configuration;
        }
    }
}
