using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezhDumaASPCore_Backend.Model
{
    public abstract class ApplicationConnection<TEntity> : DbEntity
    where TEntity : DbEntity
    {
        [ForeignKey("Application")]
        [Required]
        public string ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("ConnectedEntity")]
        [Required]
        public string ConnectedEntityId { get; set; }
        public TEntity ConnectedEntity { get; set; }
    }
}
