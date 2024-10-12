using System.Reflection;
using ExpenseTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BankAccountEntity> BankAccounts { get; set; }
    public DbSet<IncomeEntity> Incomes { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }
    
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("ExpenseTrackerDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}