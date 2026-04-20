using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;
using System.Linq.Expressions;

namespace AgriSmartSierra.Application.Services;

public class MarketplaceService : IMarketplaceService
{
    private readonly IMarketplaceListingRepository _listingRepository;
    private readonly ICropRepository _cropRepository;

    public MarketplaceService(
        IMarketplaceListingRepository listingRepository,
        ICropRepository cropRepository)
    {
        _listingRepository = listingRepository;
        _cropRepository = cropRepository;
    }

    public async Task<ApiResponse<MarketplaceListingDto>> GetListingByIdAsync(Guid id)
    {
        var listing = await _listingRepository.GetByIdAsync(id);
        if (listing == null)
            return new ApiResponse<MarketplaceListingDto> { Success = false, Message = "Listing not found" };

        return new ApiResponse<MarketplaceListingDto> { Success = true, Data = MapToDto(listing) };
    }

    public async Task<ApiResponse<IEnumerable<MarketplaceListingDto>>> GetActiveListingsAsync(string? searchTerm = null, string? category = null)
    {
        IEnumerable<MarketplaceListing> listings;
        
        if (!string.IsNullOrEmpty(searchTerm))
            listings = await _listingRepository.SearchAsync(searchTerm);
        else if (!string.IsNullOrEmpty(category))
            listings = await _listingRepository.GetByCategoryAsync(Enum.Parse<CropCategory>(category, true));
        else
            listings = await _listingRepository.GetActiveListingsAsync();

        return new ApiResponse<IEnumerable<MarketplaceListingDto>>
        {
            Success = true,
            Data = listings.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<MarketplaceListingDto>> CreateListingAsync(CreateMarketplaceListingDto dto, Guid sellerId)
    {
        var crop = await _cropRepository.GetByIdAsync(dto.CropId);
        if (crop == null)
            return new ApiResponse<MarketplaceListingDto> { Success = false, Message = "Crop not found" };

        var listing = new MarketplaceListing
        {
            Id = Guid.NewGuid(),
            CropId = dto.CropId,
            SellerId = sellerId,
            Title = dto.Title,
            Description = dto.Description,
            PricePerUnit = dto.PricePerUnit,
            Unit = dto.Unit,
            AvailableQuantity = dto.AvailableQuantity,
            MinimumOrderQuantity = dto.MinimumOrderQuantity,
            QualityGrade = dto.QualityGrade,
            IsOrganic = dto.IsOrganic,
            Status = ListingStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _listingRepository.AddAsync(listing);
        return new ApiResponse<MarketplaceListingDto> { Success = true, Message = "Listing created", Data = MapToDto(listing) };
    }

    public async Task<ApiResponse<MarketplaceListingDto>> UpdateListingAsync(Guid id, UpdateMarketplaceListingDto dto)
    {
        var listing = await _listingRepository.GetByIdAsync(id);
        if (listing == null)
            return new ApiResponse<MarketplaceListingDto> { Success = false, Message = "Listing not found" };

        if (!string.IsNullOrEmpty(dto.Title)) listing.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.Description)) listing.Description = dto.Description;
        if (dto.PricePerUnit.HasValue) listing.PricePerUnit = dto.PricePerUnit.Value;
        if (dto.AvailableQuantity.HasValue) listing.AvailableQuantity = dto.AvailableQuantity.Value;
        if (!string.IsNullOrEmpty(dto.Status)) listing.Status = Enum.Parse<ListingStatus>(dto.Status, true);
        listing.UpdatedAt = DateTime.UtcNow;

        await _listingRepository.UpdateAsync(listing);
        return new ApiResponse<MarketplaceListingDto> { Success = true, Data = MapToDto(listing) };
    }

    public async Task<ApiResponse<bool>> DeleteListingAsync(Guid id)
    {
        var listing = await _listingRepository.GetByIdAsync(id);
        if (listing == null)
            return new ApiResponse<bool> { Success = false, Message = "Listing not found" };

        listing.Status = ListingStatus.Cancelled;
        listing.UpdatedAt = DateTime.UtcNow;
        await _listingRepository.UpdateAsync(listing);

        return new ApiResponse<bool> { Success = true, Message = "Listing cancelled" };
    }

    public async Task<ApiResponse<IEnumerable<MarketplaceListingDto>>> GetSellerListingsAsync(Guid sellerId)
    {
        var listings = await _listingRepository.GetBySellerIdAsync(sellerId);
        return new ApiResponse<IEnumerable<MarketplaceListingDto>> { Success = true, Data = listings.Select(MapToDto) };
    }

    private static MarketplaceListingDto MapToDto(MarketplaceListing l) => new MarketplaceListingDto
    {
        Id = l.Id,
        CropId = l.CropId,
        SellerId = l.SellerId,
        Title = l.Title,
        Description = l.Description,
        PricePerUnit = l.PricePerUnit,
        Unit = l.Unit,
        AvailableQuantity = l.AvailableQuantity,
        MinimumOrderQuantity = l.MinimumOrderQuantity,
        ImageUrl = l.ImageUrl,
        Status = l.Status.ToString(),
        QualityGrade = l.QualityGrade,
        IsOrganic = l.IsOrganic,
        CreatedAt = l.CreatedAt
    };
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMarketplaceListingRepository _listingRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IMarketplaceListingRepository listingRepository)
    {
        _orderRepository = orderRepository;
        _listingRepository = listingRepository;
    }

