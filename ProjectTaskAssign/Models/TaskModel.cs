using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTaskAssign.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string status { get; set; }

        public DateTime CompletionDate { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectModel ProjectModel { get; set; }
    }
}
