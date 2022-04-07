using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Model
{
    public class District : DbEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<DistrictApplication> DistrictApplications { get; set; }
        public List<Application> Applications { get; set; }

        [ForeignKey("Deputy")]
        [Required]
        public string DeputyId { get; set; }
        public User Deputy { get; set; }
    }
}
