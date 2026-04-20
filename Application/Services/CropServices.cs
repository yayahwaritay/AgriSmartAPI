using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;
using AgriSmartSierra.Application.Interfaces;
using AgriSmartSierra.Domain.Entities;
using AgriSmartSierra.Domain.Interfaces;
using System.Linq.Expressions;

namespace AgriSmartSierra.Application.Services;

public class CropService : ICropService
{
    private readonly ICropRepository _cropRepository;
    private readonly IFarmRepository _farmRepository;

    public CropService(ICropRepository cropRepository, IFarmRepository farmRepository)
    {
        _cropRepository = cropRepository;
        _farmRepository = farmRepository;
    }

    public async Task<ApiResponse<CropDto>> GetByIdAsync(Guid id)
    {
        var crop = await _cropRepository.GetByIdAsync(id);
        if (crop == null)
            return new ApiResponse<CropDto> { Success = false, Message = "Crop not found" };

        return new ApiResponse<CropDto> { Success = true, Data = MapToDto(crop) };
    }

    public async Task<ApiResponse<IEnumerable<CropDto>>> GetByFarmIdAsync(Guid farmId)
    {
        var crops = await _cropRepository.GetByFarmIdAsync(farmId);
        return new ApiResponse<IEnumerable<CropDto>> { Success = true, Data = crops.Select(MapToDto) };
    }

    public async Task<ApiResponse<CropDto>> CreateAsync(CreateCropDto dto)
    {
        var crop = new Crop
        {
            Id = Guid.NewGuid(),
            FarmId = dto.FarmId,
            Name = dto.Name,
            Variety = dto.Variety,
            Category = Enum.Parse<CropCategory>(dto.Category, true),
            AreaPlanted = dto.AreaPlanted,
            EstimatedYield = dto.EstimatedYield,
            Unit = dto.Unit,
            PlantingDate = dto.PlantingDate,
            ExpectedHarvestDate = dto.ExpectedHarvestDate,
            Status = CropStatus.Planted,
            CreatedAt = DateTime.UtcNow
        };

        await _cropRepository.AddAsync(crop);
        return new ApiResponse<CropDto> { Success = true, Message = "Crop created", Data = MapToDto(crop) };
    }

    public async Task<ApiResponse<CropDto>> UpdateAsync(Guid id, UpdateCropDto dto)
    {
        var crop = await _cropRepository.GetByIdAsync(id);
        if (crop == null)
            return new ApiResponse<CropDto> { Success = false, Message = "Crop not found" };

        if (!string.IsNullOrEmpty(dto.Name)) crop.Name = dto.Name;
        if (!string.IsNullOrEmpty(dto.Variety)) crop.Variety = dto.Variety;
        if (!string.IsNullOrEmpty(dto.Category)) crop.Category = Enum.Parse<CropCategory>(dto.Category, true);
        if (dto.AreaPlanted.HasValue) crop.AreaPlanted = dto.AreaPlanted.Value;
        if (dto.EstimatedYield.HasValue) crop.EstimatedYield = dto.EstimatedYield.Value;
        if (!string.IsNullOrEmpty(dto.Unit)) crop.Unit = dto.Unit;
        if (!string.IsNullOrEmpty(dto.Status)) crop.Status = Enum.Parse<CropStatus>(dto.Status, true);
        if (dto.ExpectedHarvestDate.HasValue) crop.ExpectedHarvestDate = dto.ExpectedHarvestDate.Value;
        crop.UpdatedAt = DateTime.UtcNow;

        await _cropRepository.UpdateAsync(crop);
        return new ApiResponse<CropDto> { Success = true, Message = "Crop updated", Data = MapToDto(crop) };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var crop = await _cropRepository.GetByIdAsync(id);
        if (crop == null)
            return new ApiResponse<bool> { Success = false, Message = "Crop not found" };

        await _cropRepository.DeleteAsync(crop);
        return new ApiResponse<bool> { Success = true, Message = "Crop deleted" };
    }

