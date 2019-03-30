using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yuce.Domain.Services.IServices;

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
    }
}