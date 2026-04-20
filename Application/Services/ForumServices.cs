using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;
using System.Linq.Expressions;

namespace AgriSmartSierra.Application.Services;

public class ForumService : IForumService
{
    private readonly IForumPostRepository _postRepository;
    private readonly IForumCommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public ForumService(
        IForumPostRepository postRepository,
        IForumCommentRepository commentRepository,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<ForumPostDto>> GetPostByIdAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return new ApiResponse<ForumPostDto> { Success = false, Message = "Post not found" };

        var author = await _userRepository.GetByIdAsync(post.AuthorId);
        return new ApiResponse<ForumPostDto> { Success = true, Data = MapToDto(post, author) };
    }

    public async Task<ApiResponse<IEnumerable<ForumPostDto>>> GetRecentPostsAsync(int count = 20)
    {
        var posts = await _postRepository.GetRecentAsync(count);
        var dtos = new List<ForumPostDto>();
        
        foreach (var post in posts)
        {
            var author = await _userRepository.GetByIdAsync(post.AuthorId);
            dtos.Add(MapToDto(post, author));
        }

        return new ApiResponse<IEnumerable<ForumPostDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<IEnumerable<ForumPostDto>>> GetPostsByCategoryAsync(string category)
    {
        var posts = await _postRepository.GetByCategoryAsync(category);
        var dtos = new List<ForumPostDto>();
        
        foreach (var post in posts)
        {
            var author = await _userRepository.GetByIdAsync(post.AuthorId);
            dtos.Add(MapToDto(post, author));
        }

        return new ApiResponse<IEnumerable<ForumPostDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<ForumPostDto>> CreatePostAsync(CreateForumPostDto dto, Guid authorId)
    {
        var post = new ForumPost
        {
            Id = Guid.NewGuid(),
            AuthorId = authorId,
            Title = dto.Title,
            Content = dto.Content,
            Category = dto.Category,
            ViewCount = 0,
            CreatedAt = DateTime.UtcNow
        };

        await _postRepository.AddAsync(post);
        
        var author = await _userRepository.GetByIdAsync(authorId);
        return new ApiResponse<ForumPostDto> { Success = true, Message = "Post created", Data = MapToDto(post, author) };
    }

    public async Task<ApiResponse<ForumPostDto>> UpdatePostAsync(Guid id, UpdateForumPostDto dto)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return new ApiResponse<ForumPostDto> { Success = false, Message = "Post not found" };

        if (!string.IsNullOrEmpty(dto.Title)) post.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.Content)) post.Content = dto.Content;
        if (!string.IsNullOrEmpty(dto.Category)) post.Category = dto.Category;
        if (dto.IsPinned.HasValue) post.IsPinned = dto.IsPinned.Value;
        if (dto.IsLocked.HasValue) post.IsLocked = dto.IsLocked.Value;
        post.UpdatedAt = DateTime.UtcNow;

        await _postRepository.UpdateAsync(post);
        
