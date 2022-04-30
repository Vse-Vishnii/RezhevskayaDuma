using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    public class Answer : Message
    {
        [ForeignKey("Application")]
        [Required]
        public string ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
