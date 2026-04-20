namespace AgriSmartSierra.Domain.Entities;

public class Crop
{
    public Guid Id { get; set; }
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Variety { get; set; } = string.Empty;
    public CropCategory Category { get; set; }
    public decimal AreaPlanted { get; set; }
    public decimal EstimatedYield { get; set; }
    public string Unit { get; set; } = "kg";
    public DateTime PlantingDate { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
    public CropStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Farm Farm { get; set; } = null!;
    public ICollection<CropActivity> Activities { get; set; } = new List<CropActivity>();
    public ICollection<MarketplaceListing> MarketplaceListings { get; set; } = new List<MarketplaceListing>();
}

public enum CropCategory
{
    Cereal,
    Grain,
    Vegetable,
    Fruit,
    Root,
    Legume,
    CashCrop
}

public enum CropStatus
{
    Planned,
    Planted,
    Growing,
    Flowering,
    Fruiting,
    Harvested,
    Failed
}