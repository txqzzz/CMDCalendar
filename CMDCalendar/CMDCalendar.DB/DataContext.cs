using Microsoft.EntityFrameworkCore;

namespace CMDCalendar.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cmddb.db");
        }
    }
}