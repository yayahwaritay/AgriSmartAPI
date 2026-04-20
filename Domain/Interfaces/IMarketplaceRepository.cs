namespace AgriSmartSierra.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AgriSmartSierra.Domain.Entities;

    public interface IMarketplaceListingRepository : IRepository<MarketplaceListing>
    {
        Task<IEnumerable<MarketplaceListing>> GetActiveListingsAsync();
        Task<IEnumerable<MarketplaceListing>> GetBySellerIdAsync(Guid sellerId);
        Task<IEnumerable<MarketplaceListing>> SearchAsync(string searchTerm);
        Task<IEnumerable<MarketplaceListing>> GetByCategoryAsync(CropCategory category);
    }

    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByBuyerIdAsync(Guid buyerId);
        Task<IEnumerable<Order>> GetBySellerIdAsync(Guid sellerId);
        Task<Order?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    }

    public interface IForumPostRepository : IRepository<ForumPost>
    {
        Task<IEnumerable<ForumPost>> GetRecentAsync(int count);
        Task<IEnumerable<ForumPost>> GetByCategoryAsync(string category);
        Task IncrementViewCountAsync(Guid postId);
    }

    public interface IForumCommentRepository : IRepository<ForumComment>
    {
        Task<IEnumerable<ForumComment>> GetByPostIdAsync(Guid postId);
        Task<IEnumerable<ForumComment>> GetRepliesAsync(Guid parentCommentId);
    }

    public interface ILoanApplicationRepository : IRepository<LoanApplication>
    {
        Task<IEnumerable<LoanApplication>> GetByFarmerIdAsync(Guid farmerId);
        Task<IEnumerable<LoanApplication>> GetByStatusAsync(LoanStatus status);
    }

    public interface IInsuranceInfoRepository : IRepository<InsuranceInfo>
    {
        Task<IEnumerable<InsuranceInfo>> GetByFarmerIdAsync(Guid farmerId);
        Task<IEnumerable<InsuranceInfo>> GetActiveAsync();
    }
}