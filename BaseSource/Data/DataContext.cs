

using BaseSource.Configurations;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BaseScource.Data
{
    public class DataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DataContext(){}

        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=OrderTaoBao;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());

            modelBuilder.Entity<IdentityUser>().ToTable("USER");
            modelBuilder.Entity<IdentityRole>().ToTable("ROLE");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("USER_ROLE");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("USER_CLAIM");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("ROLE_CLAIM");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("USER_TOKEN");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("USER_LOGIN");

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Staff", NormalizedName = "STAFF" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Collaborator", NormalizedName = "COLLABORATOR" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Super Admin", NormalizedName = "SUPER ADMIN" }
            );

        }
        public DbSet<Category> Categories => Set<Category>();
      /*  public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admin => Set<Admin>();
        public DbSet<UserRole> UserRole => Set<UserRole>();
    
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Address> Address => Set<Address>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<Ward> Wards => Set<Ward>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<AccessToken> AccessToken => Set<AccessToken>();
        public DbSet<EmailVerify> EmailVerify => Set<EmailVerify>();
        public DbSet<AuthHistory> AuthHistory => Set<AuthHistory>();
        public DbSet<CustomerHistory> CustomerHistory => Set<CustomerHistory>();*/

    }
}
