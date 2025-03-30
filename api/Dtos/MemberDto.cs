using api.Extensions;
using api.Models;

namespace api.Dtos
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool varified { get; set; } = false;
        public DateOnly DateOfBirth { get; set; }
        public string? KnownAs { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; } 
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public string? LookingFor { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<PhotoDto>? Photos { get; set; }
        
    }
}