        var author = await _userRepository.GetByIdAsync(post.AuthorId);
        return new ApiResponse<ForumPostDto> { Success = true, Data = MapToDto(post, author) };
    }

    public async Task<ApiResponse<bool>> DeletePostAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return new ApiResponse<bool> { Success = false, Message = "Post not found" };

        await _postRepository.DeleteAsync(post);
        return new ApiResponse<bool> { Success = true, Message = "Post deleted" };
    }

    public async Task<ApiResponse<ForumCommentDto>> GetCommentByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
            return new ApiResponse<ForumCommentDto> { Success = false, Message = "Comment not found" };

        var author = await _userRepository.GetByIdAsync(comment.AuthorId);
        return new ApiResponse<ForumCommentDto> { Success = true, Data = MapToDto(comment, author) };
    }

    public async Task<ApiResponse<IEnumerable<ForumCommentDto>>> GetCommentsByPostIdAsync(Guid postId)
    {
        var comments = await _commentRepository.GetByPostIdAsync(postId);
        var dtos = new List<ForumCommentDto>();
        
        foreach (var comment in comments)
        {
            var author = await _userRepository.GetByIdAsync(comment.AuthorId);
            dtos.Add(MapToDto(comment, author));
        }

        return new ApiResponse<IEnumerable<ForumCommentDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<ForumCommentDto>> CreateCommentAsync(CreateForumCommentDto dto, Guid authorId)
    {
        var comment = new ForumComment
        {
            Id = Guid.NewGuid(),
            PostId = dto.PostId,
            AuthorId = authorId,
            ParentCommentId = dto.ParentCommentId,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        await _commentRepository.AddAsync(comment);
        await _postRepository.IncrementViewCountAsync(dto.PostId);
        
        var author = await _userRepository.GetByIdAsync(authorId);
        return new ApiResponse<ForumCommentDto> { Success = true, Message = "Comment created", Data = MapToDto(comment, author) };
    }

    public async Task<ApiResponse<bool>> UpvoteCommentAsync(Guid commentId, Guid userId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null)
            return new ApiResponse<bool> { Success = false, Message = "Comment not found" };

        comment.UpvoteCount++;
        await _commentRepository.UpdateAsync(comment);

        return new ApiResponse<bool> { Success = true };
    }

    private static ForumPostDto MapToDto(ForumPost post, User? author) => new ForumPostDto
    {
        Id = post.Id,
        AuthorId = post.AuthorId,
        AuthorName = author != null ? $"{author.FirstName} {author.LastName}" : "Unknown",
        AuthorRole = author?.Role.ToString() ?? "Unknown",
        Title = post.Title,
        Content = post.Content,
        Category = post.Category,
        ViewCount = post.ViewCount,
        CommentCount = post.Comments?.Count ?? 0,
        IsPinned = post.IsPinned,
        CreatedAt = post.CreatedAt
    };

    private static ForumCommentDto MapToDto(ForumComment comment, User? author) => new ForumCommentDto
    {
        Id = comment.Id,
        PostId = comment.PostId,
        AuthorId = comment.AuthorId,
        AuthorName = author != null ? $"{author.FirstName} {author.LastName}" : "Unknown",
        AuthorRole = author?.Role.ToString() ?? "Unknown",
        ParentCommentId = comment.ParentCommentId,
        Content = comment.Content,
        UpvoteCount = comment.UpvoteCount,
        IsAcceptedAnswer = comment.IsAcceptedAnswer,
        CreatedAt = comment.CreatedAt
    };
}

public class LoanService : ILoanService
{
    private readonly ILoanApplicationRepository _loanRepository;

    public LoanService(ILoanApplicationRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<ApiResponse<LoanApplicationDto>> GetByIdAsync(Guid id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);
        if (loan == null)
            return new ApiResponse<LoanApplicationDto> { Success = false, Message = "Loan application not found" };

        return new ApiResponse<LoanApplicationDto> { Success = true, Data = MapToDto(loan) };
    }

    public async Task<ApiResponse<IEnumerable<LoanApplicationDto>>> GetByFarmerIdAsync(Guid farmerId)
    {
        var loans = await _loanRepository.GetByFarmerIdAsync(farmerId);
        return new ApiResponse<IEnumerable<LoanApplicationDto>> { Success = true, Data = loans.Select(MapToDto) };
    }

    public async Task<ApiResponse<IEnumerable<LoanApplicationDto>>> GetByStatusAsync(string status)
    {
        var loans = await _loanRepository.GetByStatusAsync(Enum.Parse<LoanStatus>(status, true));
        return new ApiResponse<IEnumerable<LoanApplicationDto>> { Success = true, Data = loans.Select(MapToDto) };
    }

