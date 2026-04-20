namespace AgriSmartSierra.Domain.Entities;

public class WeatherLog
{
    public Guid Id { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Rainfall { get; set; }
    public double WindSpeed { get; set; }
    public string? WeatherCondition { get; set; }
    public string? WeatherDescription { get; set; }
    public string? AlertType { get; set; }
    public string? AlertMessage { get; set; }
    public DateTime LoggedAt { get; set; }
    public DateTime RecordedAt { get; set; }
}

public enum WeatherAlertType
{
    None,
    Flood,
    Drought,
    Storm,
    HeatWave,
    ColdWave
}