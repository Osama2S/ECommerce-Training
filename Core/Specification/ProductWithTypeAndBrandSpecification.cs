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
        public ProductWithTypeAndBrandSpecification()
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
        }
        public ProductWithTypeAndBrandSpecification(int id):base(x=>x.Id==id)
        {
            AddIncludes(x => x.productBrand!);
            AddIncludes(x => x.productType!);
        }
    }
}
