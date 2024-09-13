using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Model
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public Guid Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        // Navigation properties
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

    }
}
