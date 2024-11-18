using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()=>
            Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentDBCore;Trusted_Connection=True;MultipleActiveResultSets=true");
       
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
