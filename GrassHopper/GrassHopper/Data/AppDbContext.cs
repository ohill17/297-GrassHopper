using GrassHopper.Data;
using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace GrassHopper.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoGroup> PhotoGroups { get; set; }
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<Token> Tokens { get; set; }
    }
}
