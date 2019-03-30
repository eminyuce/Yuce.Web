using System.Collections.Generic;
using Yuce.Models.Entities;

namespace Yuce.Domain.Repositories.IRepositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        int SaveOrUpdate(Product item);
    }
}