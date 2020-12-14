using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class RoleModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Role { get; set; }

        public virtual  ICollection<UserModel> Users { get; set; }
    }
}