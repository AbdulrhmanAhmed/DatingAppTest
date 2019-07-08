using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "This Field is Required")]
        [MaxLength(10, ErrorMessage = "max length is 10")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}