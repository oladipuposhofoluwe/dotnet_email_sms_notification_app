using NotificationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NotificationApp.Context
{
    public class AppDbContext: DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
    
        public DbSet<AppUser> User { get; set; }
    }
}

