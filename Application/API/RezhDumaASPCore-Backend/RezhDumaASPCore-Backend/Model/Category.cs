﻿using System;
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
        [MaxLength(50)]
        public string Name { get; set; }
    }
}