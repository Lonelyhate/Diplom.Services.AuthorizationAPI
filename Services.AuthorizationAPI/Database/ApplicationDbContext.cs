using Microsoft.EntityFrameworkCore;
using Services.AuthorizationAPI.Models;

namespace Services.AuthorizationAPI.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
}