    public async Task<ApiResponse<OrderDto>> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return new ApiResponse<OrderDto> { Success = false, Message = "Order not found" };

        return new ApiResponse<OrderDto> { Success = true, Data = MapToDto(order) };
    }

    public async Task<ApiResponse<IEnumerable<OrderDto>>> GetByBuyerIdAsync(Guid buyerId)
    {
        var orders = await _orderRepository.GetByBuyerIdAsync(buyerId);
        return new ApiResponse<IEnumerable<OrderDto>> { Success = true, Data = orders.Select(MapToDto) };
    }

    public async Task<ApiResponse<IEnumerable<OrderDto>>> GetBySellerIdAsync(Guid sellerId)
    {
        var orders = await _orderRepository.GetBySellerIdAsync(sellerId);
        return new ApiResponse<IEnumerable<OrderDto>> { Success = true, Data = orders.Select(MapToDto) };
    }

    public async Task<ApiResponse<OrderDto>> CreateAsync(CreateOrderDto dto, Guid buyerId)
    {
        var listing = await _listingRepository.GetByIdAsync(dto.ListingId);
        if (listing == null)
            return new ApiResponse<OrderDto> { Success = false, Message = "Listing not found" };

        if (listing.AvailableQuantity < dto.Quantity)
            return new ApiResponse<OrderDto> { Success = false, Message = "Insufficient quantity available" };

        var order = new Order
        {
            Id = Guid.NewGuid(),
            ListingId = dto.ListingId,
            BuyerId = buyerId,
            OrderNumber = GenerateOrderNumber(),
            Quantity = dto.Quantity,
            UnitPrice = listing.PricePerUnit,
            TotalAmount = dto.Quantity * listing.PricePerUnit,
            DeliveryAddress = dto.DeliveryAddress,
            DeliveryNotes = dto.DeliveryNotes,
            Status = OrderStatus.Pending,
            OrderedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        listing.AvailableQuantity -= dto.Quantity;
        await _listingRepository.UpdateAsync(listing);
        await _orderRepository.AddAsync(order);

        return new ApiResponse<OrderDto> { Success = true, Message = "Order created", Data = MapToDto(order) };
    }

    public async Task<ApiResponse<OrderDto>> UpdateStatusAsync(Guid id, UpdateOrderStatusDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return new ApiResponse<OrderDto> { Success = false, Message = "Order not found" };

        order.Status = Enum.Parse<OrderStatus>(dto.Status, true);
        
        switch (order.Status)
        {
            case OrderStatus.Confirmed:
                order.ConfirmedAt = DateTime.UtcNow;
                break;
            case OrderStatus.Shipped:
                order.ShippedAt = DateTime.UtcNow;
                break;
            case OrderStatus.Delivered:
                order.DeliveredAt = DateTime.UtcNow;
                break;
            case OrderStatus.Cancelled:
                order.CancelledAt = DateTime.UtcNow;
                order.CancellationReason = dto.Notes;
                break;
        }

        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.UpdateAsync(order);

        return new ApiResponse<OrderDto> { Success = true, Data = MapToDto(order) };
    }

    private static string GenerateOrderNumber() => $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";

    private static OrderDto MapToDto(Order o) => new OrderDto
    {
        Id = o.Id,
        ListingId = o.ListingId,
        BuyerId = o.BuyerId,
        OrderNumber = o.OrderNumber,
        Quantity = o.Quantity,
        UnitPrice = o.UnitPrice,
        TotalAmount = o.TotalAmount,
        Status = o.Status.ToString(),
        DeliveryAddress = o.DeliveryAddress,
        DeliveryNotes = o.DeliveryNotes,
        OrderedAt = o.OrderedAt,
        ConfirmedAt = o.ConfirmedAt,
        ShippedAt = o.ShippedAt,
        DeliveredAt = o.DeliveredAt,
        CreatedAt = o.CreatedAt
    };
}

public class CropPriceService : ICropPriceService
{
    private readonly Dictionary<string, (decimal price, decimal change)> _prices = new()
    {
        { "Rice", (85000, 0.02m) },
        { "Cassava", (15000, -0.01m) },
        { "Maize", (45000, 0.03m) },
        { "Groundnuts", (120000, 0.00m) },
        { "Coffee", (250000, 0.05m) },
        { "Cocoa", (180000, 0.04m) },
        { "Palm Oil", (65000, 0.01m) }
    };

    public async Task<ApiResponse<IEnumerable<CropPriceDto>>> GetPricesAsync()
    {
        var prices = _prices.Select(kvp => MapToDto(kvp.Key, kvp.Value.price, kvp.Value.change));
        return new ApiResponse<IEnumerable<CropPriceDto>> { Success = true, Data = prices };
    }

    public async Task<ApiResponse<CropPriceDto>> GetPriceByCropAsync(string cropName)
    {
        if (!_prices.TryGetValue(cropName, out var priceData))
            return new ApiResponse<CropPriceDto> { Success = false, Message = "Price not found" };

        return new ApiResponse<CropPriceDto> { Success = true, Data = MapToDto(cropName, priceData.price, priceData.change) };
    }

    private static CropPriceDto MapToDto(string name, decimal price, decimal change) => new CropPriceDto
    {
        CropName = name,
        Category = "Grain",
        CurrentPrice = price,
        PreviousPrice = price / (1 + change),
        PriceChangePercent = change * 100,
        Unit = "SLL/kg",
        UpdatedAt = DateTime.UtcNow
    };
}