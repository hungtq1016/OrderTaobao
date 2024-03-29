﻿namespace OAuth2Service.Infrastructure.Data
{
    public class OAuth2Context : AppDbContext
    {
        public OAuth2Context(DbContextOptions options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<OTP> Otps => Set<OTP>();
        public DbSet<Token> Tokens => Set<Token>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new OTPConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
        }
    }
}
