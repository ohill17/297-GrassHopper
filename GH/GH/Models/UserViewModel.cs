using GH.Models;
using Microsoft.AspNetCore.Identity;

namespace GH.Models
{
    public class UserViewModel
    {
        private IEnumerable<AppUserModel> _users = new List<AppUserModel>();
        private IEnumerable<IdentityRole> _roles = new List<IdentityRole>();

        public IEnumerable<AppUserModel> Users { get => _users; set => _users = value; }
        public IEnumerable<IdentityRole> Roles { get => _roles; set => _roles = value; }
    }
}