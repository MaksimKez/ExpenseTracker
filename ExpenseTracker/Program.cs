using ExpenseTracker.Data;
using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos;
using ExpenseTracker.Data.Repos.Interfaces;
using ExpenseTracker.Mappers;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("ExpenseTrackerDb"));
        });

        builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
        builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IMapper<User, UserEntity>, UserMapper>();
        builder.Services.AddScoped<IMapper<Expense, ExpenseEntity>, ExpenseMapper>();
        builder.Services.AddScoped<IMapper<Income, IncomeEntity>, IncomeMapper>();
        builder.Services.AddScoped<IMapper<BankAccount, BankAccountEntity>, BankAccountMapper>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IBankAccountService, BankAccountService>();
        builder.Services.AddScoped<IExpenseService, ExpenseService>();
        builder.Services.AddScoped<IIncomeService, IncomeService>();
        
        
        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}