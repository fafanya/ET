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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.
                Entity<TaskInstance>().
                HasOne(ti => ti.Test).
                WithMany(t => t.TaskInstances).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.
                Entity<TaskItem>().
                HasOne(ti => ti.TaskInstance).
                WithMany(t => t.TaskItems).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.
                Entity<TaskItem>().
                HasOne(ti => ti.Parent).
                WithMany(t => t.Children).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}