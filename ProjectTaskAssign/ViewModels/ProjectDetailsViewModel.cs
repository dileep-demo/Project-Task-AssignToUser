using System;
using System.Collections.Generic;

namespace ProjectTaskAssign.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
    }
}