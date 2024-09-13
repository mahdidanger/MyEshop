using Microsoft.EntityFrameworkCore;
using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Data
{
    public class MyEshopContext : DbContext
    {
        public MyEshopContext(DbContextOptions<MyEshopContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data
            modelBuilder.Entity<CategoryToProduct>()
                .HasKey(t => new { t.ProductId, t.CategoryId });

            //modelBuilder.Entity<Product>(
            //    p =>
            //    {
            //        p.HasKey(p => p.Id);
            //        p.OwnsOne<Item>(p => p.Item);
            //        p.HasOne<Item>(p => p.Item).WithOne(p => p.Product)
            //            .HasForeignKey<Item>(p => p.Id);
            //    }
            //);

            modelBuilder.Entity<Item>(
                i =>
                {
                    i.Property(i => i.Price).HasColumnType("Money");
                }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Price = 3243.0M,
                    QuantityInStock = 5
                },
                new Item()
                {
                    Id = 2,
                    Price = 5654.0M,
                    QuantityInStock = 8
                },
                new Item()
                {
                    Id = 3,
                    Price = 5578.0M,
                    QuantityInStock = 3
                });


            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    ItemId = 1,
                    Name = "Asp.Net Core",
                    Description = "Best Fremwork In C#"
                },
                new Product()
                {
                    Id = 2,
                    ItemId = 2,
                    Name = "DJango",
                    Description = "Best Fremwork In PYTHON"
                },
                new Product()
                {
                    Id = 3,
                    ItemId = 3,
                    Name = "Laravel",
                    Description = "Best Fremwork In PHP"
                }
                );

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1 , ProductId = 1},
                new CategoryToProduct() { CategoryId = 2 , ProductId = 1},
                new CategoryToProduct() { CategoryId = 3 , ProductId = 1 },
                new CategoryToProduct() { CategoryId = 4 , ProductId = 1 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 3 }
            );

            
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Back-End",
                    Description = "For Web Developer"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Front-End",
                    Description = "For Web Developer"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Machin-Learning",
                    Description = "For AI Developer"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Android & ios",
                    Description = "For Mobile Programmer"
                });
             #endregion

            base.OnModelCreating(modelBuilder);


        }


    }
}
