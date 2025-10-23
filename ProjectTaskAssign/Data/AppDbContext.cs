using Microsoft.EntityFrameworkCore;
using ProjectTaskAssign.Models;

namespace ProjectTaskAssign.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option)  : base(option)
        {
            
        }

        public DbSet<ProjectModel> Project { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<AssigneeModel> Assignees { get; set; }
    }
}
