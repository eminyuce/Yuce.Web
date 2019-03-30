using HelpersProject;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using Yuce.Domain.DB;
using Yuce.Domain.Repositories.IRepositories;
using Yuce.Models.Entities;

namespace Yuce.Domain.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private IDatabaseUtility DatabaseUtility { get; set; }
        private IConfiguration Configuration { get; set; }

        public ProductRepository(IDatabaseUtility databaseUtility, IConfiguration configuration)
        {
            DatabaseUtility = databaseUtility;
            Configuration = configuration;
           

        }

        public List<Product> GetProducts()
        {
            var list = new List<Product>();
            String commandText = @"SELECT * FROM products ORDER BY Id DESC";
            var parameterList = new List<DbParameter>();
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(ConnectionStringKey, commandText, commandType, parameterList);
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetNwmproductFromDataRow(dr);
                        list.Add(e);
                    }
                }
            }
            return list;
        }

        private Product GetNwmproductFromDataRow(DataRow dr)
        {
            var item = new Product();

            item.Id = dr["Id"].ToInt();
            item.Name = dr["Name"].ToStr();
            return item;
        }
    }
}
