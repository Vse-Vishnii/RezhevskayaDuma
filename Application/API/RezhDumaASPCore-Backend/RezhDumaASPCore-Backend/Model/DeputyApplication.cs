using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezhDumaASPCore_Backend.Model
{
    public class DeputyApplication: DbEntity
    {
        [ForeignKey("Application")]
        [Required]
        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("Deputy")]
        public string DeputyId { get; set; }

        public User Deputy { get; set; }

        public DeputyApplication() { }

        public DeputyApplication(Application application, User deputy)
        {
            Application = application;
            DeputyId = deputy.Id;
        }
    }
}
