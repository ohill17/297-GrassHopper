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
    [Migration("20240415025956_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GrassHopper.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("tinyint(1)");

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

                    b.HasIndex("GroupId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("GrassHopper.Models.PhotoGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GroupDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("GroupId");

                    b.ToTable("PhotoGroups");
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

            modelBuilder.Entity("GrassHopper.Models.Photo", b =>
                {
                    b.HasOne("GrassHopper.Models.PhotoGroup", "Group")
                        .WithMany("Photos")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("GrassHopper.Models.PhotoGroup", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
