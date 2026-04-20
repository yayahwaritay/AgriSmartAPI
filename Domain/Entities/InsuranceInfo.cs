namespace AgriSmartSierra.Domain.Entities;

public class InsuranceInfo
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string CoverageType { get; set; } = string.Empty;
    public decimal CoverageAmount { get; set; }
    public decimal PremiumAmount { get; set; }
    public string? CoverageDetails { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User Farmer { get; set; } = null!;
}