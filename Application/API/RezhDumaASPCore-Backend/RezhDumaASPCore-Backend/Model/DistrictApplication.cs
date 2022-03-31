using System;
using System.Collections.Generic;
using System.Text;

namespace RezhDumaASPCore_Backend.Model
{
    public class DistrictApplication : DbEntity
    {
        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        public string DistrictId { get; set; }
        public District District { get; set; }
    }
}
