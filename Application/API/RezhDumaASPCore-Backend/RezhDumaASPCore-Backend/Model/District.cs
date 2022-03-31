using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Model
{
    public class District : DbEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<DistrictApplication> Applications { get; set; }
    }
}
