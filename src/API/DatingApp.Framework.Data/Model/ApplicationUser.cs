using DatingApp.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Framework.Data.Model
{
    public class ApplicationUser : IdentityUser<int>
    {

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        public required string KnownAs { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public required string Gender { get; set; } = "";

        public string? Introducation { get; set; }

        public string? LookingFor { get; set; }

        public string? Interests { get; set; }

        public required string City { get; set; } = "New Delhi";

        public required string Country { get; set; } = "India";

        public List<Photo> Photos { get; set; } = [];

        // Navigation property
        // public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }

    }
}

