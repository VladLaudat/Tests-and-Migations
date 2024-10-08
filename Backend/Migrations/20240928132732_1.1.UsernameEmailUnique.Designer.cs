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
    [Migration("20240928132732_1.1.UsernameEmailUnique")]
    partial class _11UsernameEmailUnique
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

                    b.HasIndex("UserName", "Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ce1b5797-5960-49f5-a4d5-5b24b899de37"),
                            Email = "test1@yahoo.com",
                            IsAdmin = true,
                            Password = "password1",
                            UserName = "Test1"
                        },
                        new
                        {
                            Id = new Guid("daf2d4e3-a84c-4324-8a6a-6fd70a631007"),
                            Email = "test2@yahoo.com",
                            IsAdmin = false,
                            Password = "password2",
                            UserName = "Test2"
                        },
                        new
                        {
                            Id = new Guid("532e6206-8f6c-4698-96c8-8d1d05696f0b"),
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
