using AgriSmartSierra.Domain.Entities;

namespace AgriSmartSierra.Application.DTOs;

public class CropDto
{
    public Guid Id { get; set; }
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Variety { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal AreaPlanted { get; set; }
    public decimal EstimatedYield { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime PlantingDate { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateCropDto
{
    public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Variety { get; set; } = string.Empty;
    public string Category { get; set; } = "Vegetable";
    public decimal AreaPlanted { get; set; }
    public decimal EstimatedYield { get; set; }
    public string Unit { get; set; } = "kg";
    public DateTime PlantingDate { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
}

public class UpdateCropDto
{
    public string? Name { get; set; }
    public string? Variety { get; set; }
    public string? Category { get; set; }
    public decimal? AreaPlanted { get; set; }
    public decimal? EstimatedYield { get; set; }
    public string? Unit { get; set; }
    public string? Status { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
}

public class FarmDto
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
}

public class CreateFarmDto
{
    public string Name { get; set; } = string.Empty;
    public decimal SizeHectares { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? SoilType { get; set; }
    public string? Topography { get; set; }
}

public class CropActivityDto
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid? AssignedTo { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCropActivityDto
{
    public Guid CropId { get; set; }
    public Guid? AssignedTo { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public decimal? Cost { get; set; }
    public string? Notes { get; set; }
}

public class CropCalendarDto
{
    public Guid CropId { get; set; }
    public string CropName { get; set; } = string.Empty;
    public DateTime PlantingDate { get; set; }
    public DateTime? ExpectedHarvestDate { get; set; }
    public List<CropActivityDto> UpcomingActivities { get; set; } = new();
    public List<CropActivityDto> OverdueActivities { get; set; } = new();
}