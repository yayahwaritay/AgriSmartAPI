namespace AgriSmartSierra.Domain.Entities;

public class Farm
{
    public Guid Id { get; set; }
    public Guid FarmerProfileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal SizeHectares { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? SoilType { get; set; }
    public string? Topography { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public FarmerProfile FarmerProfile { get; set; } = null!;
    public ICollection<Crop> Crops { get; set; } = new List<Crop>();
}