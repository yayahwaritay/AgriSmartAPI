using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgriSmartSierra.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FarmsController : ControllerBase
{
    private readonly IFarmService _farmService;

    public FarmsController(IFarmService farmService)
    {
        _farmService = farmService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<FarmDto>>> GetById(Guid id)
    {
        var result = await _farmService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("farmer/{farmerProfileId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<FarmDto>>>> GetByFarmerProfileId(Guid farmerProfileId)
    {
        var result = await _farmService.GetByFarmerProfileIdAsync(farmerProfileId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<FarmDto>>> Create(Guid farmerProfileId, [FromBody] CreateFarmDto dto)
    {
        var result = await _farmService.CreateAsync(farmerProfileId, dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<FarmDto>>> Update(Guid id, [FromBody] CreateFarmDto dto)
    {
        var result = await _farmService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
    {
        var result = await _farmService.DeleteAsync(id);
        return Ok(result);
    }
}