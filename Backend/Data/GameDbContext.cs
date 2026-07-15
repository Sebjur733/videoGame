using Microsoft.EntityFrameworkCore;
using Backend.Models;


namespace Backend.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }


    }
}