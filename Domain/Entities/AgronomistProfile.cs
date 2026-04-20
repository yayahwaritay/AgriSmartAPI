namespace AgriSmartSierra.Domain.Entities;

public class AgronomistProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Qualification { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string? CertificationUrl { get; set; }
    public string ServiceArea { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}