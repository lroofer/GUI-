using Microsoft.EntityFrameworkCore;

namespace GUI.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        string name = "usersdb";
        public static int cnt = 0;
        public ApplicationContext(string name)
        {
            Database.EnsureCreated();
            cnt = 1;
            foreach(var u in Users)
            {
                cnt++;
            }
            this.name = name;
        }

        public void Clear()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host=localhost;Port=5432;Database={name};Username=postgres;Password=admin");
        }
    }
}
