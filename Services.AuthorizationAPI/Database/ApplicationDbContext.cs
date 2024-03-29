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
    
    public DbSet<AddressesUser> AddressesUsers { get; set; }
    
    public DbSet<Discount> Discounts { get; set; }
}