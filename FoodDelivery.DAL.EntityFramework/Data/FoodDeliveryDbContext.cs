﻿using System.Configuration;
using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public class FoodDeliveryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public FoodDeliveryDbContext()
    {
    }

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var environment = ConfigurationManager.AppSettings["Environment"];
        var connectionString = environment switch
        {
            null => throw new Exception("Missing environment."),
            "Development" => ConfigurationManager.AppSettings["ConnectionStringDevelopment"],
            "Production" => ConfigurationManager.AppSettings["ConnectionStringProduction"],
            _ => throw new Exception($"Invalid environment {environment}")
        };
        if (connectionString is null)
        {
            throw new Exception("Missing connection string.");
        }

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
