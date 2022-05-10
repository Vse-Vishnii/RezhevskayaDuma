using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    [Table("CategoryApplications")]
    public class CategoryApplication : ApplicationConnection<Category>
    {
    }
}
