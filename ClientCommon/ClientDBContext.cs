using Microsoft.EntityFrameworkCore;

namespace ClientCommon
{
    public class ClientDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TaskInstance> TaskInstances { get; set; }
        public DbSet<TaskItemInstance> TaskItemInstances { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskItemGroup> TaskItemGroups { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }

        private static string DatabasePath { get; set; }

        public ClientDBContext()
        {

        }

        public ClientDBContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}