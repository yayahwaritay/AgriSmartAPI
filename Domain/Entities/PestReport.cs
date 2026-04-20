namespace AgriSmartSierra.Domain.Entities;

public class PestReport
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid ReportedById { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? LocalImagePath { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? DetectedDisease { get; set; }
    public double? ConfidenceScore { get; set; }
    public string? TreatmentSuggestions { get; set; }
    public string? Severity { get; set; }
    public ReportStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Crop Crop { get; set; } = null!;
    public User ReportedBy { get; set; } = null!;
}

public enum ReportStatus
{
    Pending,
    Analyzed,
    Treated,
    Resolved,
    Failed
}