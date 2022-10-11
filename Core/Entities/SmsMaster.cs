using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class SmsMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Incident? Incident { get; set; }
        [Required]
        public string? SmsText { get; set; }
        public ICollection<SmsDetail>? SmsDetails { get; set; }
    }
}
