﻿

using BaseSource.Configurations;
using BaseSource.Model;
using BaseSource.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseScource.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=OrderTaoBao;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<User>().ToTable("USER");
            modelBuilder.Entity<Role>().ToTable("ROLE");
            modelBuilder.Entity<UserRole>();
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("USER_CLAIM");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("ROLE_CLAIM");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("USER_TOKEN");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("USER_LOGIN");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid().ToString(), Name = "Customer", NormalizedName = "CUSTOMER" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Staff", NormalizedName = "STAFF" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Collaborator", NormalizedName = "COLLABORATOR" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Super Admin", NormalizedName = "SUPER ADMIN" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "Visitor", NormalizedName = "VISITOR" }
            );

        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Address> Address => Set<Address>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<Ward> Wards => Set<Ward>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<ResetPassword> ResetPassword => Set<ResetPassword>();
        public DbSet<AuthHistory> AuthHistory => Set<AuthHistory>();
        public DbSet<UserHistory> UserHistory => Set<UserHistory>();

    }
}
