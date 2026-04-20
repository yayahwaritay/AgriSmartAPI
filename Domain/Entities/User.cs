namespace AgriSmartSierra.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    public FarmerProfile? FarmerProfile { get; set; }
    public BuyerProfile? BuyerProfile { get; set; }
    public AgronomistProfile? AgronomistProfile { get; set; }
}

public enum UserRole
{
    Farmer,
    Buyer,
    Agronomist,
    Admin
}