using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriSmartAPI.DTO;

public class PestDiagnosis
{
    public int Id { get; set; }
    
    public int FarmerId { get; set; }
    [ForeignKey(nameof(FarmerId))]
    public Models.User Farmer { get; set; }
    
    [StringLength(500)]
    public string ImageUrl { get; set; }
    [StringLength(500)]
    public string Diagnosis { get; set; }
    [StringLength(1000)]
    public string TreatmentRecommendation { get; set; }
    public DateTime CreatedAt { get; set; }
}