    public async Task<ApiResponse<CropCalendarDto>> GetCropCalendarAsync(Guid cropId)
    {
        var crop = await _cropRepository.GetWithActivitiesAsync(cropId);
        if (crop == null)
            return new ApiResponse<CropCalendarDto> { Success = false, Message = "Crop not found" };

        var now = DateTime.UtcNow;
        var upcoming = crop.Activities.Where(a => a.ScheduledDate > now && a.Status != ActivityStatus.Completed)
            .OrderBy(a => a.ScheduledDate).Take(10).Select(MapActivityToDto);
        var overdue = crop.Activities.Where(a => a.ScheduledDate < now && a.Status != ActivityStatus.Completed)
            .Select(MapActivityToDto);

        return new ApiResponse<CropCalendarDto>
        {
            Success = true,
            Data = new CropCalendarDto
            {
                CropId = crop.Id,
                CropName = crop.Name,
                PlantingDate = crop.PlantingDate,
                ExpectedHarvestDate = crop.ExpectedHarvestDate,
                UpcomingActivities = upcoming.ToList(),
                OverdueActivities = overdue.ToList()
            }
        };
    }

    private static CropDto MapToDto(Crop crop) => new CropDto
    {
        Id = crop.Id,
        FarmId = crop.FarmId,
        Name = crop.Name,
        Variety = crop.Variety,
        Category = crop.Category.ToString(),
        AreaPlanted = crop.AreaPlanted,
        EstimatedYield = crop.EstimatedYield,
        Unit = crop.Unit,
        PlantingDate = crop.PlantingDate,
        ExpectedHarvestDate = crop.ExpectedHarvestDate,
        Status = crop.Status.ToString(),
        CreatedAt = crop.CreatedAt
    };

    private static CropActivityDto MapActivityToDto(CropActivity a) => new CropActivityDto
    {
        Id = a.Id,
        CropId = a.CropId,
        AssignedTo = a.AssignedTo,
        Type = a.Type.ToString(),
        Description = a.Description,
        ScheduledDate = a.ScheduledDate,
        CompletedDate = a.CompletedDate,
        Status = a.Status.ToString(),
        Cost = a.Cost,
        Notes = a.Notes,
        CreatedAt = a.CreatedAt
    };
}

public class FarmService : IFarmService
{
    private readonly IFarmRepository _farmRepository;

    public FarmService(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public async Task<ApiResponse<FarmDto>> GetByIdAsync(Guid id)
    {
        var farm = await _farmRepository.GetByIdAsync(id);
        if (farm == null)
            return new ApiResponse<FarmDto> { Success = false, Message = "Farm not found" };

        return new ApiResponse<FarmDto> { Success = true, Data = MapToDto(farm) };
    }

    public async Task<ApiResponse<IEnumerable<FarmDto>>> GetByFarmerProfileIdAsync(Guid farmerProfileId)
    {
        var farms = await _farmRepository.GetByFarmerProfileIdAsync(farmerProfileId);
        return new ApiResponse<IEnumerable<FarmDto>> { Success = true, Data = farms.Select(MapToDto) };
    }

    public async Task<ApiResponse<FarmDto>> CreateAsync(Guid farmerProfileId, CreateFarmDto dto)
    {
        var farm = new Farm
        {
            Id = Guid.NewGuid(),
            FarmerProfileId = farmerProfileId,
            Name = dto.Name,
            SizeHectares = dto.SizeHectares,
            Location = dto.Location,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            SoilType = dto.SoilType,
            Topography = dto.Topography,
            CreatedAt = DateTime.UtcNow
        };

        await _farmRepository.AddAsync(farm);
        return new ApiResponse<FarmDto> { Success = true, Message = "Farm created", Data = MapToDto(farm) };
    }

    public async Task<ApiResponse<FarmDto>> UpdateAsync(Guid id, CreateFarmDto dto)
    {
        var farm = await _farmRepository.GetByIdAsync(id);
        if (farm == null)
            return new ApiResponse<FarmDto> { Success = false, Message = "Farm not found" };

        farm.Name = dto.Name;
        farm.SizeHectares = dto.SizeHectares;
        farm.Location = dto.Location;
        farm.Latitude = dto.Latitude;
        farm.Longitude = dto.Longitude;
        farm.SoilType = dto.SoilType;
        farm.Topography = dto.Topography;
        farm.UpdatedAt = DateTime.UtcNow;

        await _farmRepository.UpdateAsync(farm);
        return new ApiResponse<FarmDto> { Success = true, Message = "Farm updated", Data = MapToDto(farm) };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var farm = await _farmRepository.GetByIdAsync(id);
        if (farm == null)
            return new ApiResponse<bool> { Success = false, Message = "Farm not found" };

        await _farmRepository.DeleteAsync(farm);
        return new ApiResponse<bool> { Success = true, Message = "Farm deleted" };
    }

    private static FarmDto MapToDto(Farm farm) => new FarmDto
    {
        Id = farm.Id,
        FarmerProfileId = farm.FarmerProfileId,
        Name = farm.Name,
        SizeHectares = farm.SizeHectares,
        Location = farm.Location,
        Latitude = farm.Latitude,
        Longitude = farm.Longitude,
        SoilType = farm.SoilType,
        Topography = farm.Topography,
        CreatedAt = farm.CreatedAt
    };
}

public class CropActivityService : ICropActivityService
{
    private readonly ICropActivityRepository _activityRepository;

