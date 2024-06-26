﻿using GrassHopper.Data;
using GrassHopper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace GrassHopper.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoGroup> PhotoGroups { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<AppSettings> AppSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.PortfolioPGroups)
                .WithMany(g => g.AssocPortfolios);
            base.OnModelCreating(modelBuilder);
        }
    }
}
