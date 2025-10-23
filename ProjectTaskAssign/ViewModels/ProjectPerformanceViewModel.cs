namespace ProjectTaskAssign.ViewModels
{
    public class ProjectPerformanceViewModel
    {
        public string ProjectName { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int CompletionRate { get; set; }
    }
}
