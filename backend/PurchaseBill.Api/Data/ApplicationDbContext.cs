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
    }
}