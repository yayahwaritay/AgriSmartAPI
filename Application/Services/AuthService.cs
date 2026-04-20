using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AgriSmartSierra.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    public async Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Email already registered"
            };
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Role = Enum.Parse<UserRole>(request.Role, true),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        user.PasswordHash = HashPassword(request.Password);

        await _userRepository.AddAsync(user);
        
        var token = await GenerateJwtTokenAsync(user);
        
        return new ApiResponse<LoginResponseDto>
        {
            Success = true,
            Message = "Registration successful",
            Data = new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            }
        };
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user == null)
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        if (!VerifyPassword(request.Password, user.PasswordHash))
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        if (!user.IsActive)
        {
            return new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = "Account is disabled"
            };
        }

        var token = await GenerateJwtTokenAsync(user);
        
        return new ApiResponse<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful",
            Data = new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            }
        };
    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "YourSecretKeyHere12345678901234567890"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "AgriSmartSierra",
            audience: _configuration["Jwt:Audience"] ?? "AgriSmartSierra",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}