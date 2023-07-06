using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        
        public string? FirstName { get; set; }
        
        [MaxLength(50)]
        public string? LastName { get; set;}

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? RepeatPassword { get; set; }

        public string? NewPassword { get; set; }

        public UserDto()
        {
            
        }

        public UserDto(User entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            Password = "";
            NewPassword = "";
            RepeatPassword = "";
        }
    }
}
