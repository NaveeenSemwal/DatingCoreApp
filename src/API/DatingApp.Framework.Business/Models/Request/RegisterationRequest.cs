using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Models.Request
{
    public class RegisterationRequest
    {
        [Required]
        public string Username { get; set; }

        // [Required]
        // public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Role { get; set; } = "User";

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Introducation { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
