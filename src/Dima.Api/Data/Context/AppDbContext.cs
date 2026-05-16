using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options) 
{
    public DbSet<Core.Models.Category>Categories { get; set; }= null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }
}