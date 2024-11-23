using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        public required string Email { get; set; }
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,16}$", ErrorMessage ="Enter valid password dd")]
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
    }
}
