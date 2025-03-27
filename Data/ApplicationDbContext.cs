using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Data.Models.ImageTables;
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
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<CategoryImages> CategoryImages { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Global Ignore Soft Deleted Products For Data Recording
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            // Global hide favorites if product is soft-deleted
            modelBuilder.Entity<Favorites>().HasQueryFilter(p => !p.Product.IsDeleted);

            // ProductImage To Products  Many To One
            modelBuilder.Entity<Product>()
                .HasMany( p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // CategoryImages to Category One to One
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Images)
                .WithOne(ci => ci.Category)
                .HasForeignKey<CategoryImages>(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // CategoryImages Ensure Unique to Each Category
            modelBuilder.Entity<CategoryImages>()
                .HasIndex(ci => ci.CategoryId)
                .IsUnique();    
            // Set Category Name as Unique to prevent duplicates
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();
            // User and Cart ( One To One ) 
            modelBuilder.Entity<ApplicationUser>()
                .HasOne( u => u.Cart)
                .WithOne( c => c.User)
                .HasForeignKey<Cart>(c=> c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // User and Purchase (One To Many)
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            // Cart and CartItems ( One To Many ) 
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            // CartItems and Product ( Many to One) 
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Purchase and PurchaseItem ( One To Many)
            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchaseItems)
                .WithOne(pi => pi.Purchase)
                .HasForeignKey(pi => pi.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite Key
            modelBuilder.Entity<PurchaseItem>()
              .HasKey(pi => new { pi.PurchaseId, pi.ProductId });

        

            // DO NOT FORGET ON QUERIES NOT GLOBALLY IGNORE FILTER For DELETED Purchase Products
            // PurchaseItem and Product (Many to One)
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
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
            modelBuilder.Entity<CategoryImages>()
                .Property(ci => ci.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductImages>()
                .Property(pi => pi.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Cart>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.Id)
                .ValueGeneratedOnAdd();
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
