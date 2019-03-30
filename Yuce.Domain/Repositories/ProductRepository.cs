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
        public ProductRepository(IDatabaseContext databaseContext, IConfiguration configuration) : base(databaseContext, configuration)
        {

        }

        public Product GetProduct(int id)
        {
            var item = new Product();
            String commandText = @"SELECT * FROM products ORDER BY Id DESC";
            var parameterList = new List<DbParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseContext.GetDbParameter("Id", id, SqlDbType.Int));
            DataSet dataSet = DatabaseContext.ExecuteDataSet(ConnectionStringKey, commandText, commandType, parameterList);
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetNwmproductFromDataRow(dr);
                        item = e;
                    }
                }
            }
            return item;
        }

        public List<Product> GetProducts()
        {
            var list = new List<Product>();
            String commandText = @"SELECT * FROM products ORDER BY Id DESC";
            var parameterList = new List<DbParameter>();
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseContext.ExecuteDataSet(ConnectionStringKey, commandText, commandType, parameterList);
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

        public int SaveOrUpdate(Product item)
        {
            String commandText = @"SaveOrUpdateProduct";
            var parameterList = new List<DbParameter>();
            var commandType = CommandType.StoredProcedure;
            parameterList.Add(DatabaseContext.GetDbParameter("id", item.Id, SqlDbType.Int));
            parameterList.Add(DatabaseContext.GetDbParameter("name", item.Name.ToStr(), SqlDbType.NVarChar));
            int id = DatabaseContext.SaveOrUpdate(ConnectionStringKey, commandText, commandType, parameterList).ToInt();
            return id;
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
