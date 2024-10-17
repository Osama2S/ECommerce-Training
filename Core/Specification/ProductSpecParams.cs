using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        public string? sort { get; set; }
        public int pageIndex { get; set; } = 1;
        private int pageSize =15;
        public int maxPageSize { get; set; } = 25;
        public int PageSize
        {
            get => pageSize;
            set => pageSize =value>maxPageSize?maxPageSize:value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private string? search;
        public string? Search
        {
            get => search;
            set => search=value!.ToLower();
        }
    }
}
