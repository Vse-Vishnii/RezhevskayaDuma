using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    public class User : DbEntity
    {
        [MaxLength(50)]
        public string Firstname { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Phone { get; set; }
        [Required]
        public Role Role { get; set; }

        [NotMapped]
        public List<Application> Applications { get; set; }
        [NotMapped]
        public List<Answer> Answers { get; set; }
    }
}
