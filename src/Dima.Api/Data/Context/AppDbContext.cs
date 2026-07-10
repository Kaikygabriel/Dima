using System.Reflection;
using Dima.Api.Models;
using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) :
    IdentityDbContext<
        User,
        IdentityRole<Guid>,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>
    >(options) 
{
    public DbSet<Category>Categories { get; set; }= null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<IncomesByCategory>IncomesByCategory { get; set; }
    public DbSet<ExpensesByCategory>ExpensesByCategory { get; set; }
    public DbSet<FinanceSummary>FinanceSummary { get; set; }
    public DbSet<IncomeAndExpenses>IncomesAndExpenses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}