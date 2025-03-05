using FluentBlazor_Project.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
       public DbSet<PurchaseItem> PurchaseItems { get; set; }


        public DbSet<Favorites> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            // User and Purchase (One To Many)
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Purchase and PurchaseItem ( One To Many)
            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchaseItems)
                .WithOne(pi => pi.Purchase)
                .HasForeignKey(pi => pi.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite Key
            modelBuilder.Entity<PurchaseItem>()
              .HasKey(pi => new { pi.PurchaseId, pi.ProductId });

            modelBuilder.Entity<Favorites>()
                .HasKey(f => new { f.UserId, f.ProductId });

            // PurchaseItem and Product (Many to One)
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            // User And Favorites ( One To Many )

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Favorites to Product ( Many To one) 

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Product)
                .WithMany()
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Precision Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            // Generate Key in Database
            modelBuilder.Entity<Product>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<Purchase>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Favorites>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
