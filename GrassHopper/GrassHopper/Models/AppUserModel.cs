﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    public class AppUserModel : IdentityUser
    {
        public string? Name { get; set; }

        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}