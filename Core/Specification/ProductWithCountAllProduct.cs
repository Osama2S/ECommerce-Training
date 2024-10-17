using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithCountAllProduct : BaseSpecification<Product>
    {
        public ProductWithCountAllProduct(ProductSpecParams @params) :base(
              x => (string.IsNullOrEmpty(@params.Search)||x.name.ToLower().Contains(@params.Search))&&
            (!@params.BrandId.HasValue||x.ProductBrandId==@params.BrandId)&&
            (!@params.TypeId.HasValue||x.ProductTypeId==@params.TypeId)) 
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
        }
    }
}
