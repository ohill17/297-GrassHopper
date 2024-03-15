using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using GH.Data;
using GH.Models;

namespace GH.Data
{
    public class AppDbContext : IdentityDbContext<AppUserModel> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<PhotoModel> Photos { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
    }
}
