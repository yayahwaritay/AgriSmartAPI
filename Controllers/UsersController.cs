using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AgriSmartSierra.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IFarmerProfileService _farmerProfileService;
    private readonly IBuyerProfileService _buyerProfileService;
    private readonly IAgronomistProfileService _agronomistProfileService;

    public UsersController(
        IUserService userService,
        IFarmerProfileService farmerProfileService,
        IBuyerProfileService buyerProfileService,
        IAgronomistProfileService agronomistProfileService)
    {
        _userService = userService;
        _farmerProfileService = farmerProfileService;
        _buyerProfileService = buyerProfileService;
        _agronomistProfileService = agronomistProfileService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetById(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpGet("profile/farmer")]
    public async Task<ActionResult<ApiResponse<FarmerProfileDto>>> GetFarmerProfile()
    {
        var userId = GetUserId();
        var result = await _farmerProfileService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost("profile/farmer")]
    public async Task<ActionResult<ApiResponse<FarmerProfileDto>>> CreateFarmerProfile([FromBody] CreateFarmerProfileDto dto)
    {
        var userId = GetUserId();
        var result = await _farmerProfileService.CreateAsync(userId, dto);
        return Ok(result);
    }

    [HttpPut("profile/farmer/{id}")]
    public async Task<ActionResult<ApiResponse<FarmerProfileDto>>> UpdateFarmerProfile(Guid id, [FromBody] CreateFarmerProfileDto dto)
    {
        var result = await _farmerProfileService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpGet("profile/buyer")]
    public async Task<ActionResult<ApiResponse<BuyerProfileDto>>> GetBuyerProfile()
    {
        var userId = GetUserId();
        var result = await _buyerProfileService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost("profile/buyer")]
    public async Task<ActionResult<ApiResponse<BuyerProfileDto>>> CreateBuyerProfile([FromBody] CreateBuyerProfileDto dto)
    {
        var userId = GetUserId();
        var result = await _buyerProfileService.CreateAsync(userId, dto);
        return Ok(result);
    }

    [HttpGet("profile/agronomist")]
    public async Task<ActionResult<ApiResponse<AgronomistProfileDto>>> GetAgronomistProfile()
    {
        var userId = GetUserId();
        var result = await _agronomistProfileService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost("profile/agronomist")]
    public async Task<ActionResult<ApiResponse<AgronomistProfileDto>>> CreateAgronomistProfile([FromBody] CreateAgronomistProfileDto dto)
    {
        var userId = GetUserId();
        var result = await _agronomistProfileService.CreateAsync(userId, dto);
        return Ok(result);
    }

    [HttpGet("agronomists/verified")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<AgronomistProfileDto>>>> GetVerifiedAgronomists()
    {
        var result = await _agronomistProfileService.GetVerifiedAsync();
        return Ok(result);
    }

    [HttpGet("agronomists")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<AgronomistProfileDto>>>> GetAgronomistsByArea([FromQuery] string area)
    {
        var result = await _agronomistProfileService.GetByServiceAreaAsync(area);
        return Ok(result);
    }
}