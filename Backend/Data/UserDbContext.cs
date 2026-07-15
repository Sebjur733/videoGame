using Microsoft.EntityFrameworkCore;
using Backend.Models;


namespace Backend.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            entity.HasIndex(e => e.Username).IsUnique());
        }
    }
}