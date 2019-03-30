using System;
using System.Collections.Generic;
using System.Text;

namespace Yuce.Domain.Repositories
{
    public abstract class BaseRepository
    {
        public string ConnectionString { get; set; }
        public static string ConnectionStringKey = "DefaultConnection";
        protected BaseRepository()
        {

        }
    }
}
