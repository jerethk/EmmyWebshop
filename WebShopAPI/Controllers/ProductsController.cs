using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private myshopContext _shopContext;

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            _shopContext = new myshopContext();

            List<Product> productList = (from Product p in _shopContext.Products
                                         select p).ToList();

            return productList;
        }

        [HttpGet("{category}")]
        public IEnumerable<Product> GetByCategory(string category)
        {
            _shopContext = new myshopContext();

            List<Product> productList = (from Product p in _shopContext.Products
                                         where p.Category == category
                                         select p).ToList();

            return productList;
        }
    }
}
