using IstanbulNs.Repositories.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static IstanbulNs.Repositories.Enums.Gender;
using static IstanbulNs.Repositories.Enums.Roles;

namespace IstanbulNs.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Sex SexId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        [MaxLength(200)]
        public string Token { get; set; }
        public int IsDisabled { get; set; }
        public Role Role { get; set; }
    }
}
