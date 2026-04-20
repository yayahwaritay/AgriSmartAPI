namespace AgriSmartSierra.Domain.Interfaces
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using AgriSmartSierra.Domain.Entities;

    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }

    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailWithProfileAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }

    public interface IFarmerProfileRepository : IRepository<FarmerProfile>
    {
        Task<FarmerProfile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<FarmerProfile>> GetByDistrictAsync(string district);
    }

    public interface IBuyerProfileRepository : IRepository<BuyerProfile>
    {
        Task<BuyerProfile?> GetByUserIdAsync(Guid userId);
    }

    public interface IAgronomistProfileRepository : IRepository<AgronomistProfile>
    {
        Task<AgronomistProfile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<AgronomistProfile>> GetVerifiedAsync();
        Task<IEnumerable<AgronomistProfile>> GetByServiceAreaAsync(string area);
    }

    public interface IFarmRepository : IRepository<Farm>
    {
        Task<IEnumerable<Farm>> GetByFarmerProfileIdAsync(Guid farmerProfileId);
    }

    public interface ICropRepository : IRepository<Crop>
    {
        Task<IEnumerable<Crop>> GetByFarmIdAsync(Guid farmId);
        Task<IEnumerable<Crop>> GetByStatusAsync(CropStatus status);
        Task<Crop?> GetWithActivitiesAsync(Guid id);
    }

    public interface ICropActivityRepository : IRepository<CropActivity>
    {
        Task<IEnumerable<CropActivity>> GetByCropIdAsync(Guid cropId);
        Task<IEnumerable<CropActivity>> GetByStatusAsync(ActivityStatus status);
        Task<IEnumerable<CropActivity>> GetUpcomingAsync(int days);
    }

    public interface IPestReportRepository : IRepository<PestReport>
    {
        Task<IEnumerable<PestReport>> GetByCropIdAsync(Guid cropId);
        Task<IEnumerable<PestReport>> GetPendingAsync();
    }

    public interface IWeatherLogRepository : IRepository<WeatherLog>
    {
        Task<WeatherLog?> GetLatestByLocationAsync(string location);
        Task<IEnumerable<WeatherLog>> GetByDateRangeAsync(string location, DateTime startDate, DateTime endDate);
    }

    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}