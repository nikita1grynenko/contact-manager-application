using ContactManagerApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApplication.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}