namespace Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public string ?AppUserid { get; set; }
        public AppUser? AppUser { get; set; }
    }
}