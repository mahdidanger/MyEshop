﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEshop.Data;

namespace MyEshop.Migrations
{
    [DbContext(typeof(MyEshopContext))]
    [Migration("20240814102155_SeedDataCategoryMig")]
    partial class SeedDataCategoryMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyEshop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Asp.Net Core 3.1",
                            Name = "Asp.Net Core"
                        },
                        new
                        {
                            Id = 2,
                            Description = "I AM DEVELOPER",
                            Name = "Mahdi"
                        },
                        new
                        {
                            Id = 3,
                            Description = "I AM TRADER",
                            Name = "Hasan"
                        },
                        new
                        {
                            Id = 4,
                            Description = "I AM EDITOR",
                            Name = "Alireza"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
