using System.Collections.Generic;
using Yuce.Models.Entities;

namespace Yuce.Domain.Services.IServices
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}