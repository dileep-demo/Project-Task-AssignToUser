using System.ComponentModel.DataAnnotations;

namespace ProjectTaskAssign.Models
{
    public class ProjectModel
    {
        [Key]
        public int Id { get; set; }
       
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        //One project has collection of Tasks. 
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();

    }
}
