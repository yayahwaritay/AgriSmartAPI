using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AgriSmartSierra.Application.Services;

public class PestReportService : IPestReportService
{
    private readonly IPestReportRepository _pestReportRepository;
    private readonly ICropRepository _cropRepository;

    public PestReportService(
        IPestReportRepository pestReportRepository,
        ICropRepository cropRepository)
    {
        _pestReportRepository = pestReportRepository;
        _cropRepository = cropRepository;
    }

    public async Task<ApiResponse<PestReportDto>> GetByIdAsync(Guid id)
    {
        var report = await _pestReportRepository.GetByIdAsync(id);
        if (report == null)
            return new ApiResponse<PestReportDto> { Success = false, Message = "Report not found" };

        return new ApiResponse<PestReportDto> { Success = true, Data = MapToDto(report) };
    }

    public async Task<ApiResponse<IEnumerable<PestReportDto>>> GetByCropIdAsync(Guid cropId)
    {
        var reports = await _pestReportRepository.GetByCropIdAsync(cropId);
        return new ApiResponse<IEnumerable<PestReportDto>> { Success = true, Data = reports.Select(MapToDto) };
    }

    public async Task<ApiResponse<IEnumerable<PestReportDto>>> GetPendingAsync()
    {
        var reports = await _pestReportRepository.GetPendingAsync();
        return new ApiResponse<IEnumerable<PestReportDto>> { Success = true, Data = reports.Select(MapToDto) };
    }

    public async Task<ApiResponse<PestReportDto>> CreateAsync(CreatePestReportDto dto, Guid userId)
    {
        var crop = await _cropRepository.GetByIdAsync(dto.CropId);
        if (crop == null)
            return new ApiResponse<PestReportDto> { Success = false, Message = "Crop not found" };

        var report = new PestReport
        {
            Id = Guid.NewGuid(),
            CropId = dto.CropId,
            ReportedById = userId,
            Description = dto.Description,
            Status = ReportStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _pestReportRepository.AddAsync(report);
        return new ApiResponse<PestReportDto> { Success = true, Message = "Report created", Data = MapToDto(report) };
    }

    public async Task<ApiResponse<PestReportDto>> UpdateStatusAsync(Guid id, string status)
    {
        var report = await _pestReportRepository.GetByIdAsync(id);
        if (report == null)
            return new ApiResponse<PestReportDto> { Success = false, Message = "Report not found" };

        report.Status = Enum.Parse<ReportStatus>(status, true);
        report.UpdatedAt = DateTime.UtcNow;

        await _pestReportRepository.UpdateAsync(report);
        return new ApiResponse<PestReportDto> { Success = true, Data = MapToDto(report) };
    }

    private static PestReportDto MapToDto(PestReport r) => new PestReportDto
    {
        Id = r.Id,
        CropId = r.CropId,
        ReportedById = r.ReportedById,
        ImageUrl = r.ImageUrl,
        Description = r.Description,
        DetectedDisease = r.DetectedDisease,
        ConfidenceScore = r.ConfidenceScore,
        TreatmentSuggestions = r.TreatmentSuggestions,
        Severity = r.Severity,
        Status = r.Status.ToString(),
        CreatedAt = r.CreatedAt
    };
}

public class MLPredictionService : IMLPredictionService
{
    private readonly IPestReportRepository _pestReportRepository;
    private readonly ICropRepository _cropRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public MLPredictionService(
        IPestReportRepository pestReportRepository,
        ICropRepository cropRepository,
        IConfiguration configuration)
    {
        _pestReportRepository = pestReportRepository;
        _cropRepository = cropRepository;
        _configuration = configuration;
        _httpClient = new HttpClient();
    }

    public async Task<ApiResponse<object>> PredictDiseaseAsync(DiseasePredictionRequestDto request)
    {
        var mlEndpoint = _configuration["MLService:Endpoint"];
        var mlApiKey = _configuration["MLService:ApiKey"];

        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(request.Image.OpenReadStream());
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);
        content.Add(streamContent, "image", request.Image.FileName);

        _httpClient.DefaultRequestHeaders.Clear();
        if (!string.IsNullOrEmpty(mlApiKey))
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {mlApiKey}");

        var response = await _httpClient.PostAsync($"{mlEndpoint}/detect", content);
        var json = await response.Content.ReadAsStringAsync();
        var rawResult = System.Text.Json.JsonSerializer.Deserialize<object>(json);

        return new ApiResponse<object>
        {
            Success = true,
            Data = rawResult
        };
    }

    private static string? ExtractDiseaseFromResponse(string json)
    {
        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty("result", out var result) && result.TryGetProperty("detections", out var detections) && detections.GetArrayLength() > 0)
            {
                return detections[0].GetProperty("disease").GetString();
            }
            return null;
        }
        catch { return null; }
    }

    private static double? ExtractConfidenceFromResponse(string json)
    {
        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty("result", out var result) && result.TryGetProperty("detections", out var detections) && detections.GetArrayLength() > 0)
            {
                return detections[0].GetProperty("confidence").GetDouble();
            }
            return null;
        }
        catch { return null; }
    }

    private static string? ExtractAdviceFromResponse(string json)
    {
        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty("result", out var result) && result.TryGetProperty("detections", out var detections) && detections.GetArrayLength() > 0)
            {
                return detections[0].GetProperty("advice").GetString();
            }
            return null;
        }
        catch { return null; }
    }

    

    public async Task<ApiResponse<YieldPredictionDto>> PredictYieldAsync(Guid cropId)
    {
        var crop = await _cropRepository.GetByIdAsync(cropId);
        if (crop == null)
            return new ApiResponse<YieldPredictionDto> { Success = false, Message = "Crop not found" };

        var predictedYield = crop.EstimatedYield * 0.9m;
        var random = new Random();
        var confidence = 0.7 + random.NextDouble() * 0.25;

        return new ApiResponse<YieldPredictionDto>
        {
            Success = true,
            Data = new YieldPredictionDto
            {
                CropId = cropId,
                CropName = crop.Name,
                PredictedYield = predictedYield,
                Unit = crop.Unit,
                ConfidenceScore = confidence,
                PredictedAt = DateTime.UtcNow,
                Factors = new List<YieldFactorDto>
                {
                    new YieldFactorDto { FactorName = "Weather", Impact = "Positive", Description = "Good rainfall patterns" },
                    new YieldFactorDto { FactorName = "Soil", Impact = "Neutral", Description = "Adequate nutrients" }
                }
            }
        };
    }

    public async Task<ApiResponse<IEnumerable<WeatherRecommendationDto>>> GetWeatherRecommendationsAsync(WeatherRequestDto request)
    {
        var crops = new List<WeatherRecommendationDto>
        {
            new WeatherRecommendationDto
            {
                RecommendationType = "Irrigation",
                Message = "Consider irrigation if no rainfall in 48 hours",
                Priority = "Medium",
                Actions = new List<string> { "Check soil moisture", "Water early morning" }
            },
            new WeatherRecommendationDto
            {
                RecommendationType = "Fertilizer",
                Message = "Apply fertilizer before expected rain",
                Priority = "Low",
                Actions = new List<string> { "Time application with forecast" }
            }
        };

        return new ApiResponse<IEnumerable<WeatherRecommendationDto>>
        {
            Success = true,
            Data = crops
        };
    }
}