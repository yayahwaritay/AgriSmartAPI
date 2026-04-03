using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriSmartAPI.Models;

public class Crop
{
    public int Id { get; set; }
    [StringLength(100)]
    public string? Name { get; set; }
    [StringLength(200)]
    public string? Location { get; set; }
    public DateTime PlantingDate { get; set; }
    public string? CareSchedule { get; set; }
    public string? HarvestSchedule { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}