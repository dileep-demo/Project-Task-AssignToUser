using System.ComponentModel.DataAnnotations;

namespace ProjectTaskAssign.Models
{
    public class AssigneeModel
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
