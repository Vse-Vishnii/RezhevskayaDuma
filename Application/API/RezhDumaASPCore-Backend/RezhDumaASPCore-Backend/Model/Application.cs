using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    public class Application : Message
    {
        public List<DistrictApplication> Districts { get; set; }
        public List<CategoryApplication> Categories { get; set; }

        [ForeignKey("Deputy")]
        public string DeputyId { get; set; }
        [ForeignKey("Applicant")]
        public string ApplicantId { get; set; }

        
        public User Deputy { get; set; }
        [NotMapped]
        public User Applicant { get; set; }

        [Required]
        public Status Status { get; set; }        
    }
}
