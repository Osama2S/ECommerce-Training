namespace API.DTO
{
    public class OrderDto
    {
        public int BasketId {  get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO? ShipToAddress { get; set; }
    }
}
