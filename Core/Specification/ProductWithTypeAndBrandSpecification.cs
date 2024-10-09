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
        public ProductWithTypeAndBrandSpecification(string sort,int? brandid,int? typeid) :base(
            x=>(!brandid.HasValue||x.ProductBrandId==brandid)&&
            (!typeid.HasValue||x.ProductTypeId==typeid)
            )
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
            AddOrderBy(x => x.name);
            if (!string.IsNullOrEmpty(sort)) {
                switch (sort)
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
