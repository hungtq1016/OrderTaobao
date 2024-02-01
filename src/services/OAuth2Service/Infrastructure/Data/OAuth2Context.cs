using Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using OAuth2Service.Configurations;
using OAuth2Service.Models;

namespace OAuth2Service.Infrastructure.Data
{
    public class OAuth2Context : AppDbContext
    {
        public OAuth2Context(DbContextOptions options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Assignment> Assignments => Set<Assignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
        }
    }
}
