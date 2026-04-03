using System.ComponentModel.DataAnnotations;

namespace AgriSmartAPI.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateUserRequest
{
    [StringLength(100)]
    public string? FullName { get; set; }
    
    [StringLength(50)]
    public string? Username { get; set; }
    
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
    
    [StringLength(50)]
    public string? Role { get; set; }
}

public class ChangePasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}

public class UserProfileResponse
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CropCount { get; set; }
    public int DiagnosisCount { get; set; }
}