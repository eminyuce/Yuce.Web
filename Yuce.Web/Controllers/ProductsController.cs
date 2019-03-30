using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yuce.Domain.Services.IServices;
using Yuce.Models.Entities;

namespace Yuce.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService ProductService { set; get; }
        private ILogger<ProductsController> Logger { get; set; }

        public ProductsController(IProductService productService, ILoggerFactory loggerFactory)
        {
            ProductService = productService;
            Logger = loggerFactory.CreateLogger<ProductsController>();
        }

        public IActionResult Index()
        {
            var returnList = ProductService.GetProducts();
            return View(returnList);
        }
        public IActionResult Details(int id)
        {
            var returnItem = ProductService.GetProduct(id);
            return View(returnItem);
        }
        public IActionResult SaveOrUpdate(int id=0)
        {
            var item = new Product();
            if(id > 0)
            {
                item = ProductService.GetProduct(id);
            }

            return View(item);
        }
        [HttpPost]
        public IActionResult SaveOrUpdate(Product product)
        {
            int id = ProductService.SaveOrUpdate(product);
            return RedirectToAction("Index");
        }
    }
}