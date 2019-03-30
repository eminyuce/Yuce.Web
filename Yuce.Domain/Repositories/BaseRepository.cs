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
        protected IDatabaseContext DatabaseContext { get; set; }
        protected IConfiguration Configuration { get; set; }

        public BaseRepository(IDatabaseContext databaseContext, IConfiguration configuration)
        {
            DatabaseContext = databaseContext;
            Configuration = configuration;
        }
    }
}
