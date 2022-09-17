using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class IncidentDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public IncidentCategory? Category { get; set; }
        [Required]
        public DateTime StatusChangeDateTime { get; set; }
        [Required]
        public string? UserId { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email not valid")]
        public string? UserEmail { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        //[Required]
        //[Range(-90.0, 90.0)]
        //public double Latitude { get; set; }
        //[Required]
        //[Range(-90.0, 90.0)]
        //public double Longitude { get; set; }
       // [Column(TypeName = "geometry (point)")]
        public Point? Coords { get; set; }
        public Incident? MasterIncident { get; set; }
    }
}
