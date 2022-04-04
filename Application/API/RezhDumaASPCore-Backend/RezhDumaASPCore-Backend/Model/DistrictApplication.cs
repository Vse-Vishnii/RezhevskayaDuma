using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    public class DistrictApplication : DbEntity
    {
        [ForeignKey("Application")]
        [Required]
        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("District")]
        [Required]
        public string DistrictId { get; set; }
        public District District { get; set; }

        public DistrictApplication(Application application, District district)
        {
            Application = application;
            District = district;
        }
    }
}
