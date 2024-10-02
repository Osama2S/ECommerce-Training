using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }
        //[HttpGet]
        //public ActionResult<List<Product>> GetAllProducts()
        //{
        //    List<Product> products = [
        //        new Product {id=1,name="Product 1"},
        //        new Product {id=2,name="Product 2"},
        //        new Product {id=3,name="Product 3"},
        //        new Product {id=4,name="Product 4"},
        //        ];
        //    return products;
        //}

    }
}
