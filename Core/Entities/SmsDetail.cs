using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
