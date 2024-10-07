﻿// <auto-generated />
using System;
using Backend.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(BackendDBContext))]
    [Migration("20240928131930_1.1.EmailAdressUnique")]
    partial class _11EmailAdressUnique
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.DbContext.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a57cc43d-1d79-496f-a1ea-3eb0e553a396"),
                            Email = "test1@yahoo.com",
                            IsAdmin = true,
                            Password = "password1",
                            UserName = "Test1"
                        },
                        new
                        {
                            Id = new Guid("c1c911ba-8b15-449f-ae46-b873a27129a2"),
                            Email = "test2@yahoo.com",
                            IsAdmin = false,
                            Password = "password2",
                            UserName = "Test2"
                        },
                        new
                        {
                            Id = new Guid("27bff935-3583-4120-ae10-a43be1210ea9"),
                            Email = "test3@yahoo.com",
                            IsAdmin = false,
                            Password = "password3",
                            UserName = "Test3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
