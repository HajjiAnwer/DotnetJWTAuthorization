using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(c => c.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.RoleId);
        }
        public DbSet<Models.UserModel> Users { get; set; }
        public DbSet<Models.Command> Commands { get; set; }
        public DbSet<Models.RoleModel> RoleModel { get; set; }
    }
}