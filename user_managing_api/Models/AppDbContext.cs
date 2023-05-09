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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<User_Group>().Property(b => b.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<User_State>().Property(b => b.Id).UseIdentityAlwaysColumn();
        }
    }
}
