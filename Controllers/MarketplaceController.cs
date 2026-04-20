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
public class MarketplaceController : ControllerBase
{
    private readonly IMarketplaceService _marketplaceService;

    public MarketplaceController(IMarketplaceService marketplaceService)
    {
        _marketplaceService = marketplaceService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<MarketplaceListingDto>>> GetById(Guid id)
    {
        var result = await _marketplaceService.GetListingByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<MarketplaceListingDto>>>> GetActiveListings([FromQuery] string? searchTerm, [FromQuery] string? category)
    {
        var result = await _marketplaceService.GetActiveListingsAsync(searchTerm, category);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<MarketplaceListingDto>>> CreateListing([FromForm] CreateMarketplaceListingDto dto)
    {
        var sellerId = GetUserId();
        var result = await _marketplaceService.CreateListingAsync(dto, sellerId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<MarketplaceListingDto>>> UpdateListing(Guid id, [FromForm] UpdateMarketplaceListingDto dto)
    {
        var result = await _marketplaceService.UpdateListingAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteListing(Guid id)
    {
        var result = await _marketplaceService.DeleteListingAsync(id);
        return Ok(result);
    }

    [HttpGet("my-listings")]
    public async Task<ActionResult<ApiResponse<IEnumerable<MarketplaceListingDto>>>> GetMyListings()
    {
        var sellerId = GetUserId();
        var result = await _marketplaceService.GetSellerListingsAsync(sellerId);
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("buyer")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetByBuyer()
    {
        var buyerId = GetUserId();
        var result = await _orderService.GetByBuyerIdAsync(buyerId);
        return Ok(result);
    }

    [HttpGet("seller")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetBySeller()
    {
        var sellerId = GetUserId();
        var result = await _orderService.GetBySellerIdAsync(sellerId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderDto>>> Create([FromBody] CreateOrderDto dto)
    {
        var buyerId = GetUserId();
        var result = await _orderService.CreateAsync(dto, buyerId);
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusDto dto)
    {
        var result = await _orderService.UpdateStatusAsync(id, dto);
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PricesController : ControllerBase
{
    private readonly ICropPriceService _priceService;

    public PricesController(ICropPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CropPriceDto>>>> GetPrices()
    {
        var result = await _priceService.GetPricesAsync();
        return Ok(result);
    }

    [HttpGet("{cropName}")]
    public async Task<ActionResult<ApiResponse<CropPriceDto>>> GetPriceByCrop(string cropName)
    {
        var result = await _priceService.GetPriceByCropAsync(cropName);
        return Ok(result);
    }
}