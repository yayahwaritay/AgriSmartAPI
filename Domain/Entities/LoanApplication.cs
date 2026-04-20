namespace AgriSmartSierra.Domain.Entities;

public class LoanApplication
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string LenderName { get; set; } = string.Empty;
    public decimal AmountRequested { get; set; }
    public decimal? AmountApproved { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int RepaymentPeriodMonths { get; set; }
    public LoanStatus Status { get; set; }
    public string? Remarks { get; set; }
    public DateTime AppliedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? DisbursedAt { get; set; }

    public User Farmer { get; set; } = null!;
}

public enum LoanStatus
{
    Pending,
    UnderReview,
    Approved,
    Rejected,
    Disbursed,
    Repaid,
    Defaulted
}