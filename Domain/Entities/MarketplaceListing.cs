namespace AgriSmartSierra.Domain.Entities;

public class MarketplaceListing
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid SellerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public string Unit { get; set; } = "kg";
    public decimal AvailableQuantity { get; set; }
    public decimal? MinimumOrderQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public ListingStatus Status { get; set; }
    public string? QualityGrade { get; set; }
    public bool IsOrganic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public Crop Crop { get; set; } = null!;
    public User Seller { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public enum ListingStatus
{
    Active,
    Pending,
    Sold,
    Expired,
    Cancelled
}