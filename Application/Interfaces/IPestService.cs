using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface IPestReportService
{
    Task<ApiResponse<PestReportDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<PestReportDto>>> GetByCropIdAsync(Guid cropId);
    Task<ApiResponse<IEnumerable<PestReportDto>>> GetPendingAsync();
    Task<ApiResponse<PestReportDto>> CreateAsync(CreatePestReportDto dto, Guid userId);
    Task<ApiResponse<PestReportDto>> UpdateStatusAsync(Guid id, string status);
}

public interface IMLPredictionService
{
    Task<ApiResponse<object>> PredictDiseaseAsync(DiseasePredictionRequestDto request);
    Task<ApiResponse<YieldPredictionDto>> PredictYieldAsync(Guid cropId);
    Task<ApiResponse<IEnumerable<WeatherRecommendationDto>>> GetWeatherRecommendationsAsync(WeatherRequestDto request);
}