using System.ComponentModel.DataAnnotations;

namespace AgriSmartAPI.Models;

public class SoilPrediction
{
    public int Id { get; set; }
    [StringLength(100)]
    public string SoilType { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    [StringLength(500)]
    public string RecommendedCrops { get; set; }
    public DateTime PredictionDate { get; set; }
}
