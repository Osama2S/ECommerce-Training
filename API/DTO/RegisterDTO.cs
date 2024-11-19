using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$",ErrorMessage ="Enter valid password")]
        public required string DisplayName { get; set; }
    }
}
