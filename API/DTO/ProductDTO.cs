namespace API.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public string? pictureUrl { get; set; }
        public string? productType { get; set; }
        public string? productBrand { get; set; }

    }
}
