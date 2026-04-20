namespace AgriSmartSierra.Application.DTOs;

public class MarketplaceListingDto
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid SellerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal AvailableQuantity { get; set; }
    public decimal? MinimumOrderQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? QualityGrade { get; set; }
    public bool IsOrganic { get; set; }
    public string CropName { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateMarketplaceListingDto
{
    public Guid CropId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public string Unit { get; set; } = "kg";
    public decimal AvailableQuantity { get; set; }
    public decimal? MinimumOrderQuantity { get; set; }
    public IFormFile? Image { get; set; }
    public string? QualityGrade { get; set; }
    public bool IsOrganic { get; set; }
}

public class UpdateMarketplaceListingDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? PricePerUnit { get; set; }
    public decimal? AvailableQuantity { get; set; }
    public string? Status { get; set; }
}

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? DeliveryAddress { get; set; }
    public string? DeliveryNotes { get; set; }
    public DateTime? OrderedAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderDto
{
    public Guid ListingId { get; set; }
    public decimal Quantity { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? DeliveryNotes { get; set; }
}

public class UpdateOrderStatusDto
{
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class CropPriceDto
{
    public string CropName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public decimal PreviousPrice { get; set; }
    public decimal PriceChangePercent { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}