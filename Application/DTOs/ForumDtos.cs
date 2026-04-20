namespace AgriSmartSierra.Application.DTOs;

public class ForumPostDto
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorRole { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
    public int ViewCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsPinned { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateForumPostDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
}

public class UpdateForumPostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Category { get; set; }
    public bool? IsPinned { get; set; }
    public bool? IsLocked { get; set; }
}

public class ForumCommentDto
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorRole { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int UpvoteCount { get; set; }
    public bool IsAcceptedAnswer { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateForumCommentDto
{
    public Guid PostId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
}

public class LoanApplicationDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string LenderName { get; set; } = string.Empty;
    public decimal AmountRequested { get; set; }
    public decimal? AmountApproved { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int RepaymentPeriodMonths { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTime AppliedAt { get; set; }
}

public class CreateLoanApplicationDto
{
    public string LenderName { get; set; } = string.Empty;
    public decimal AmountRequested { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int RepaymentPeriodMonths { get; set; }
}

public class UpdateLoanApplicationDto
{
    public string Status { get; set; } = string.Empty;
    public decimal? AmountApproved { get; set; }
    public string? Remarks { get; set; }
}

public class InsuranceInfoDto
{
    public Guid Id { get; set; }
    public Guid FarmerId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string CoverageType { get; set; } = string.Empty;
    public decimal CoverageAmount { get; set; }
    public decimal PremiumAmount { get; set; }
    public string? CoverageDetails { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}

public class CreateInsuranceInfoDto
{
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string CoverageType { get; set; } = string.Empty;
    public decimal CoverageAmount { get; set; }
    public decimal PremiumAmount { get; set; }
    public string? CoverageDetails { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}