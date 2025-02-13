﻿using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashflowdb;Uid=root;Pwd=1234;";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 41));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}