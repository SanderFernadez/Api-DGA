using Api_DGA.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace APi_DGA_Infrastructure.Contexts
{
    public class InfrastructureContext : DbContext
    {
        public InfrastructureContext(DbContextOptions<InfrastructureContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleProduct> SaleProducts => Set<SaleProduct>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables Configuration
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Sale>().ToTable("Sales");
            modelBuilder.Entity<SaleProduct>().ToTable("SaleProducts");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Sale>().HasKey(s => s.Id);
            modelBuilder.Entity<SaleProduct>().HasKey(sp => new { sp.SaleId, sp.ProductId });
            #endregion

            #region Relationships
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SaleProducts)
                .HasForeignKey(sp => sp.ProductId);
            #endregion

            #region Property Configurations
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleProduct>()
                .Property(sp => sp.UnitPrice)
                .HasColumnType("decimal(18,2)");
            #endregion
        }
    }
}
