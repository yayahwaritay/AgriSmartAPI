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

    public class UserRepository : IUserRepository
    {
        private readonly AgriSmartDbContext _context;
        public UserRepository(AgriSmartDbContext context)
        {
            _context = context;
        }
        
        public Task<User?> GetByIdAsync(Guid id)
        {
            return _context.Users.FindAsync(id).AsTask();
        }
        
        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_context.Users.ToList());
        }
        
        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<User>>(_context.Users.Where(predicate).ToList());
        }
        
        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public Task<int> CountAsync()
        {
            return _context.Users.CountAsync();
        }
        
        public Task<int> CountAsync(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.CountAsync(predicate);
        }
        
        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public Task<User?> GetByEmailWithProfileAsync(string email)
        {
            return _context.Users.Include(u => u.FarmerProfile).FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Users.AnyAsync(u => u.Email == email);
        }
    }

    public class FarmerProfileRepository : IFarmerProfileRepository
    {
        private readonly AgriSmartDbContext _context;
        public FarmerProfileRepository(AgriSmartDbContext context)
        {
            _context = context;
        }
        
        public Task<FarmerProfile?> GetByIdAsync(Guid id)
        {
            return _context.FarmerProfiles.FindAsync(id).AsTask();
        }
        
        public Task<IEnumerable<FarmerProfile>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<FarmerProfile>>(_context.FarmerProfiles.ToList());
        }
        
        public Task<IEnumerable<FarmerProfile>> FindAsync(Expression<Func<FarmerProfile, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<FarmerProfile>>(_context.FarmerProfiles.Where(predicate).ToList());
        }
        
        public async Task<FarmerProfile> AddAsync(FarmerProfile entity)
        {
            await _context.FarmerProfiles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task UpdateAsync(FarmerProfile entity)
        {
            _context.FarmerProfiles.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(FarmerProfile entity)
        {
            _context.FarmerProfiles.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public Task<int> CountAsync()
        {
            return _context.FarmerProfiles.CountAsync();
        }
        
        public Task<int> CountAsync(Expression<Func<FarmerProfile, bool>> predicate)
        {
            return _context.FarmerProfiles.CountAsync(predicate);
        }
        
        public Task<FarmerProfile?> GetByUserIdAsync(Guid userId)
        {
            return _context.FarmerProfiles.FirstOrDefaultAsync(f => f.UserId == userId);
        }
        
        public Task<IEnumerable<FarmerProfile>> GetByDistrictAsync(string district)
        {
            return Task.FromResult<IEnumerable<FarmerProfile>>(_context.FarmerProfiles.Where(f => f.District == district).ToList());
        }
    }

    public class BuyerProfileRepository : IBuyerProfileRepository
    {
        private readonly AgriSmartDbContext _context;
        public BuyerProfileRepository(AgriSmartDbContext context)
        {
            _context = context;
        }
        
        public Task<BuyerProfile?> GetByIdAsync(Guid id)
        {
            return _context.BuyerProfiles.FindAsync(id).AsTask();
        }
        
        public Task<IEnumerable<BuyerProfile>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<BuyerProfile>>(_context.BuyerProfiles.ToList());
        }
        
        public Task<IEnumerable<BuyerProfile>> FindAsync(Expression<Func<BuyerProfile, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<BuyerProfile>>(_context.BuyerProfiles.Where(predicate).ToList());
        }
        
        public async Task<BuyerProfile> AddAsync(BuyerProfile entity)
        {
            await _context.BuyerProfiles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task UpdateAsync(BuyerProfile entity)
        {
            _context.BuyerProfiles.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(BuyerProfile entity)
        {
            _context.BuyerProfiles.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public Task<int> CountAsync()
        {
            return _context.BuyerProfiles.CountAsync();
        }
        
        public Task<int> CountAsync(Expression<Func<BuyerProfile, bool>> predicate)
        {
            return _context.BuyerProfiles.CountAsync(predicate);
        }
        
        public Task<BuyerProfile?> GetByUserIdAsync(Guid userId)
        {
            return _context.BuyerProfiles.FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }

    public class AgronomistProfileRepository : IAgronomistProfileRepository
    {
        private readonly AgriSmartDbContext _context;
        public AgronomistProfileRepository(AgriSmartDbContext context)
        {
            _context = context;
        }
        
        public Task<AgronomistProfile?> GetByIdAsync(Guid id)
        {
            return _context.AgronomistProfiles.FindAsync(id).AsTask();
        }
        
        public Task<IEnumerable<AgronomistProfile>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<AgronomistProfile>>(_context.AgronomistProfiles.ToList());
        }
        
        public Task<IEnumerable<AgronomistProfile>> FindAsync(Expression<Func<AgronomistProfile, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<AgronomistProfile>>(_context.AgronomistProfiles.Where(predicate).ToList());
        }
        
        public async Task<AgronomistProfile> AddAsync(AgronomistProfile entity)
        {
            await _context.AgronomistProfiles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task UpdateAsync(AgronomistProfile entity)
        {
            _context.AgronomistProfiles.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(AgronomistProfile entity)
        {
            _context.AgronomistProfiles.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public Task<int> CountAsync()
        {
            return _context.AgronomistProfiles.CountAsync();
        }
        
        public Task<int> CountAsync(Expression<Func<AgronomistProfile, bool>> predicate)
        {
            return _context.AgronomistProfiles.CountAsync(predicate);
        }
        
        public Task<AgronomistProfile?> GetByUserIdAsync(Guid userId)
        {
            return _context.AgronomistProfiles.FirstOrDefaultAsync(a => a.UserId == userId);
        }
        
        public Task<IEnumerable<AgronomistProfile>> GetVerifiedAsync()
        {
            return Task.FromResult<IEnumerable<AgronomistProfile>>(_context.AgronomistProfiles.Where(a => a.IsVerified).ToList());
        }
        
        public Task<IEnumerable<AgronomistProfile>> GetByServiceAreaAsync(string area)
        {
            return Task.FromResult<IEnumerable<AgronomistProfile>>(_context.AgronomistProfiles.Where(a => a.ServiceArea == area && a.IsVerified).ToList());
        }
    }
}