﻿// <auto-generated />
using System;
using GrassHopper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrassHopper.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240408233313_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GrassHopper.Models.PhotoModel", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PhotoCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PhotoId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("GrassHopper.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ReviewBody")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("ReviewDate")
                        .HasColumnType("date");

                    b.Property<int>("ReviewRating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ReviewID");

                    b.ToTable("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}