    public CropActivityService(ICropActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<ApiResponse<CropActivityDto>> GetByIdAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
            return new ApiResponse<CropActivityDto> { Success = false, Message = "Activity not found" };

        return new ApiResponse<CropActivityDto> { Success = true, Data = MapToDto(activity) };
    }

    public async Task<ApiResponse<IEnumerable<CropActivityDto>>> GetByCropIdAsync(Guid cropId)
    {
        var activities = await _activityRepository.GetByCropIdAsync(cropId);
        return new ApiResponse<IEnumerable<CropActivityDto>> { Success = true, Data = activities.Select(MapToDto) };
    }

    public async Task<ApiResponse<IEnumerable<CropActivityDto>>> GetUpcomingAsync(int days)
    {
        var activities = await _activityRepository.GetUpcomingAsync(days);
        return new ApiResponse<IEnumerable<CropActivityDto>> { Success = true, Data = activities.Select(MapToDto) };
    }

    public async Task<ApiResponse<CropActivityDto>> CreateAsync(CreateCropActivityDto dto)
    {
        var activity = new CropActivity
        {
            Id = Guid.NewGuid(),
            CropId = dto.CropId,
            AssignedTo = dto.AssignedTo,
            Type = Enum.Parse<ActivityType>(dto.Type, true),
            Description = dto.Description,
            ScheduledDate = dto.ScheduledDate,
            Cost = dto.Cost,
            Notes = dto.Notes,
            Status = ActivityStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };

        await _activityRepository.AddAsync(activity);
        return new ApiResponse<CropActivityDto> { Success = true, Message = "Activity created", Data = MapToDto(activity) };
    }

    public async Task<ApiResponse<CropActivityDto>> UpdateAsync(Guid id, CreateCropActivityDto dto)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
            return new ApiResponse<CropActivityDto> { Success = false, Message = "Activity not found" };

        activity.Type = Enum.Parse<ActivityType>(dto.Type, true);
        activity.Description = dto.Description;
        activity.ScheduledDate = dto.ScheduledDate;
        activity.Cost = dto.Cost;
        activity.Notes = dto.Notes;
        activity.UpdatedAt = DateTime.UtcNow;

        await _activityRepository.UpdateAsync(activity);
        return new ApiResponse<CropActivityDto> { Success = true, Message = "Activity updated", Data = MapToDto(activity) };
    }

    public async Task<ApiResponse<CropActivityDto>> CompleteAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
            return new ApiResponse<CropActivityDto> { Success = false, Message = "Activity not found" };

        activity.Status = ActivityStatus.Completed;
        activity.CompletedDate = DateTime.UtcNow;
        activity.UpdatedAt = DateTime.UtcNow;

        await _activityRepository.UpdateAsync(activity);
        return new ApiResponse<CropActivityDto> { Success = true, Message = "Activity completed", Data = MapToDto(activity) };
    }

    private static CropActivityDto MapToDto(CropActivity a) => new CropActivityDto
    {
        Id = a.Id,
        CropId = a.CropId,
        AssignedTo = a.AssignedTo,
        Type = a.Type.ToString(),
        Description = a.Description,
        ScheduledDate = a.ScheduledDate,
        CompletedDate = a.CompletedDate,
        Status = a.Status.ToString(),
        Cost = a.Cost,
        Notes = a.Notes,
        CreatedAt = a.CreatedAt
    };
}