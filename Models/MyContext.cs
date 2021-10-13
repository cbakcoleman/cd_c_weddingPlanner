using Microsoft.EntityFrameworkCore;

namespace cd_c_weddingPlanner.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Guest> Guests {get;set;}
        public DbSet<Wedding> Weddings {get;set;}
    }
}