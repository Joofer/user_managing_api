using Microsoft.EntityFrameworkCore;

namespace user_managing_api.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<User_Group>? UserGroups { get; set; }
        public DbSet<User_State>? UserStates { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
