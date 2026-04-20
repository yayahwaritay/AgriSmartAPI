using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface IMarketplaceService
{
    Task<ApiResponse<MarketplaceListingDto>> GetListingByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<MarketplaceListingDto>>> GetActiveListingsAsync(string? searchTerm = null, string? category = null);
    Task<ApiResponse<MarketplaceListingDto>> CreateListingAsync(CreateMarketplaceListingDto dto, Guid sellerId);
    Task<ApiResponse<MarketplaceListingDto>> UpdateListingAsync(Guid id, UpdateMarketplaceListingDto dto);
    Task<ApiResponse<bool>> DeleteListingAsync(Guid id);
    Task<ApiResponse<IEnumerable<MarketplaceListingDto>>> GetSellerListingsAsync(Guid sellerId);
}

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<OrderDto>>> GetByBuyerIdAsync(Guid buyerId);
    Task<ApiResponse<IEnumerable<OrderDto>>> GetBySellerIdAsync(Guid sellerId);
    Task<ApiResponse<OrderDto>> CreateAsync(CreateOrderDto dto, Guid buyerId);
    Task<ApiResponse<OrderDto>> UpdateStatusAsync(Guid id, UpdateOrderStatusDto dto);
}

public interface ICropPriceService
{
    Task<ApiResponse<IEnumerable<CropPriceDto>>> GetPricesAsync();
    Task<ApiResponse<CropPriceDto>> GetPriceByCropAsync(string cropName);
}