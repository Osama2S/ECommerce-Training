using API.DTO;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _repositoryProduct;
        private readonly IGenericRepository<ProductBrand> _repositoryProductBrand;
        private readonly IGenericRepository<ProductType> _repositoryProductType;
        public ProductController(IGenericRepository<Product> repositoryProduct,
            IGenericRepository<ProductBrand> repositoryProductBrand,
            IGenericRepository<ProductType> repositoryProductType,
             IMapper mapper)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryProductBrand = repositoryProductBrand;
            _repositoryProductType = repositoryProductType;
            _mapper = mapper;
        }
        [HttpGet]   
        public async Task<IReadOnlyList<ProductDTO>> GetProducts([FromQuery]string? sort, [FromQuery]  int ?brandid, [FromQuery] int?typeid)
        {
            var spec = new ProductWithTypeAndBrandSpecification(sort!, brandid, typeid);
            IReadOnlyList<Product> products = await _repositoryProduct.ListAsync(spec);
            List<ProductDTO> productsDTO = _mapper.Map<List<ProductDTO>>(products);
            return productsDTO;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id) {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            Product product = await _repositoryProduct.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new APIResponse(404));
            ProductDTO productDTO=_mapper.Map<ProductDTO>(product);
            return productDTO;
        }
        [HttpGet("GetBrands")]
        public async Task<IReadOnlyList<ProductBrand>> GetBrands()
        {

            return await _repositoryProductBrand.GetAllAsync();
        }
        [HttpGet("GetTypes")]
        public async Task<IReadOnlyList<ProductType>> GetTypes()
        {
            return await _repositoryProductType.GetAllAsync();
        }

    }
}
