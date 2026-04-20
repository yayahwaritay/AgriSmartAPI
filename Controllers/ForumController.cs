using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgriSmartSierra.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ForumController : ControllerBase
{
    private readonly IForumService _forumService;

    public ForumController(IForumService forumService)
    {
        _forumService = forumService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("post/{id}")]
    public async Task<ActionResult<ApiResponse<ForumPostDto>>> GetPostById(Guid id)
    {
        var result = await _forumService.GetPostByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("posts/recent")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<ForumPostDto>>>> GetRecentPosts([FromQuery] int count = 20)
    {
        var result = await _forumService.GetRecentPostsAsync(count);
        return Ok(result);
    }

    [HttpGet("posts/category/{category}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<ForumPostDto>>>> GetPostsByCategory(string category)
    {
        var result = await _forumService.GetPostsByCategoryAsync(category);
        return Ok(result);
    }

    [HttpPost("post")]
    public async Task<ActionResult<ApiResponse<ForumPostDto>>> CreatePost([FromBody] CreateForumPostDto dto)
    {
        var authorId = GetUserId();
        var result = await _forumService.CreatePostAsync(dto, authorId);
        return Ok(result);
    }

    [HttpPut("post/{id}")]
    public async Task<ActionResult<ApiResponse<ForumPostDto>>> UpdatePost(Guid id, [FromBody] UpdateForumPostDto dto)
    {
        var result = await _forumService.UpdatePostAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("post/{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeletePost(Guid id)
    {
        var result = await _forumService.DeletePostAsync(id);
        return Ok(result);
    }

    [HttpGet("comment/{id}")]
    public async Task<ActionResult<ApiResponse<ForumCommentDto>>> GetCommentById(Guid id)
    {
        var result = await _forumService.GetCommentByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("post/{postId}/comments")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<ForumCommentDto>>>> GetCommentsByPost(Guid postId)
    {
        var result = await _forumService.GetCommentsByPostIdAsync(postId);
        return Ok(result);
    }

    [HttpPost("comment")]
    public async Task<ActionResult<ApiResponse<ForumCommentDto>>> CreateComment([FromBody] CreateForumCommentDto dto)
    {
        var authorId = GetUserId();
        var result = await _forumService.CreateCommentAsync(dto, authorId);
        return Ok(result);
    }

    [HttpPost("comment/{commentId}/upvote")]
    public async Task<ActionResult<ApiResponse<bool>>> UpvoteComment(Guid commentId)
    {
        var userId = GetUserId();
        var result = await _forumService.UpvoteCommentAsync(commentId, userId);
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly IInsuranceService _insuranceService;

    public FinanceController(ILoanService loanService, IInsuranceService insuranceService)
    {
        _loanService = loanService;
        _insuranceService = insuranceService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("loan/{id}")]
    public async Task<ActionResult<ApiResponse<LoanApplicationDto>>> GetLoanById(Guid id)
    {
        var result = await _loanService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("loans/my")]
    public async Task<ActionResult<ApiResponse<IEnumerable<LoanApplicationDto>>>> GetMyLoans()
    {
        var farmerId = GetUserId();
        var result = await _loanService.GetByFarmerIdAsync(farmerId);
        return Ok(result);
    }

    [HttpPost("loan")]
    public async Task<ActionResult<ApiResponse<LoanApplicationDto>>> CreateLoan([FromBody] CreateLoanApplicationDto dto)
    {
        var farmerId = GetUserId();
        var result = await _loanService.CreateAsync(dto, farmerId);
        return Ok(result);
    }

    [HttpPut("loan/{id}")]
    public async Task<ActionResult<ApiResponse<LoanApplicationDto>>> UpdateLoan(Guid id, [FromBody] UpdateLoanApplicationDto dto)
    {
        var result = await _loanService.UpdateStatusAsync(id, dto);
        return Ok(result);
    }

    [HttpGet("insurance/{id}")]
    public async Task<ActionResult<ApiResponse<InsuranceInfoDto>>> GetInsuranceById(Guid id)
    {
        var result = await _insuranceService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("insurances/my")]
    public async Task<ActionResult<ApiResponse<IEnumerable<InsuranceInfoDto>>>> GetMyInsurances()
    {
        var farmerId = GetUserId();
        var result = await _insuranceService.GetByFarmerIdAsync(farmerId);
        return Ok(result);
    }

    [HttpPost("insurance")]
    public async Task<ActionResult<ApiResponse<InsuranceInfoDto>>> CreateInsurance([FromBody] CreateInsuranceInfoDto dto)
    {
        var farmerId = GetUserId();
        var result = await _insuranceService.CreateAsync(dto, farmerId);
        return Ok(result);
    }

    [HttpGet("insurances/active")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<InsuranceInfoDto>>>> GetActiveInsurances()
    {
        var result = await _insuranceService.GetActiveAsync();
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<ApiResponse<AnalyticsDashboardDto>>> GetDashboard()
    {
        var result = await _analyticsService.GetDashboardAsync(Guid.Empty);
        return Ok(result);
    }

    [HttpGet("input-usage/{farmId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<InputUsageDto>>>> GetInputUsage(Guid farmId)
    {
        var result = await _analyticsService.GetInputUsageAsync(farmId);
        return Ok(result);
    }

    [HttpGet("profit-estimate/{cropId}")]
    public async Task<ActionResult<ApiResponse<ProfitEstimationDto>>> EstimateProfit(Guid cropId)
    {
        var result = await _analyticsService.EstimateProfitAsync(cropId);
        return Ok(result);
    }
}