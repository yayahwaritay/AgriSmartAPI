using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto request);
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<string> GenerateJwtTokenAsync(Domain.Entities.User user);
}

public interface IUserService
{
    Task<ApiResponse<UserDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<UserDto>> UpdateAsync(Guid id, UpdateUserDto dto);
}

public interface IFarmerProfileService
{
    Task<ApiResponse<FarmerProfileDto>> GetByUserIdAsync(Guid userId);
    Task<ApiResponse<FarmerProfileDto>> CreateAsync(Guid userId, CreateFarmerProfileDto dto);
    Task<ApiResponse<FarmerProfileDto>> UpdateAsync(Guid id, CreateFarmerProfileDto dto);
    Task<ApiResponse<IEnumerable<FarmerProfileDto>>> GetByDistrictAsync(string district);
}

public interface IBuyerProfileService
{
    Task<ApiResponse<BuyerProfileDto>> GetByUserIdAsync(Guid userId);
    Task<ApiResponse<BuyerProfileDto>> CreateAsync(Guid userId, CreateBuyerProfileDto dto);
    Task<ApiResponse<BuyerProfileDto>> UpdateAsync(Guid id, CreateBuyerProfileDto dto);
}

public interface IAgronomistProfileService
{
    Task<ApiResponse<AgronomistProfileDto>> GetByUserIdAsync(Guid userId);
    Task<ApiResponse<AgronomistProfileDto>> CreateAsync(Guid userId, CreateAgronomistProfileDto dto);
    Task<ApiResponse<IEnumerable<AgronomistProfileDto>>> GetVerifiedAsync();
    Task<ApiResponse<IEnumerable<AgronomistProfileDto>>> GetByServiceAreaAsync(string area);
}