using AgriSmartAPI.DTO;
using AgriSmartAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AgriSmartAPI.Controllers;

[ApiController]
[Route("api/v1/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new UnauthorizedAccessException("User ID not found in token");
        return userId;
    }

    private string GetUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? "";
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(new
            {
                message = "Users retrieved successfully",
                users = users,
                status = 1
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to retrieve users",
                detail = ex.Message
            });
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        try
        {
            var userId = GetUserId();
            var profile = await _userService.GetUserProfileAsync(userId);
            return Ok(new
            {
                message = "Profile retrieved successfully",
                profile = profile,
                status = 1
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user profile");
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to retrieve profile",
                detail = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                message = "User retrieved successfully",
                user = user,
                status = 1
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user {UserId}", id);
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to retrieve user",
                detail = ex.Message
            });
        }
    }

    [HttpGet("username/{username}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                message = "User retrieved successfully",
                user = user,
                status = 1
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user by username {Username}", username);
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to retrieve user",
                detail = ex.Message
            });
        }
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateUserRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updatedUser = await _userService.UpdateUserAsync(userId, request);

            return Ok(new
            {
                message = "Profile updated successfully",
                user = updatedUser,
                status = 1
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user profile");
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to update profile",
                detail = ex.Message
            });
        }
    }

    [HttpPut("me/password")]
    public async Task<IActionResult> ChangeMyPassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var result = await _userService.ChangePasswordAsync(userId, request);

            return Ok(new
            {
                message = "Password changed successfully",
                status = 1
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password");
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to change password",
                detail = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUserAsync(id, request);

            return Ok(new
            {
                message = "User updated successfully",
                user = updatedUser,
                status = 1
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", id);
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to update user",
                detail = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var currentUserId = GetUserId();
            if (currentUserId == id)
                return BadRequest(new { message = "You cannot delete your own account" });

            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound(new { message = "User not found" });

            return Ok(new
            {
                message = "User deleted successfully",
                status = 1
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            return StatusCode(500, new
            {
                statusCode = 500,
                message = "Failed to delete user",
                detail = ex.Message
            });
        }
    }
}