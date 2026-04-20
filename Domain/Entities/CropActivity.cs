namespace AgriSmartSierra.Domain.Entities;

public class CropActivity
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid? AssignedTo { get; set; }
    public ActivityType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public ActivityStatus Status { get; set; }
    public decimal? Cost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Crop Crop { get; set; } = null!;
    public User? AssignedUser { get; set; }
}

public enum ActivityType
{
    Planting,
    Watering,
    Fertilizing,
    PestControl,
    Weeding,
    Pruning,
    Harvesting,
    SoilPreparation,
    Irrigation,
    Inspection
}

public enum ActivityStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled,
    Delayed
}