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
public class PestController : ControllerBase
{
    private readonly IPestReportService _pestReportService;

    public PestController(IPestReportService pestReportService)
    {
        _pestReportService = pestReportService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PestReportDto>>> GetById(Guid id)
    {
        var result = await _pestReportService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("crop/{cropId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PestReportDto>>>> GetByCropId(Guid cropId)
    {
        var result = await _pestReportService.GetByCropIdAsync(cropId);
        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PestReportDto>>>> GetPending()
    {
        var result = await _pestReportService.GetPendingAsync();
        return Ok(result);
    }

    [HttpPost("report")]
    public async Task<ActionResult<ApiResponse<PestReportDto>>> Create([FromForm] CreatePestReportDto dto)
    {
        var userId = GetUserId();
        var result = await _pestReportService.CreateAsync(dto, userId);
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<ApiResponse<PestReportDto>>> UpdateStatus(Guid id, [FromQuery] string status)
    {
        var result = await _pestReportService.UpdateStatusAsync(id, status);
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MLPredictionsController : ControllerBase
{
    private readonly IMLPredictionService _mlPredictionService;

    public MLPredictionsController(IMLPredictionService mlPredictionService)
    {
        _mlPredictionService = mlPredictionService;
    }

    [HttpPost("predict-disease")]
    public async Task<ActionResult<ApiResponse<object>>> PredictDisease(IFormFile file, Guid cropId)
    {
        var request = new DiseasePredictionRequestDto { Image = file, CropId = cropId };
        var result = await _mlPredictionService.PredictDiseaseAsync(request);
        return Ok(result);
    }

    [HttpGet("predict-yield/{cropId}")]
    public async Task<ActionResult<ApiResponse<YieldPredictionDto>>> PredictYield(Guid cropId)
    {
        var result = await _mlPredictionService.PredictYieldAsync(cropId);
        return Ok(result);
    }

    [HttpPost("weather-recommendations")]
    public async Task<ActionResult<ApiResponse<IEnumerable<WeatherRecommendationDto>>>> GetWeatherRecommendations([FromBody] WeatherRequestDto request)
    {
        var result = await _mlPredictionService.GetWeatherRecommendationsAsync(request);
        return Ok(result);
    }
}