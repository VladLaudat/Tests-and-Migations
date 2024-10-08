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
    [Migration("20240905112630_1.1.EmailAdress")]
    partial class _11EmailAdress
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

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("520a3d1d-e370-4bd9-b8cc-8f3892a11738"),
                            Email = "test1@yahoo.com",
                            IsAdmin = true,
                            Password = "password1",
                            UserName = "Test1"
                        },
                        new
                        {
                            Id = new Guid("323f8a3f-509d-455b-9ab7-b6279e571ef2"),
                            Email = "test2@yahoo.com",
                            IsAdmin = false,
                            Password = "password2",
                            UserName = "Test2"
                        },
                        new
                        {
                            Id = new Guid("8789e8a4-2bce-4114-b6b7-ceb3408404d9"),
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
