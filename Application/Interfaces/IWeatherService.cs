using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface IWeatherService
{
    Task<ApiResponse<WeatherDto>> GetCurrentWeatherAsync(WeatherRequestDto request);
    Task<ApiResponse<IEnumerable<WeatherForecastDto>>> GetForecastAsync(WeatherRequestDto request, int days);
    Task<ApiResponse<IEnumerable<WeatherAlertDto>>> GetAlertsAsync(WeatherRequestDto request);
    Task<ApiResponse<bool>> FetchAndStoreWeatherAsync(string location);
}

public interface IResourceCalculatorService
{
    Task<ApiResponse<FertilizerRecommendationDto>> CalculateFertilizerAsync(ResourceCalculatorDto dto);
    Task<ApiResponse<WaterRecommendationDto>> CalculateWaterAsync(ResourceCalculatorDto dto);
    Task<ApiResponse<decimal>> CalculateFarmSizeAsync(decimal length, decimal width, string unit);
}