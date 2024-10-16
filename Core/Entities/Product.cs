﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public required string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public string? pictureUrl { get; set; }
        public ProductType? productType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand? productBrand    { get; set; }
        public int ProductBrandId { get; set; }
    }
}
