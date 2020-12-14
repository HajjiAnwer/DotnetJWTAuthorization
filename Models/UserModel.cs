using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Models
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]

        public virtual  RoleModel Role { get; set; }
        
    }
}