using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public required int Quantity { get; set; }
        public required string Brand { get; set; }
        public required string Type { get; set; }

    }
}