    public async Task<ApiResponse<LoanApplicationDto>> CreateAsync(CreateLoanApplicationDto dto, Guid farmerId)
    {
        var loan = new LoanApplication
        {
            Id = Guid.NewGuid(),
            FarmerId = farmerId,
            LenderName = dto.LenderName,
            AmountRequested = dto.AmountRequested,
            Purpose = dto.Purpose,
            RepaymentPeriodMonths = dto.RepaymentPeriodMonths,
            Status = LoanStatus.Pending,
            AppliedAt = DateTime.UtcNow
        };

        await _loanRepository.AddAsync(loan);
        return new ApiResponse<LoanApplicationDto> { Success = true, Message = "Loan application submitted", Data = MapToDto(loan) };
    }

    public async Task<ApiResponse<LoanApplicationDto>> UpdateStatusAsync(Guid id, UpdateLoanApplicationDto dto)
    {
        var loan = await _loanRepository.GetByIdAsync(id);
        if (loan == null)
            return new ApiResponse<LoanApplicationDto> { Success = false, Message = "Loan application not found" };

        if (!string.IsNullOrEmpty(dto.Status))
        {
            loan.Status = Enum.Parse<LoanStatus>(dto.Status, true);
            
            switch (loan.Status)
            {
                case LoanStatus.Approved:
                    loan.AmountApproved = dto.AmountApproved;
                    loan.ApprovedAt = DateTime.UtcNow;
                    break;
                case LoanStatus.Disbursed:
                    loan.DisbursedAt = DateTime.UtcNow;
                    break;
            }
        }

        if (!string.IsNullOrEmpty(dto.Remarks)) loan.Remarks = dto.Remarks;
        loan.ReviewedAt = DateTime.UtcNow;

        await _loanRepository.UpdateAsync(loan);
        return new ApiResponse<LoanApplicationDto> { Success = true, Data = MapToDto(loan) };
    }

    private static LoanApplicationDto MapToDto(LoanApplication loan) => new LoanApplicationDto
    {
        Id = loan.Id,
        FarmerId = loan.FarmerId,
        LenderName = loan.LenderName,
        AmountRequested = loan.AmountRequested,
        AmountApproved = loan.AmountApproved,
        Purpose = loan.Purpose,
        RepaymentPeriodMonths = loan.RepaymentPeriodMonths,
        Status = loan.Status.ToString(),
        Remarks = loan.Remarks,
        AppliedAt = loan.AppliedAt
    };
}

public class InsuranceService : IInsuranceService
{
    private readonly IInsuranceInfoRepository _insuranceRepository;

    public InsuranceService(IInsuranceInfoRepository insuranceRepository)
    {
        _insuranceRepository = insuranceRepository;
    }

    public async Task<ApiResponse<InsuranceInfoDto>> GetByIdAsync(Guid id)
    {
        var insurance = await _insuranceRepository.GetByIdAsync(id);
        if (insurance == null)
            return new ApiResponse<InsuranceInfoDto> { Success = false, Message = "Insurance not found" };

        return new ApiResponse<InsuranceInfoDto> { Success = true, Data = MapToDto(insurance) };
    }

    public async Task<ApiResponse<IEnumerable<InsuranceInfoDto>>> GetByFarmerIdAsync(Guid farmerId)
    {
        var insurances = await _insuranceRepository.GetByFarmerIdAsync(farmerId);
        return new ApiResponse<IEnumerable<InsuranceInfoDto>> { Success = true, Data = insurances.Select(MapToDto) };
    }

    public async Task<ApiResponse<IEnumerable<InsuranceInfoDto>>> GetActiveAsync()
    {
        var insurances = await _insuranceRepository.GetActiveAsync();
        return new ApiResponse<IEnumerable<InsuranceInfoDto>> { Success = true, Data = insurances.Select(MapToDto) };
    }

