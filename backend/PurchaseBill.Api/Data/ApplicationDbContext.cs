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

    public DbSet<LocationDetail> LocationDetails
        => Set<LocationDetail>();

    public DbSet<PurchaseBillItem> PurchaseBillItems
        => Set<PurchaseBillItem>();

    public DbSet<PurchaseOrder> PurchaseOrders
        => Set<PurchaseOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==========================================
        // Location Details
        // ==========================================

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


        // ==========================================
        // Purchase Bill Items
        // ==========================================

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


        // ==========================================
        // Purchase Orders
        // ==========================================

        modelBuilder.Entity<PurchaseOrder>()
            .ToTable("Purchase_Orders");

        modelBuilder.Entity<PurchaseOrder>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<PurchaseOrder>()
            .Property(x => x.NetAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PurchaseOrder>()
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");


        // ==========================================
        // Purchase Order -> Purchase Bill Items
        // One Purchase Order has many Items
        // ==========================================

        modelBuilder.Entity<PurchaseOrder>()
            .HasMany(x => x.Items)
            .WithOne(x => x.PurchaseOrder)
            .HasForeignKey(x => x.PurchaseOrderId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}