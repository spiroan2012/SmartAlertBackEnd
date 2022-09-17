using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Incident
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public IncidentCategory? Category { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime StatusChangeDateTime{ get; set; }
        //[Required]
        //[Range(-90.0, 90.0)]
        //public double Latitude { get; set; }
        //[Required]
        //[Range(-90.0, 90.0)]
        //public double Longitude { get; set; }
        //[Column(TypeName = "geometry (point)")]
        public Point? Coords { get; set; }
        public ICollection<IncidentDetail>? Details { get; set; }
    }
}
