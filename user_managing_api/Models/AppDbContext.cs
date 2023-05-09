using Microsoft.EntityFrameworkCore;
using user_managing_api.Models;

namespace user_managing_api.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<User_Group> UserGroups { get; set; } = null!;
        public DbSet<User_State> UserStates { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
