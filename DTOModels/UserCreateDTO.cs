using System.ComponentModel.DataAnnotations;

namespace Commander.DTOModels
{
    public class UserCreateDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}