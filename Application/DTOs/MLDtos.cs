namespace AgriSmartSierra.Application.DTOs;

public class YieldPredictionDto
{
    public Guid CropId { get; set; }
    public string CropName { get; set; } = string.Empty;
    public decimal PredictedYield { get; set; }
    public string Unit { get; set; } = string.Empty;
    public double ConfidenceScore { get; set; }
    public DateTime PredictedAt { get; set; }
    public List<YieldFactorDto>? Factors { get; set; }
}

public class YieldFactorDto
{
    public string FactorName { get; set; } = string.Empty;
    public string Impact { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class WeatherRecommendationDto
{
    public string RecommendationType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public List<string>? Actions { get; set; }
}

public class AnalyticsDashboardDto
{
    public int TotalFarms { get; set; }
    public int TotalCrops { get; set; }
    public int ActiveListings { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal EstimatedYield { get; set; }
    public List<MonthlyTrendDto> YieldTrends { get; set; } = new();
    public List<MonthlyTrendDto> RevenueTrends { get; set; } = new();
}

public class MonthlyTrendDto
{
    public string Month { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class InputUsageDto
{
    public string InputType { get; set; } = string.Empty;
    public decimal QuantityUsed { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public DateTime DateUsed { get; set; }
}

public class ProfitEstimationDto
{
    public decimal EstimatedRevenue { get; set; }
    public decimal EstimatedCosts { get; set; }
    public decimal EstimatedProfit { get; set; }
    public decimal ProfitMargin { get; set; }
    public List<CostBreakdownDto> CostBreakdown { get; set; } = new();
}

public class CostBreakdownDto
{
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}