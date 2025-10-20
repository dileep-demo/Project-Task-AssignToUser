using System;
using System.ComponentModel.DataAnnotations;


namespace ProjectTaskAssign.ViewModels
{

   
        public class TaskViewModel
        {
            public int TaskId { get; set; }

            [Required(ErrorMessage = "Project is required.")]
            public int ProjectId { get; set; }

            [Required(ErrorMessage = "Task name is required.")]
            [StringLength(100)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            [StringLength(50)]
            public string Status { get; set; }

            [Display(Name = "Completion Date")]
            [DataType(DataType.Date)]
            public DateTime CompletionDate { get; set; }

            // Optional: Project Name (for display in views, if needed)
            public string ProjectName { get; set; }
        }
    

}
