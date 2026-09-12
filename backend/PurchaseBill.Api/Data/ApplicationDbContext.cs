using Microsoft.EntityFrameworkCore;
using PurchaseBill.Api.Models;

namespace PurchaseBill.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LocationDetail> LocationDetails => Set<LocationDetail>();

    public DbSet<PurchaseBillItem> PurchaseBillItems => Set<PurchaseBillItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LocationDetail>()
            .ToTable("Location_Details");

        modelBuilder.Entity<LocationDetail>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<LocationDetail>()
            .Property(x => x.LocationCode)
            .HasColumnName("Location_Code")
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<LocationDetail>()
            .Property(x => x.LocationName)
            .HasColumnName("Location_Name")
            .HasMaxLength(200)
            .IsRequired();


        // Purchase Bill Item configuration
        modelBuilder.Entity<PurchaseBillItem>()
            .ToTable("Purchase_Bill_Items");

        modelBuilder.Entity<PurchaseBillItem>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.Item)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.Batch)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.StandardCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.StandardPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.Discount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.TotalCost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PurchaseBillItem>()
            .Property(x => x.TotalSelling)
            .HasColumnType("decimal(18,2)");
    }
}