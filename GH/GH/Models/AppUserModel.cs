using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GH.Models
{
    public class AppUserModel : IdentityUser
    {
        public string? Name { get; set; }


        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}
