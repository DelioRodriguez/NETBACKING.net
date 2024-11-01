﻿using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    public DbSet<CashAdvance> CashAdvances { get; set; }
    public DbSet<Transaction> Transactions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashAdvance>()
            .HasOne(ca => ca.DestinationAccount)
            .WithMany()
            .HasForeignKey(ca => ca.DestinationAccountId)
            .OnDelete(DeleteBehavior.NoAction);
    }

}