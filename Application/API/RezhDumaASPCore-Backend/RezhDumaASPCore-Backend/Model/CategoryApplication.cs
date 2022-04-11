using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    [Table("CategoryApplications")]
    public class CategoryApplication : DbEntity
    {
        [ForeignKey("Application")]
        [Required]
        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("Category")]
        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public CategoryApplication(){}
    }
}
