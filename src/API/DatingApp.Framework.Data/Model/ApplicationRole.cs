using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Model
{
    [Table("ApplicationRole")]
    public class ApplicationRole : IdentityRole<int>
    {
        // Navigation property
        //public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
