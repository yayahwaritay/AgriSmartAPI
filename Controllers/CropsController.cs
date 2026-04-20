using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriSmartSierra.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CropsController : ControllerBase
{
    private readonly ICropService _cropService;
    private readonly IFarmService _farmService;
    private readonly ICropActivityService _activityService;

    public CropsController(
        ICropService cropService,
        IFarmService farmService,
        ICropActivityService activityService)
    {
        _cropService = cropService;
        _farmService = farmService;
        _activityService = activityService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CropDto>>> GetById(Guid id)
    {
        var result = await _cropService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("farm/{farmId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CropDto>>>> GetByFarmId(Guid farmId)
    {
        var result = await _cropService.GetByFarmIdAsync(farmId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CropDto>>> Create([FromBody] CreateCropDto dto)
    {
        var result = await _cropService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<CropDto>>> Update(Guid id, [FromBody] UpdateCropDto dto)
    {
        var result = await _cropService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
    {
        var result = await _cropService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/calendar")]
    public async Task<ActionResult<ApiResponse<CropCalendarDto>>> GetCropCalendar(Guid id)
    {
        var result = await _cropService.GetCropCalendarAsync(id);
        return Ok(result);
    }

    [HttpGet("activity/upcoming")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CropActivityDto>>>> GetUpcomingActivities([FromQuery] int days = 7)
    {
        var result = await _activityService.GetUpcomingAsync(days);
        return Ok(result);
    }

    [HttpPost("activity")]
    public async Task<ActionResult<ApiResponse<CropActivityDto>>> CreateActivity([FromBody] CreateCropActivityDto dto)
    {
        var result = await _activityService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("activity/{id}/complete")]
    public async Task<ActionResult<ApiResponse<CropActivityDto>>> CompleteActivity(Guid id)
    {
        var result = await _activityService.CompleteAsync(id);
        return Ok(result);
    }
}