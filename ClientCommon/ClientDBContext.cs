using Microsoft.EntityFrameworkCore;

namespace ClientCommon
{
    public class ClientDBContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<TaskInstance> TaskInstances { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<UIType> UITypes { get; set; }

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