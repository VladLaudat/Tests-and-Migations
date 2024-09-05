using Microsoft.EntityFrameworkCore;

namespace Backend.DbContext
{
    public class BackendDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BackendDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            //Seeding
            modelBuilder.Entity<User>().HasData(
                new User() { Id = Guid.NewGuid(), UserName = "Test1", Password = "password1", IsAdmin=true, Email="test1@yahoo.com"},
                new User() { Id = Guid.NewGuid(), UserName = "Test2", Password = "password2", IsAdmin=false, Email = "test2@yahoo.com" },
                new User() { Id = Guid.NewGuid(), UserName = "Test3", Password = "password3", IsAdmin=false, Email = "test3@yahoo.com" }
                );
        }
    }
}
