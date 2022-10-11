using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class SmsDetail
    {
        [Key]
        public int Id { get; set; }
        public SmsMaster? SmsMaster { get; set; }
        [Required]
        public string? MobilePhone { get; set; }
    }
}
