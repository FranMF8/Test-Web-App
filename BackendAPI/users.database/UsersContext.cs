using Microsoft.EntityFrameworkCore;

namespace users.database
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UsersData { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserData>().ToTable("UserData");
        }*/

    }
}