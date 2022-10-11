using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class IncidentCategory
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Type { get; set; }
        public string? SmsText { get; set; }
    }
}
