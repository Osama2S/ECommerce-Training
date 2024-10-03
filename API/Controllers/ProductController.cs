using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]   
        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await _repository.GetProductsAsync();
        }
        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id) { 
            return await _repository.GetProductIdAsync(id);
        }
        [HttpGet]
        public async Task<IReadOnlyList<ProductBrand>> GetBrands()
        {
            return await _repository.GetProductBrandsAsync();
        }
        [HttpGet]
        public async Task<IReadOnlyList<ProductType>> GetTypes()
        {
            return await _repository.GetProductTypesAsync();
        }

    }
}
