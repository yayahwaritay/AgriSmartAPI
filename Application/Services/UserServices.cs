using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;

namespace AgriSmartSierra.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<UserDto>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Data = MapToDto(user)
        };
    }

    public async Task<ApiResponse<UserDto>> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };
        }

        if (!string.IsNullOrEmpty(dto.FirstName))
            user.FirstName = dto.FirstName;
        if (!string.IsNullOrEmpty(dto.LastName))
            user.LastName = dto.LastName;
        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            user.PhoneNumber = dto.PhoneNumber;
        if (!string.IsNullOrEmpty(dto.ProfileImageUrl))
            user.ProfileImageUrl = dto.ProfileImageUrl;

        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User updated successfully",
            Data = MapToDto(user)
        };
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString(),
            ProfileImageUrl = user.ProfileImageUrl,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}

public class FarmerProfileService : IFarmerProfileService
{
    private readonly IFarmerProfileRepository _farmerProfileRepository;
    private readonly IUserRepository _userRepository;

    public FarmerProfileService(
        IFarmerProfileRepository farmerProfileRepository,
        IUserRepository userRepository)
    {
        _farmerProfileRepository = farmerProfileRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<FarmerProfileDto>> GetByUserIdAsync(Guid userId)
    {
        var profile = await _farmerProfileRepository.GetByUserIdAsync(userId);
        
        if (profile == null)
        {
            return new ApiResponse<FarmerProfileDto>
            {
                Success = false,
                Message = "Farmer profile not found"
            };
        }

        return new ApiResponse<FarmerProfileDto>
        {
            Success = true,
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<FarmerProfileDto>> CreateAsync(Guid userId, CreateFarmerProfileDto dto)
    {
        var profile = new FarmerProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FarmName = dto.FarmName,
            FarmSizeHectares = dto.FarmSizeHectares,
            Location = dto.Location,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            District = dto.District,
            SoilType = dto.SoilType,
            WaterSource = dto.WaterSource,
            IrrigationType = dto.IrrigationType,
            CreatedAt = DateTime.UtcNow
        };

        await _farmerProfileRepository.AddAsync(profile);

        return new ApiResponse<FarmerProfileDto>
        {
            Success = true,
            Message = "Farmer profile created successfully",
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<FarmerProfileDto>> UpdateAsync(Guid id, CreateFarmerProfileDto dto)
    {
        var profile = await _farmerProfileRepository.GetByIdAsync(id);
        
        if (profile == null)
        {
            return new ApiResponse<FarmerProfileDto>
            {
                Success = false,
                Message = "Farmer profile not found"
            };
        }

        profile.FarmName = dto.FarmName;
        profile.FarmSizeHectares = dto.FarmSizeHectares;
        profile.Location = dto.Location;
        profile.Latitude = dto.Latitude;
        profile.Longitude = dto.Longitude;
        profile.District = dto.District;
        profile.SoilType = dto.SoilType;
        profile.WaterSource = dto.WaterSource;
        profile.IrrigationType = dto.IrrigationType;
        profile.UpdatedAt = DateTime.UtcNow;

        await _farmerProfileRepository.UpdateAsync(profile);

        return new ApiResponse<FarmerProfileDto>
        {
            Success = true,
            Message = "Farmer profile updated successfully",
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<IEnumerable<FarmerProfileDto>>> GetByDistrictAsync(string district)
    {
        var profiles = await _farmerProfileRepository.GetByDistrictAsync(district);
        
        return new ApiResponse<IEnumerable<FarmerProfileDto>>
        {
            Success = true,
            Data = profiles.Select(MapToDto)
        };
    }

    private static FarmerProfileDto MapToDto(FarmerProfile profile)
    {
        return new FarmerProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            FarmName = profile.FarmName,
            FarmSizeHectares = profile.FarmSizeHectares,
            Location = profile.Location,
            Latitude = profile.Latitude,
            Longitude = profile.Longitude,
            District = profile.District,
            SoilType = profile.SoilType,
            WaterSource = profile.WaterSource,
            IrrigationType = profile.IrrigationType,
            CreatedAt = profile.CreatedAt
        };
    }
}

public class BuyerProfileService : IBuyerProfileService
{
    private readonly IBuyerProfileRepository _buyerProfileRepository;

    public BuyerProfileService(IBuyerProfileRepository buyerProfileRepository)
    {
        _buyerProfileRepository = buyerProfileRepository;
    }

    public async Task<ApiResponse<BuyerProfileDto>> GetByUserIdAsync(Guid userId)
    {
        var profile = await _buyerProfileRepository.GetByUserIdAsync(userId);
        
        if (profile == null)
        {
            return new ApiResponse<BuyerProfileDto>
            {
                Success = false,
                Message = "Buyer profile not found"
            };
        }

        return new ApiResponse<BuyerProfileDto>
        {
            Success = true,
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<BuyerProfileDto>> CreateAsync(Guid userId, CreateBuyerProfileDto dto)
    {
        var profile = new BuyerProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CompanyName = dto.CompanyName,
            BusinessType = dto.BusinessType,
            Address = dto.Address,
            TaxId = dto.TaxId,
            CreatedAt = DateTime.UtcNow
        };

        await _buyerProfileRepository.AddAsync(profile);

        return new ApiResponse<BuyerProfileDto>
        {
            Success = true,
            Message = "Buyer profile created successfully",
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<BuyerProfileDto>> UpdateAsync(Guid id, CreateBuyerProfileDto dto)
    {
        var profile = await _buyerProfileRepository.GetByIdAsync(id);
        
        if (profile == null)
        {
            return new ApiResponse<BuyerProfileDto>
            {
                Success = false,
                Message = "Buyer profile not found"
            };
        }

        profile.CompanyName = dto.CompanyName;
        profile.BusinessType = dto.BusinessType;
        profile.Address = dto.Address;
        profile.TaxId = dto.TaxId;
        profile.UpdatedAt = DateTime.UtcNow;

        await _buyerProfileRepository.UpdateAsync(profile);

        return new ApiResponse<BuyerProfileDto>
        {
            Success = true,
            Message = "Buyer profile updated successfully",
            Data = MapToDto(profile)
        };
    }

    private static BuyerProfileDto MapToDto(BuyerProfile profile)
    {
        return new BuyerProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            CompanyName = profile.CompanyName,
            BusinessType = profile.BusinessType,
            Address = profile.Address,
            TaxId = profile.TaxId,
            CreatedAt = profile.CreatedAt
        };
    }
}

public class AgronomistProfileService : IAgronomistProfileService
{
    private readonly IAgronomistProfileRepository _agronomistProfileRepository;

    public AgronomistProfileService(IAgronomistProfileRepository agronomistProfileRepository)
    {
        _agronomistProfileRepository = agronomistProfileRepository;
    }

    public async Task<ApiResponse<AgronomistProfileDto>> GetByUserIdAsync(Guid userId)
    {
        var profile = await _agronomistProfileRepository.GetByUserIdAsync(userId);
        
        if (profile == null)
        {
            return new ApiResponse<AgronomistProfileDto>
            {
                Success = false,
                Message = "Agronomist profile not found"
            };
        }

        return new ApiResponse<AgronomistProfileDto>
        {
            Success = true,
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<AgronomistProfileDto>> CreateAsync(Guid userId, CreateAgronomistProfileDto dto)
    {
        var profile = new AgronomistProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Qualification = dto.Qualification,
            Specialization = dto.Specialization,
            YearsOfExperience = dto.YearsOfExperience,
            ServiceArea = dto.ServiceArea,
            IsVerified = false,
            CreatedAt = DateTime.UtcNow
        };

        await _agronomistProfileRepository.AddAsync(profile);

        return new ApiResponse<AgronomistProfileDto>
        {
            Success = true,
            Message = "Agronomist profile created successfully",
            Data = MapToDto(profile)
        };
    }

    public async Task<ApiResponse<IEnumerable<AgronomistProfileDto>>> GetVerifiedAsync()
    {
        var profiles = await _agronomistProfileRepository.GetVerifiedAsync();
        
        return new ApiResponse<IEnumerable<AgronomistProfileDto>>
        {
            Success = true,
            Data = profiles.Select(MapToDto)
        };
    }

    public async Task<ApiResponse<IEnumerable<AgronomistProfileDto>>> GetByServiceAreaAsync(string area)
    {
        var profiles = await _agronomistProfileRepository.GetByServiceAreaAsync(area);
        
        return new ApiResponse<IEnumerable<AgronomistProfileDto>>
        {
            Success = true,
            Data = profiles.Select(MapToDto)
        };
    }

    private static AgronomistProfileDto MapToDto(AgronomistProfile profile)
    {
        return new AgronomistProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            Qualification = profile.Qualification,
            Specialization = profile.Specialization,
            YearsOfExperience = profile.YearsOfExperience,
            ServiceArea = profile.ServiceArea,
            IsVerified = profile.IsVerified,
            CreatedAt = profile.CreatedAt
        };
    }
}