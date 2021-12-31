using jschmitt2747ex1i.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Data
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ComponentProduct> ComponentProducts { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }
        public virtual DbSet<FinishedProduct> FinishedProducts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().ToTable("Product");
        //    modelBuilder.Entity<ComponentProduct>().ToTable("ComponentProduct");
        //    modelBuilder.Entity<UnitOfMeasure>().ToTable("UnitOfMeasure");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
            
                entity.ToTable("Products");
            });
            modelBuilder.Entity<UnitOfMeasure>(entity =>
            {
                entity.ToTable("UnitsOfMeasure");
            });
            modelBuilder.Entity<FinishedProduct>(entity =>
            {
                entity.HasKey(e => e.FinishedProductId);
                entity.ToTable("FinishedProducts");
                entity.HasMany<ComponentProduct>(e => e.ComponentProducts)
                .WithOne()
                .HasForeignKey(e => e.FinishedProductId);
                entity.HasOne<Category>(e => e.Category);
            });

            modelBuilder.Entity<ComponentProduct>(entity =>
            {
                entity.HasKey(e => e.ComponentProductId);
                entity.ToTable("ComponentProducts");
                entity.HasOne<FinishedProduct>(e => e.FinishedProduct)
                .WithMany(e => e.ComponentProducts)
                .HasForeignKey(e => e.FinishedProductId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
            });



        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().ToTable("Product");
        //    modelBuilder.Entity<ComponentProduct>().ToTable("ComponentProduct");
        //    modelBuilder.Entity<UnitOfMeasure>().ToTable("UnitOfMeasure");
        //}

        public DbSet<jschmitt2747ex1i.Models.ComponentsViewModel> ComponentsViewModel { get; set; }
    }
}
