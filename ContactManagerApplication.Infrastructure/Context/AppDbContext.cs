using ContactManagerApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApplication.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}