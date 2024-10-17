using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(ProductSpecParams @params) :base(
            x=>(string.IsNullOrEmpty(@params.Search)||x.name.ToLower().Contains(@params.Search))&&
            (!@params.BrandId.HasValue||x.ProductBrandId==@params.BrandId)&&
            (!@params.TypeId.HasValue||x.ProductTypeId==@params.TypeId)
            )
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
            ApplyPaging(@params.PageSize, (@params.PageSize*(@params.pageIndex-1)));
            if (!string.IsNullOrEmpty(@params.sort)) {
                switch (@params.sort)
                {
                    case "priceAsc":
                        AddOrderBy(x=>x.price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(x=>x.price);
                        break;
                    default:
                        AddOrderBy(x => x.name);
                        break;
                }
            }
        }
        public ProductWithTypeAndBrandSpecification(int id):base(x=>x.Id==id)
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
        }
    }
}
