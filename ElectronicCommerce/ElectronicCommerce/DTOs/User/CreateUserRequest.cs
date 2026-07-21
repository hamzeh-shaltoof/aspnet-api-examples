using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace ElectronicCommerce.DTO.User
{
    public class 
        CreateUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; } = null!;
    }
}
