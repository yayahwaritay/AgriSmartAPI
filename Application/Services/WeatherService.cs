namespace AgriSmartSierra.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AgriSmartSierra.Application.DTOs;
    using AgriSmartSierra.Application.DTOs.Common;
    using AgriSmartSierra.Application.Interfaces;
    using AgriSmartSierra.Domain.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class WeatherService : IWeatherService
    {
        private readonly IWeatherLogRepository _weatherLogRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeatherService(IWeatherLogRepository weatherLogRepository, IConfiguration configuration)
        {
            _weatherLogRepository = weatherLogRepository;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<ApiResponse<WeatherDto>> GetCurrentWeatherAsync(WeatherRequestDto request)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var baseUrl = _configuration["WeatherApi:BaseUrl"] ?? "https://api.openweathermap.org/data/2.5";
            
            if (string.IsNullOrEmpty(apiKey))
            {
                return new ApiResponse<WeatherDto> { Success = false, Message = "Weather API key not configured" };
            }

            try
            {
                var lat = request.Latitude != 0 ? request.Latitude : 8.4657;
                var lon = request.Longitude != 0 ? request.Longitude : -11.8673;
                var url = string.Format("{0}/weather?lat={1}&lon={2}&appid={3}&units=metric", baseUrl, lat, lon, apiKey);
                
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<WeatherDto> { Success = false, Message = "Failed to fetch weather data" };
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var main = root.GetProperty("main");
                var temp = main.GetProperty("temp").GetDouble();
                var humidity = main.GetProperty("humidity").GetDouble();
                
                double rainfall = 0;
                if (root.TryGetProperty("rain", out var rain) && rain.TryGetProperty("1h", out var rain1h))
                {
                    rainfall = rain1h.GetDouble();
                }

                double windSpeed = 0;
                if (root.TryGetProperty("wind", out var wind) && wind.TryGetProperty("speed", out var windSpeedProp))
                {
                    windSpeed = windSpeedProp.GetDouble();
                }

                string? weatherCondition = null;
                if (root.TryGetProperty("weather", out var weather) && weather.GetArrayLength() > 0)
                {
                    weatherCondition = weather[0].GetProperty("main").GetString();
                }

                var alertType = DetermineAlertType(temp, humidity, windSpeed, rainfall);
                
                return new ApiResponse<WeatherDto>
                {
                    Success = true,
                    Data = new WeatherDto
                    {
                        Id = Guid.NewGuid(),
                        Location = request.Location,
                        Temperature = temp,
                        Humidity = humidity,
                        Rainfall = rainfall,
                        WindSpeed = windSpeed,
                        WeatherCondition = weatherCondition,
                        AlertType = alertType,
                        AlertMessage = GetAlertMessage(alertType),
                        RecordedAt = DateTime.UtcNow
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<WeatherDto> { Success = false, Message = "Error fetching weather: " + ex.Message };
            }
        }

        public async Task<ApiResponse<IEnumerable<WeatherForecastDto>>> GetForecastAsync(WeatherRequestDto request, int days)
        {
            return new ApiResponse<IEnumerable<WeatherForecastDto>>
            {
                Success = false,
                Message = "Forecast feature requires external API integration"
            };
        }

        public async Task<ApiResponse<IEnumerable<WeatherAlertDto>>> GetAlertsAsync(WeatherRequestDto request)
        {
            var weather = await GetCurrentWeatherAsync(request);
            if (!weather.Success || weather.Data == null)
            {
                return new ApiResponse<IEnumerable<WeatherAlertDto>> { Success = false, Message = weather.Message };
            }

            var alerts = new List<WeatherAlertDto>();
            
            if (!string.IsNullOrEmpty(weather.Data.AlertType) && weather.Data.AlertType != "None")
            {
                alerts.Add(new WeatherAlertDto
                {
                    AlertType = weather.Data.AlertType,
                    Message = weather.Data.AlertMessage ?? "",
                    Severity = "Medium",
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(24),
                    Recommendations = GetRecommendations(weather.Data.AlertType)
                });
            }

            return new ApiResponse<IEnumerable<WeatherAlertDto>> { Success = true, Data = alerts };
        }

        public async Task<ApiResponse<bool>> FetchAndStoreWeatherAsync(string location)
        {
            return new ApiResponse<bool> { Success = true, Data = true };
        }

        private static string DetermineAlertType(double temp, double humidity, double windSpeed, double rainfall)
        {
            if (rainfall > 50) return "Flood";
            if (humidity < 20) return "Drought";
            if (windSpeed > 25) return "Storm";
            if (temp > 40) return "HeatWave";
            if (temp < 5) return "ColdWave";
            return "None";
        }

        private static string? GetAlertMessage(string alertType)
        {
            return alertType switch
            {
                "Flood" => "Heavy rainfall expected. Take precautions.",
                "Drought" => "Low humidity. Ensure adequate irrigation.",
                "Storm" => "Strong winds expected. Secure crops.",
                "HeatWave" => "High temperatures. Provide shade and water.",
                "ColdWave" => "Cold temperatures. Protect sensitive crops.",
                _ => null
            };
        }

        private static List<string>? GetRecommendations(string alertType)
        {
            return alertType switch
            {
                "Flood" => new List<string> { "Move livestock to higher ground", "Check drainage", "Harvest early if ready" },
                "Drought" => new List<string> { "Use drip irrigation", "Apply mulch", "Water early morning" },
                "Storm" => new List<string> { "Secure structures", "Harvest if ready", "Move equipment indoors" },
                _ => new List<string> { "Monitor crops regularly" }
            };
        }
    }

    public class ResourceCalculatorService : IResourceCalculatorService
    {
        public async Task<ApiResponse<FertilizerRecommendationDto>> CalculateFertilizerAsync(ResourceCalculatorDto dto)
        {
            var recommendation = GetFertilizerRecommendation(dto);
            return new ApiResponse<FertilizerRecommendationDto> { Success = true, Data = recommendation };
        }

        public async Task<ApiResponse<WaterRecommendationDto>> CalculateWaterAsync(ResourceCalculatorDto dto)
        {
            var dailyNeed = dto.FarmSizeHectares * 10000 * GetCropWaterFactor(dto.CropType);
            
            return new ApiResponse<WaterRecommendationDto>
            {
                Success = true,
                Data = new WaterRecommendationDto
                {
                    DailyWaterNeedLiters = dailyNeed,
                    WeeklyWaterNeedLiters = dailyNeed * 7,
                    IrrigationMethod = dto.IrrigationType ?? "Drip",
                    BestTimesToWater = new List<string> { "Early Morning (6-8 AM)", "Late Evening (5-7 PM)" },
                    Notes = "Adjust based on weather conditions"
                }
            };
        }

        public async Task<ApiResponse<decimal>> CalculateFarmSizeAsync(decimal length, decimal width, string unit)
        {
            var conversionFactor = unit.ToLower() == "meters" ? 1m : 10000m;
            var squareMeters = length * width / conversionFactor;
            return new ApiResponse<decimal> { Success = true, Data = squareMeters };
        }

        private static decimal GetCropWaterFactor(string cropType)
        {
            return cropType.ToLower() switch
            {
                "rice" => 5m,
                "maize" => 4m,
                "cassava" => 3m,
                "vegetables" => 6m,
                "fruits" => 4m,
                _ => 4m
            };
        }

        private static FertilizerRecommendationDto GetFertilizerRecommendation(ResourceCalculatorDto dto)
        {
            var (type, amount, timing) = GetFertilizerType(dto.CropType);
            
            return new FertilizerRecommendationDto
            {
                FertilizerType = type,
                AmountPerHectare = amount,
                Unit = "kg/ha",
                TotalAmount = amount * dto.FarmSizeHectares,
                ApplicationTiming = timing,
                Notes = "Apply based on soil test results"
            };
        }

        private static (string type, decimal amount, string timing) GetFertilizerType(string cropType)
        {
            return cropType.ToLower() switch
            {
                "rice" => ("NPK 20-10-10", 300m, "At planting and 6 weeks after"),
                "maize" => ("NPK 20-10-10", 250m, "At planting and 6 weeks after"),
                "cassava" => ("NPK 15-15-15", 200m, "At planting"),
                "vegetables" => ("NPK 10-10-20", 400m, "Split application every 2 weeks"),
                "fruits" => ("NPK 10-10-10", 150m, "Monthly during growing season"),
                _ => ("NPK 15-15-15", 200m, "At planting")
            };
        }
    }
}