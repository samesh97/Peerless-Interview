using Microsoft.EntityFrameworkCore;
using PeerlessInterview.src.Domain.Entities;

namespace PeerlessInterview.src.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}