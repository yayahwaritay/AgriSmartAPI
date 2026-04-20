using AgriSmartSierra.Domain.Entities;

namespace AgriSmartSierra.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class FarmerProfileDto
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
}

public class CreateFarmerProfileDto
{
    public string FarmName { get; set; } = string.Empty;
    public decimal FarmSizeHectares { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string District { get; set; } = string.Empty;
    public string? SoilType { get; set; }
    public string? WaterSource { get; set; }
    public string? IrrigationType { get; set; }
}

public class BuyerProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateBuyerProfileDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
}

public class AgronomistProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string ServiceArea { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAgronomistProfileDto
{
    public string Qualification { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string ServiceArea { get; set; } = string.Empty;
}

public class UpdateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
}