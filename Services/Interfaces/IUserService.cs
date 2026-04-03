using AgriSmartAPI.DTO;
using AgriSmartAPI.Models;

namespace AgriSmartAPI.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUsernameAsync(string username);
    Task<UserProfileResponse> GetUserProfileAsync(int userId);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserRequest request);
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> UserExistsAsync(int id);
}