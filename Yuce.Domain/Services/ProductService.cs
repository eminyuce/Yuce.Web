using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Yuce.Domain.Repositories.IRepositories;
using Yuce.Domain.Services.IServices;
using Yuce.Models.Entities;

namespace Yuce.Domain.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository ProductRepository { get; set; }
        private IConfiguration Configuration { get; set; }

        public ProductService(IProductRepository productRepository, IConfiguration configuration)
        {
            ProductRepository = productRepository;
            Configuration = configuration;
        }
        public List<Product> GetProducts()
        {
            return ProductRepository.GetProducts();
        }

    }
}
