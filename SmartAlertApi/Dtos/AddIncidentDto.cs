using System.ComponentModel.DataAnnotations;

namespace SmartAlertApi.Dtos
{
    public class AddIncidentDto
    {
        //[Required]
        //public long CreatedOn { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? HumanAddress { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        [Range(-90.0, 90.0)]
        public double Latitude { get; set; }
        [Required]
        [Range(-90.0, 90.0)]
        public double Longitude { get; set; }
        public decimal LocCreatedOn { get; set; }
        [Required]
        public string? Uid { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email not valid")]
        public string? Email { get; set; }
        [Required]
        public string? Image { get; set; }
    }
}
