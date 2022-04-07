using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    [Table("Categories")]
    public class Category : DbEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [ForeignKey("Deputy")]
        [Required]
        public string DeputyId { get; set; }
        public User Deputy { get; set; }

        public List<CategoryApplication> CategoryApplications { get; set; }
        public List<Application> Applications { get; set; }
    }
}
