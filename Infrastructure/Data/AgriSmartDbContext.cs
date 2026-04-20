using AgriSmartSierra.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriSmartSierra.Infrastructure.Data;

public class AgriSmartDbContext : DbContext
{
    public AgriSmartDbContext(DbContextOptions<AgriSmartDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<FarmerProfile> FarmerProfiles => Set<FarmerProfile>();
    public DbSet<BuyerProfile> BuyerProfiles => Set<BuyerProfile>();
    public DbSet<AgronomistProfile> AgronomistProfiles => Set<AgronomistProfile>();
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Crop> Crops => Set<Crop>();
    public DbSet<CropActivity> CropActivities => Set<CropActivity>();
    public DbSet<PestReport> PestReports => Set<PestReport>();
    public DbSet<WeatherLog> WeatherLogs => Set<WeatherLog>();
    public DbSet<MarketplaceListing> MarketplaceListings => Set<MarketplaceListing>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ForumPost> ForumPosts => Set<ForumPost>();
    public DbSet<ForumComment> ForumComments => Set<ForumComment>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<InsuranceInfo> InsuranceInfos => Set<InsuranceInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasConversion<string>();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<FarmerProfile>(entity =>
        {
            entity.ToTable("FarmerProfiles");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User).WithOne(u => u.FarmerProfile).HasForeignKey<FarmerProfile>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BuyerProfile>(entity =>
        {
            entity.ToTable("BuyerProfiles");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User).WithOne(u => u.BuyerProfile).HasForeignKey<BuyerProfile>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AgronomistProfile>(entity =>
        {
            entity.ToTable("AgronomistProfiles");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User).WithOne(u => u.AgronomistProfile).HasForeignKey<AgronomistProfile>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.ToTable("Farms");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.FarmerProfile).WithMany(f => f.Farms).HasForeignKey(e => e.FarmerProfileId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Crop>(entity =>
        {
            entity.ToTable("Crops");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Category).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Farm).WithMany(f => f.Crops).HasForeignKey(e => e.FarmId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CropActivity>(entity =>
        {
            entity.ToTable("CropActivities");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Crop).WithMany(c => c.Activities).HasForeignKey(e => e.CropId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.AssignedUser).WithMany().HasForeignKey(e => e.AssignedTo).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PestReport>(entity =>
        {
            entity.ToTable("PestReports");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Crop).WithMany().HasForeignKey(e => e.CropId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.ReportedBy).WithMany().HasForeignKey(e => e.ReportedById).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WeatherLog>(entity =>
        {
            entity.ToTable("WeatherLogs");
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<MarketplaceListing>(entity =>
        {
            entity.ToTable("MarketplaceListings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Crop).WithMany(c => c.MarketplaceListings).HasForeignKey(e => e.CropId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Seller).WithMany().HasForeignKey(e => e.SellerId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Listing).WithMany(l => l.Orders).HasForeignKey(e => e.ListingId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Buyer).WithMany().HasForeignKey(e => e.BuyerId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.ToTable("ForumPosts");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.ToTable("ForumComments");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Post).WithMany(p => p.Comments).HasForeignKey(e => e.PostId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.ParentComment).WithMany(c => c.Replies).HasForeignKey(e => e.ParentCommentId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.ToTable("LoanApplications");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasOne(e => e.Farmer).WithMany().HasForeignKey(e => e.FarmerId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InsuranceInfo>(entity =>
        {
            entity.ToTable("InsuranceInfos");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Farmer).WithMany().HasForeignKey(e => e.FarmerId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}