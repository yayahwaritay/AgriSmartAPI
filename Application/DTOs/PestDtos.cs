namespace AgriSmartSierra.Application.DTOs;

public class PestReportDto
{
    public Guid Id { get; set; }
    public Guid CropId { get; set; }
    public Guid ReportedById { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? DetectedDisease { get; set; }
    public double? ConfidenceScore { get; set; }
    public string? TreatmentSuggestions { get; set; }
    public string? Severity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreatePestReportDto
{
    public Guid CropId { get; set; }
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}

public class DiseasePredictionDto
{
    public string DiseaseName { get; set; } = string.Empty;
    public double ConfidenceScore { get; set; }
    public string TreatmentSuggestions { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public List<string>? RecommendedActions { get; set; }
}

public class DiseasePredictionRequestDto
{
    public IFormFile Image { get; set; } = null!;
    public Guid CropId { get; set; }
}