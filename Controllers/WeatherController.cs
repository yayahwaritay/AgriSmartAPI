using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriSmartSierra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<WeatherDto>>> GetCurrentWeather([FromQuery] WeatherRequestDto request)
    {
        var result = await _weatherService.GetCurrentWeatherAsync(request);
        return Ok(result);
    }

    [HttpGet("forecast")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<WeatherForecastDto>>>> GetForecast([FromQuery] WeatherRequestDto request, [FromQuery] int days = 5)
    {
        var result = await _weatherService.GetForecastAsync(request, days);
        return Ok(result);
    }

    [HttpGet("alerts")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<WeatherAlertDto>>>> GetAlerts([FromQuery] WeatherRequestDto request)
    {
        var result = await _weatherService.GetAlertsAsync(request);
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResourcesController : ControllerBase
{
    private readonly IResourceCalculatorService _resourceService;

    public ResourcesController(IResourceCalculatorService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpPost("fertilizer")]
    public async Task<ActionResult<ApiResponse<FertilizerRecommendationDto>>> CalculateFertilizer([FromBody] ResourceCalculatorDto dto)
    {
        var result = await _resourceService.CalculateFertilizerAsync(dto);
        return Ok(result);
    }

    [HttpPost("water")]
    public async Task<ActionResult<ApiResponse<WaterRecommendationDto>>> CalculateWater([FromBody] ResourceCalculatorDto dto)
    {
        var result = await _resourceService.CalculateWaterAsync(dto);
        return Ok(result);
    }

    [HttpGet("size")]
    public async Task<ActionResult<ApiResponse<decimal>>> CalculateFarmSize([FromQuery] decimal length, [FromQuery] decimal width, [FromQuery] string unit = "meters")
    {
        var result = await _resourceService.CalculateFarmSizeAsync(length, width, unit);
        return Ok(result);
    }
}