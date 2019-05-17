using Microsoft.EntityFrameworkCore;

namespace RobotMissile.Models
{
    public class GameStateContext : DbContext
    {
        public GameStateContext(DbContextOptions<GameStateContext> options) : base(options)
        {
        }

        public DbSet<GameState> GameStates { get; set; }
    }
}
