using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Pages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "nothing";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public int AddToCart([FromBody] string productcode)
        {
            List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

            if (HttpContext.Session.GetString("cart") != null)
            {
                // deserialise existing shopping cart (List) from session state
                shoppingCart = JsonSerializer.Deserialize<List<ShoppingCartItem>>(HttpContext.Session.GetString("cart"));
            }

            // Load products from database
            var shopContext = new myshopContext();
            var productList = (from Product p in shopContext.Products
                               select p).ToList();

            // query the selected product
            var q = (from p in productList
                     where p.ProductCode == productcode
                     select p).ToList();

            if (q.Count > 0)
            {
                // check if product is already in cart
                Product p = q[0];
                int index = -1;
                for (int i = 0; i < shoppingCart.Count; i++)
                {
                    if (shoppingCart[i].product.ProductCode == p.ProductCode)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    // add product to cart
                    shoppingCart.Add(new ShoppingCartItem() { product = p, count = 1 });
                }
                else
                {
                    // increment existing item in cart
                    shoppingCart[index].count++;
                }
            }

            // serialise shopping cart and store in session
            string jsonCart = JsonSerializer.Serialize(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            // get cart item count
            int cartCount = 0;            
            foreach (ShoppingCartItem item in shoppingCart)
            {
                cartCount += item.count;
            }

            // return the cart item count
            return cartCount;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }
    }
}
