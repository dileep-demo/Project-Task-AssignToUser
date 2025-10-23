using ProjectTaskAssign.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectTaskAssign.ViewModels
{
    public class AssigneeViewModel
    {
       
        public int AssigneeId { get; set; }
        [Required(ErrorMessage = "User Name is required.")]

        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        //public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
