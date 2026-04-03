using AgriSmartAPI.Data;
using AgriSmartAPI.DTO;
using AgriSmartAPI.Models;
using AgriSmartAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AgriSmartAPI.Services;

public class UserService : IUserService
{
    private readonly AgriSmartContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(AgriSmartContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserProfileResponse> GetUserProfileAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new ValidationException("User not found");

        var cropCount = await _context.Crops.CountAsync(c => c.UserId == userId);
        var diagnosisCount = await _context.PestDiagnoses.CountAsync(p => p.FarmerId == userId);

        return new UserProfileResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            CropCount = cropCount,
            DiagnosisCount = diagnosisCount
        };
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new ValidationException("User not found");

        if (!string.IsNullOrEmpty(request.FullName))
            user.FullName = request.FullName;

        if (!string.IsNullOrEmpty(request.Username))
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Id != id);
            if (existingUser != null)
                throw new ValidationException("Username already exists");
            user.Username = request.Username;
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            var existingEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Id != id);
            if (existingEmail != null)
                throw new ValidationException("Email already exists");
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Role))
            user.Role = request.Role;

        await _context.SaveChangesAsync();

        _logger.LogInformation("User {Username} updated successfully", user.Username);

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new ValidationException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
            throw new ValidationException("Current password is incorrect");

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, workFactor: 12);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Password changed for user {Username}", user.Username);
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User {Username} deleted successfully", user.Username);
        return true;
    }

    public async Task<bool> UserExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }
}