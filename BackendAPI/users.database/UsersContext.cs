using Microsoft.EntityFrameworkCore;

namespace users.database
{
    public class UsersContext : DbContext
    {

        public UsersContext() { }
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
        public DbSet<DBUser> Users { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<Student> Students { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserData>().ToTable("UserData");
        }*/

    }
}