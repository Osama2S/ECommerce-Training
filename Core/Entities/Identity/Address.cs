namespace Core.Entities.Identity
{
    public class Address
    {
#nullable disable
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string AppUserid { get; set; }
        public AppUser AppUser { get; set; }
    }
}