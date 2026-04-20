namespace AgriSmartSierra.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AgriSmartSierra.Domain.Entities;
    using AgriSmartSierra.Domain.Interfaces;
    using AgriSmartSierra.Infrastructure.Data;

    public class CropRepository : ICropRepository
    {
        private readonly AgriSmartDbContext _context;
        public CropRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<Crop?> GetByIdAsync(Guid id) { return _context.Crops.FindAsync(id).AsTask(); }
        
        public Task<IEnumerable<Crop>> GetAllAsync() { return Task.FromResult<IEnumerable<Crop>>(_context.Crops.ToList()); }
        
        public Task<IEnumerable<Crop>> FindAsync(Expression<Func<Crop, bool>> predicate) { return Task.FromResult<IEnumerable<Crop>>(_context.Crops.Where(predicate).ToList()); }
        
        public async Task<Crop> AddAsync(Crop entity) { await _context.Crops.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(Crop entity) { _context.Crops.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Crop entity) { _context.Crops.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.Crops.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<Crop, bool>> predicate) { return _context.Crops.CountAsync(predicate); }
        
        public Task<IEnumerable<Crop>> GetByFarmIdAsync(Guid farmId) { return Task.FromResult<IEnumerable<Crop>>(_context.Crops.Where(c => c.FarmId == farmId).ToList()); }
        public Task<IEnumerable<Crop>> GetByStatusAsync(CropStatus status) { return Task.FromResult<IEnumerable<Crop>>(_context.Crops.Where(c => c.Status == status).ToList()); }
        public Task<Crop?> GetWithActivitiesAsync(Guid id) { return _context.Crops.Include(c => c.Activities).FirstOrDefaultAsync(c => c.Id == id); }
    }

    public class FarmRepository : IFarmRepository
    {
        private readonly AgriSmartDbContext _context;
        public FarmRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<Farm?> GetByIdAsync(Guid id) { return _context.Farms.FindAsync(id).AsTask(); }
        public Task<IEnumerable<Farm>> GetAllAsync() { return Task.FromResult<IEnumerable<Farm>>(_context.Farms.ToList()); }
        public Task<IEnumerable<Farm>> FindAsync(Expression<Func<Farm, bool>> predicate) { return Task.FromResult<IEnumerable<Farm>>(_context.Farms.Where(predicate).ToList()); }
        public async Task<Farm> AddAsync(Farm entity) { await _context.Farms.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(Farm entity) { _context.Farms.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Farm entity) { _context.Farms.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.Farms.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<Farm, bool>> predicate) { return _context.Farms.CountAsync(predicate); }
        
        public Task<IEnumerable<Farm>> GetByFarmerProfileIdAsync(Guid farmerProfileId) { return Task.FromResult<IEnumerable<Farm>>(_context.Farms.Where(f => f.FarmerProfileId == farmerProfileId).ToList()); }
    }

    public class CropActivityRepository : ICropActivityRepository
    {
        private readonly AgriSmartDbContext _context;
        public CropActivityRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<CropActivity?> GetByIdAsync(Guid id) { return _context.CropActivities.FindAsync(id).AsTask(); }
        public Task<IEnumerable<CropActivity>> GetAllAsync() { return Task.FromResult<IEnumerable<CropActivity>>(_context.CropActivities.ToList()); }
        public Task<IEnumerable<CropActivity>> FindAsync(Expression<Func<CropActivity, bool>> predicate) { return Task.FromResult<IEnumerable<CropActivity>>(_context.CropActivities.Where(predicate).ToList()); }
        public async Task<CropActivity> AddAsync(CropActivity entity) { await _context.CropActivities.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(CropActivity entity) { _context.CropActivities.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(CropActivity entity) { _context.CropActivities.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.CropActivities.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<CropActivity, bool>> predicate) { return _context.CropActivities.CountAsync(predicate); }
        
        public Task<IEnumerable<CropActivity>> GetByCropIdAsync(Guid cropId) { return Task.FromResult<IEnumerable<CropActivity>>(_context.CropActivities.Where(a => a.CropId == cropId).OrderBy(a => a.ScheduledDate).ToList()); }
        public Task<IEnumerable<CropActivity>> GetByStatusAsync(ActivityStatus status) { return Task.FromResult<IEnumerable<CropActivity>>(_context.CropActivities.Where(a => a.Status == status).ToList()); }
        public Task<IEnumerable<CropActivity>> GetUpcomingAsync(int days) { return Task.FromResult<IEnumerable<CropActivity>>(_context.CropActivities.Include(a => a.Crop).Where(a => a.ScheduledDate <= DateTime.UtcNow.AddDays(days) && a.Status != ActivityStatus.Completed).OrderBy(a => a.ScheduledDate).ToList()); }
    }

    public class PestReportRepository : IPestReportRepository
    {
        private readonly AgriSmartDbContext _context;
        public PestReportRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<PestReport?> GetByIdAsync(Guid id) { return _context.PestReports.FindAsync(id).AsTask(); }
        public Task<IEnumerable<PestReport>> GetAllAsync() { return Task.FromResult<IEnumerable<PestReport>>(_context.PestReports.ToList()); }
        public Task<IEnumerable<PestReport>> FindAsync(Expression<Func<PestReport, bool>> predicate) { return Task.FromResult<IEnumerable<PestReport>>(_context.PestReports.Where(predicate).ToList()); }
        public async Task<PestReport> AddAsync(PestReport entity) { await _context.PestReports.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(PestReport entity) { _context.PestReports.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(PestReport entity) { _context.PestReports.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.PestReports.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<PestReport, bool>> predicate) { return _context.PestReports.CountAsync(predicate); }
        
        public Task<IEnumerable<PestReport>> GetByCropIdAsync(Guid cropId) { return Task.FromResult<IEnumerable<PestReport>>(_context.PestReports.Where(p => p.CropId == cropId).OrderByDescending(p => p.CreatedAt).ToList()); }
        public Task<IEnumerable<PestReport>> GetPendingAsync() { return Task.FromResult<IEnumerable<PestReport>>(_context.PestReports.Where(p => p.Status == ReportStatus.Pending).ToList()); }
    }

    public class WeatherLogRepository : IWeatherLogRepository
    {
        private readonly AgriSmartDbContext _context;
        public WeatherLogRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<WeatherLog?> GetByIdAsync(Guid id) { return _context.WeatherLogs.FindAsync(id).AsTask(); }
        public Task<IEnumerable<WeatherLog>> GetAllAsync() { return Task.FromResult<IEnumerable<WeatherLog>>(_context.WeatherLogs.ToList()); }
        public Task<IEnumerable<WeatherLog>> FindAsync(Expression<Func<WeatherLog, bool>> predicate) { return Task.FromResult<IEnumerable<WeatherLog>>(_context.WeatherLogs.Where(predicate).ToList()); }
        public async Task<WeatherLog> AddAsync(WeatherLog entity) { await _context.WeatherLogs.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(WeatherLog entity) { _context.WeatherLogs.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(WeatherLog entity) { _context.WeatherLogs.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.WeatherLogs.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<WeatherLog, bool>> predicate) { return _context.WeatherLogs.CountAsync(predicate); }
        
        public Task<WeatherLog?> GetLatestByLocationAsync(string location) { return _context.WeatherLogs.Where(w => w.Location == location).OrderByDescending(w => w.RecordedAt).FirstOrDefaultAsync(); }
        public Task<IEnumerable<WeatherLog>> GetByDateRangeAsync(string location, DateTime startDate, DateTime endDate) { return Task.FromResult<IEnumerable<WeatherLog>>(_context.WeatherLogs.Where(w => w.Location == location && w.RecordedAt >= startDate && w.RecordedAt <= endDate).OrderByDescending(w => w.RecordedAt).ToList()); }
    }

    public class MarketplaceListingRepository : IMarketplaceListingRepository
    {
        private readonly AgriSmartDbContext _context;
        public MarketplaceListingRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<MarketplaceListing?> GetByIdAsync(Guid id) { return _context.MarketplaceListings.FindAsync(id).AsTask(); }
        public Task<IEnumerable<MarketplaceListing>> GetAllAsync() { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.ToList()); }
        public Task<IEnumerable<MarketplaceListing>> FindAsync(Expression<Func<MarketplaceListing, bool>> predicate) { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.Where(predicate).ToList()); }
        public async Task<MarketplaceListing> AddAsync(MarketplaceListing entity) { await _context.MarketplaceListings.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(MarketplaceListing entity) { _context.MarketplaceListings.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(MarketplaceListing entity) { _context.MarketplaceListings.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.MarketplaceListings.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<MarketplaceListing, bool>> predicate) { return _context.MarketplaceListings.CountAsync(predicate); }
        
        public Task<IEnumerable<MarketplaceListing>> GetActiveListingsAsync() { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.Where(l => l.Status == ListingStatus.Active).Include(l => l.Crop).ToList()); }
        public Task<IEnumerable<MarketplaceListing>> GetBySellerIdAsync(Guid sellerId) { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.Where(l => l.SellerId == sellerId).ToList()); }
        public Task<IEnumerable<MarketplaceListing>> SearchAsync(string searchTerm) { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.Where(l => l.Status == ListingStatus.Active && (l.Title.Contains(searchTerm) || l.Description.Contains(searchTerm))).ToList()); }
        public Task<IEnumerable<MarketplaceListing>> GetByCategoryAsync(CropCategory category) { return Task.FromResult<IEnumerable<MarketplaceListing>>(_context.MarketplaceListings.Where(l => l.Status == ListingStatus.Active && l.Crop.Category == category).ToList()); }
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AgriSmartDbContext _context;
        public OrderRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<Order?> GetByIdAsync(Guid id) { return _context.Orders.FindAsync(id).AsTask(); }
        public Task<IEnumerable<Order>> GetAllAsync() { return Task.FromResult<IEnumerable<Order>>(_context.Orders.ToList()); }
        public Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate) { return Task.FromResult<IEnumerable<Order>>(_context.Orders.Where(predicate).ToList()); }
        public async Task<Order> AddAsync(Order entity) { await _context.Orders.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(Order entity) { _context.Orders.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Order entity) { _context.Orders.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.Orders.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<Order, bool>> predicate) { return _context.Orders.CountAsync(predicate); }
        
        public Task<IEnumerable<Order>> GetByBuyerIdAsync(Guid buyerId) { return Task.FromResult<IEnumerable<Order>>(_context.Orders.Where(o => o.BuyerId == buyerId).OrderByDescending(o => o.CreatedAt).ToList()); }
        public Task<IEnumerable<Order>> GetBySellerIdAsync(Guid sellerId) { return Task.FromResult<IEnumerable<Order>>(_context.Orders.Include(o => o.Listing).Where(o => o.Listing.SellerId == sellerId).OrderByDescending(o => o.CreatedAt).ToList()); }
        public Task<Order?> GetByOrderNumberAsync(string orderNumber) { return _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber); }
        public Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status) { return Task.FromResult<IEnumerable<Order>>(_context.Orders.Where(o => o.Status == status).ToList()); }
    }

    public class ForumPostRepository : IForumPostRepository
    {
        private readonly AgriSmartDbContext _context;
        public ForumPostRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<ForumPost?> GetByIdAsync(Guid id) { return _context.ForumPosts.FindAsync(id).AsTask(); }
        public Task<IEnumerable<ForumPost>> GetAllAsync() { return Task.FromResult<IEnumerable<ForumPost>>(_context.ForumPosts.ToList()); }
        public Task<IEnumerable<ForumPost>> FindAsync(Expression<Func<ForumPost, bool>> predicate) { return Task.FromResult<IEnumerable<ForumPost>>(_context.ForumPosts.Where(predicate).ToList()); }
        public async Task<ForumPost> AddAsync(ForumPost entity) { await _context.ForumPosts.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(ForumPost entity) { _context.ForumPosts.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(ForumPost entity) { _context.ForumPosts.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.ForumPosts.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<ForumPost, bool>> predicate) { return _context.ForumPosts.CountAsync(predicate); }
        
        public Task<IEnumerable<ForumPost>> GetRecentAsync(int count) { return Task.FromResult<IEnumerable<ForumPost>>(_context.ForumPosts.OrderByDescending(p => p.CreatedAt).Take(count).ToList()); }
        public Task<IEnumerable<ForumPost>> GetByCategoryAsync(string category) { return Task.FromResult<IEnumerable<ForumPost>>(_context.ForumPosts.Where(p => p.Category == category).OrderByDescending(p => p.CreatedAt).ToList()); }
        public async Task IncrementViewCountAsync(Guid postId) { var post = await _context.ForumPosts.FindAsync(postId); if (post != null) { post.ViewCount++; await _context.SaveChangesAsync(); } }
    }

    public class ForumCommentRepository : IForumCommentRepository
    {
        private readonly AgriSmartDbContext _context;
        public ForumCommentRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<ForumComment?> GetByIdAsync(Guid id) { return _context.ForumComments.FindAsync(id).AsTask(); }
        public Task<IEnumerable<ForumComment>> GetAllAsync() { return Task.FromResult<IEnumerable<ForumComment>>(_context.ForumComments.ToList()); }
        public Task<IEnumerable<ForumComment>> FindAsync(Expression<Func<ForumComment, bool>> predicate) { return Task.FromResult<IEnumerable<ForumComment>>(_context.ForumComments.Where(predicate).ToList()); }
        public async Task<ForumComment> AddAsync(ForumComment entity) { await _context.ForumComments.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(ForumComment entity) { _context.ForumComments.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(ForumComment entity) { _context.ForumComments.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.ForumComments.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<ForumComment, bool>> predicate) { return _context.ForumComments.CountAsync(predicate); }
        
        public Task<IEnumerable<ForumComment>> GetByPostIdAsync(Guid postId) { return Task.FromResult<IEnumerable<ForumComment>>(_context.ForumComments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedAt).ToList()); }
        public Task<IEnumerable<ForumComment>> GetRepliesAsync(Guid parentCommentId) { return Task.FromResult<IEnumerable<ForumComment>>(_context.ForumComments.Where(c => c.ParentCommentId == parentCommentId).ToList()); }
    }

    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly AgriSmartDbContext _context;
        public LoanApplicationRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<LoanApplication?> GetByIdAsync(Guid id) { return _context.LoanApplications.FindAsync(id).AsTask(); }
        public Task<IEnumerable<LoanApplication>> GetAllAsync() { return Task.FromResult<IEnumerable<LoanApplication>>(_context.LoanApplications.ToList()); }
        public Task<IEnumerable<LoanApplication>> FindAsync(Expression<Func<LoanApplication, bool>> predicate) { return Task.FromResult<IEnumerable<LoanApplication>>(_context.LoanApplications.Where(predicate).ToList()); }
        public async Task<LoanApplication> AddAsync(LoanApplication entity) { await _context.LoanApplications.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(LoanApplication entity) { _context.LoanApplications.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(LoanApplication entity) { _context.LoanApplications.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.LoanApplications.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<LoanApplication, bool>> predicate) { return _context.LoanApplications.CountAsync(predicate); }
        
        public Task<IEnumerable<LoanApplication>> GetByFarmerIdAsync(Guid farmerId) { return Task.FromResult<IEnumerable<LoanApplication>>(_context.LoanApplications.Where(l => l.FarmerId == farmerId).OrderByDescending(l => l.AppliedAt).ToList()); }
        public Task<IEnumerable<LoanApplication>> GetByStatusAsync(LoanStatus status) { return Task.FromResult<IEnumerable<LoanApplication>>(_context.LoanApplications.Where(l => l.Status == status).ToList()); }
    }

    public class InsuranceInfoRepository : IInsuranceInfoRepository
    {
        private readonly AgriSmartDbContext _context;
        public InsuranceInfoRepository(AgriSmartDbContext context) { _context = context; }
        
        public Task<InsuranceInfo?> GetByIdAsync(Guid id) { return _context.InsuranceInfos.FindAsync(id).AsTask(); }
        public Task<IEnumerable<InsuranceInfo>> GetAllAsync() { return Task.FromResult<IEnumerable<InsuranceInfo>>(_context.InsuranceInfos.ToList()); }
        public Task<IEnumerable<InsuranceInfo>> FindAsync(Expression<Func<InsuranceInfo, bool>> predicate) { return Task.FromResult<IEnumerable<InsuranceInfo>>(_context.InsuranceInfos.Where(predicate).ToList()); }
        public async Task<InsuranceInfo> AddAsync(InsuranceInfo entity) { await _context.InsuranceInfos.AddAsync(entity); await _context.SaveChangesAsync(); return entity; }
        public async Task UpdateAsync(InsuranceInfo entity) { _context.InsuranceInfos.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(InsuranceInfo entity) { _context.InsuranceInfos.Remove(entity); await _context.SaveChangesAsync(); }
        public Task<int> CountAsync() { return _context.InsuranceInfos.CountAsync(); }
        public Task<int> CountAsync(Expression<Func<InsuranceInfo, bool>> predicate) { return _context.InsuranceInfos.CountAsync(predicate); }
        
        public Task<IEnumerable<InsuranceInfo>> GetByFarmerIdAsync(Guid farmerId) { return Task.FromResult<IEnumerable<InsuranceInfo>>(_context.InsuranceInfos.Where(i => i.FarmerId == farmerId).ToList()); }
        public Task<IEnumerable<InsuranceInfo>> GetActiveAsync() { return Task.FromResult<IEnumerable<InsuranceInfo>>(_context.InsuranceInfos.Where(i => i.IsActive && i.EndDate > DateTime.UtcNow).ToList()); }
    }
}