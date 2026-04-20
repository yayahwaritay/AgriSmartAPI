using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface IForumService
{
    Task<ApiResponse<ForumPostDto>> GetPostByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<ForumPostDto>>> GetRecentPostsAsync(int count = 20);
    Task<ApiResponse<IEnumerable<ForumPostDto>>> GetPostsByCategoryAsync(string category);
    Task<ApiResponse<ForumPostDto>> CreatePostAsync(CreateForumPostDto dto, Guid authorId);
    Task<ApiResponse<ForumPostDto>> UpdatePostAsync(Guid id, UpdateForumPostDto dto);
    Task<ApiResponse<bool>> DeletePostAsync(Guid id);
    Task<ApiResponse<ForumCommentDto>> GetCommentByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<ForumCommentDto>>> GetCommentsByPostIdAsync(Guid postId);
    Task<ApiResponse<ForumCommentDto>> CreateCommentAsync(CreateForumCommentDto dto, Guid authorId);
    Task<ApiResponse<bool>> UpvoteCommentAsync(Guid commentId, Guid userId);
}

public interface ILoanService
{
    Task<ApiResponse<LoanApplicationDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<LoanApplicationDto>>> GetByFarmerIdAsync(Guid farmerId);
    Task<ApiResponse<IEnumerable<LoanApplicationDto>>> GetByStatusAsync(string status);
    Task<ApiResponse<LoanApplicationDto>> CreateAsync(CreateLoanApplicationDto dto, Guid farmerId);
    Task<ApiResponse<LoanApplicationDto>> UpdateStatusAsync(Guid id, UpdateLoanApplicationDto dto);
}

public interface IInsuranceService
{
    Task<ApiResponse<InsuranceInfoDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<InsuranceInfoDto>>> GetByFarmerIdAsync(Guid farmerId);
    Task<ApiResponse<IEnumerable<InsuranceInfoDto>>> GetActiveAsync();
    Task<ApiResponse<InsuranceInfoDto>> CreateAsync(CreateInsuranceInfoDto dto, Guid farmerId);
}

public interface IAnalyticsService
{
    Task<ApiResponse<AnalyticsDashboardDto>> GetDashboardAsync(Guid userId);
    Task<ApiResponse<IEnumerable<InputUsageDto>>> GetInputUsageAsync(Guid farmId);
    Task<ApiResponse<ProfitEstimationDto>> EstimateProfitAsync(Guid cropId);
}