    public async Task<ApiResponse<InsuranceInfoDto>> CreateAsync(CreateInsuranceInfoDto dto, Guid farmerId)
    {
        var insurance = new InsuranceInfo
        {
            Id = Guid.NewGuid(),
            FarmerId = farmerId,
            Provider = dto.Provider,
            PolicyNumber = dto.PolicyNumber,
            CoverageType = dto.CoverageType,
            CoverageAmount = dto.CoverageAmount,
            PremiumAmount = dto.PremiumAmount,
            CoverageDetails = dto.CoverageDetails,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _insuranceRepository.AddAsync(insurance);
        return new ApiResponse<InsuranceInfoDto> { Success = true, Message = "Insurance added", Data = MapToDto(insurance) };
    }

    private static InsuranceInfoDto MapToDto(InsuranceInfo i) => new InsuranceInfoDto
    {
        Id = i.Id,
        FarmerId = i.FarmerId,
        Provider = i.Provider,
        PolicyNumber = i.PolicyNumber,
        CoverageType = i.CoverageType,
        CoverageAmount = i.CoverageAmount,
        PremiumAmount = i.PremiumAmount,
        CoverageDetails = i.CoverageDetails,
        StartDate = i.StartDate,
        EndDate = i.EndDate,
        IsActive = i.IsActive
    };
}

public class AnalyticsService : IAnalyticsService
{
    private readonly IFarmRepository _farmRepository;
    private readonly ICropRepository _cropRepository;
    private readonly IMarketplaceListingRepository _listingRepository;
    private readonly IOrderRepository _orderRepository;

    public AnalyticsService(
        IFarmRepository farmRepository,
        ICropRepository cropRepository,
        IMarketplaceListingRepository listingRepository,
        IOrderRepository orderRepository)
    {
        _farmRepository = farmRepository;
        _cropRepository = cropRepository;
        _listingRepository = listingRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ApiResponse<AnalyticsDashboardDto>> GetDashboardAsync(Guid userId)
    {
        return new ApiResponse<AnalyticsDashboardDto>
        {
            Success = true,
            Data = new AnalyticsDashboardDto
            {
                TotalFarms = 0,
                TotalCrops = 0,
                ActiveListings = 0,
                TotalOrders = 0,
                TotalRevenue = 0,
                EstimatedYield = 0,
                YieldTrends = new List<MonthlyTrendDto>(),
                RevenueTrends = new List<MonthlyTrendDto>()
            }
        };
    }

    public async Task<ApiResponse<IEnumerable<InputUsageDto>>> GetInputUsageAsync(Guid farmId)
    {
        return new ApiResponse<IEnumerable<InputUsageDto>>
        {
            Success = true,
            Data = new List<InputUsageDto>()
        };
    }

    public async Task<ApiResponse<ProfitEstimationDto>> EstimateProfitAsync(Guid cropId)
    {
        var crop = await _cropRepository.GetByIdAsync(cropId);
        if (crop == null)
            return new ApiResponse<ProfitEstimationDto> { Success = false, Message = "Crop not found" };

        var estimatedRevenue = crop.EstimatedYield * 50000m;
        var estimatedCosts = estimatedRevenue * 0.4m;
        var estimatedProfit = estimatedRevenue - estimatedCosts;
        var profitMargin = (estimatedProfit / estimatedRevenue) * 100;

        return new ApiResponse<ProfitEstimationDto>
        {
            Success = true,
            Data = new ProfitEstimationDto
            {
                EstimatedRevenue = estimatedRevenue,
                EstimatedCosts = estimatedCosts,
                EstimatedProfit = estimatedProfit,
                ProfitMargin = profitMargin,
                CostBreakdown = new List<CostBreakdownDto>
                {
                    new CostBreakdownDto { Category = "Seeds", Amount = estimatedCosts * 0.2m, Percentage = 20 },
                    new CostBreakdownDto { Category = "Fertilizer", Amount = estimatedCosts * 0.3m, Percentage = 30 },
                    new CostBreakdownDto { Category = "Labor", Amount = estimatedCosts * 0.3m, Percentage = 30 },
                    new CostBreakdownDto { Category = "Other", Amount = estimatedCosts * 0.2m, Percentage = 20 }
                }
            }
        };
    }
}