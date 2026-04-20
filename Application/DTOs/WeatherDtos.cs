namespace AgriSmartSierra.Application.DTOs;

public class WeatherDto
{
    public Guid Id { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Rainfall { get; set; }
    public double WindSpeed { get; set; }
    public string? WeatherCondition { get; set; }
    public string? WeatherDescription { get; set; }
    public string? AlertType { get; set; }
    public string? AlertMessage { get; set; }
    public DateTime RecordedAt { get; set; }
}

public class WeatherForecastDto
{
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Date { get; set; }
    public double Temperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double Humidity { get; set; }
    public double Rainfall { get; set; }
    public double RainfallProbability { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class WeatherAlertDto
{
    public string AlertType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<string>? Recommendations { get; set; }
}

public class WeatherRequestDto
{
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class ResourceCalculatorDto
{
    public decimal FarmSizeHectares { get; set; }
    public string CropType { get; set; } = string.Empty;
    public string? SoilType { get; set; }
    public string? IrrigationType { get; set; }
}

public class FertilizerRecommendationDto
{
    public string FertilizerType { get; set; } = string.Empty;
    public decimal AmountPerHectare { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string ApplicationTiming { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class WaterRecommendationDto
{
    public decimal DailyWaterNeedLiters { get; set; }
    public decimal WeeklyWaterNeedLiters { get; set; }
    public string IrrigationMethod { get; set; } = string.Empty;
    public List<string>? BestTimesToWater { get; set; }
    public string? Notes { get; set; }
}