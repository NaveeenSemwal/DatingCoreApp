using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Models
{
    public class AppUser
    {
        public required string Username { get; set; }
        public required string KnownAs { get; set; }
        public required string Token { get; set; }
        public required string Gender { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
