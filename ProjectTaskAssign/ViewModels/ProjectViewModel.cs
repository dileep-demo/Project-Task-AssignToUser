using System.ComponentModel.DataAnnotations;

namespace ProjectTaskAssign.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public int TotalTasks { get; set; }
    }
}
