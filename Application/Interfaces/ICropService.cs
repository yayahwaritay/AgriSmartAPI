using AgriSmartSierra.Application.DTOs;
using AgriSmartSierra.Application.DTOs.Common;

namespace AgriSmartSierra.Application.Interfaces;

public interface ICropService
{
    Task<ApiResponse<CropDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<CropDto>>> GetByFarmIdAsync(Guid farmId);
    Task<ApiResponse<CropDto>> CreateAsync(CreateCropDto dto);
    Task<ApiResponse<CropDto>> UpdateAsync(Guid id, UpdateCropDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
    Task<ApiResponse<CropCalendarDto>> GetCropCalendarAsync(Guid cropId);
}

public interface IFarmService
{
    Task<ApiResponse<FarmDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<FarmDto>>> GetByFarmerProfileIdAsync(Guid farmerProfileId);
    Task<ApiResponse<FarmDto>> CreateAsync(Guid farmerProfileId, CreateFarmDto dto);
    Task<ApiResponse<FarmDto>> UpdateAsync(Guid id, CreateFarmDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}

public interface ICropActivityService
{
    Task<ApiResponse<CropActivityDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<CropActivityDto>>> GetByCropIdAsync(Guid cropId);
    Task<ApiResponse<IEnumerable<CropActivityDto>>> GetUpcomingAsync(int days);
    Task<ApiResponse<CropActivityDto>> CreateAsync(CreateCropActivityDto dto);
    Task<ApiResponse<CropActivityDto>> UpdateAsync(Guid id, CreateCropActivityDto dto);
    Task<ApiResponse<CropActivityDto>> CompleteAsync(Guid id);
}