namespace AgriSmartSierra.Domain.Entities;

public class FarmerProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public decimal FarmSizeHectares { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string District { get; set; } = string.Empty;
    public string? SoilType { get; set; }
    public string? WaterSource { get; set; }
    public string? IrrigationType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<Farm> Farms { get; set; } = new List<Farm>();
}