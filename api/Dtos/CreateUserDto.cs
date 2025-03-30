using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; internal set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string KnownAs { get; set; }
        public string LookingFor { get;  set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
