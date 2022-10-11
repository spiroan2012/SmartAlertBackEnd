using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class UpdateIncidentDto
    {
        [Required]
        public int IncidentId { get; set; }
        [Range(1,2)]
        [Required]
        public int Status { get; set; }
        [Required]
        public string Uid { get; set; }
    }
}
