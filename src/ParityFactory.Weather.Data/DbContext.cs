using System;
using Microsoft.EntityFrameworkCore;
using ParityFactory.Weather.Models;
using ParityFactory.Weather.Models.Data;

namespace ParityFactory.Weather.Data
{
    public class SqlLiteDbContext : DbContext
    {
        private static bool _created = false;

        public SqlLiteDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(Environment.GetEnvironmentVariable("DB_CONNECTION"));
        }
 
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Models.Data.Weather> WeatherRecords { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }
        
        public DbSet<Condition> ConditionStaging { get; set; }
        public DbSet<Location> LocationStaging { get; set; }
        public DbSet<Region> RegionStaging { get; set; }
        public DbSet<Models.Data.Weather> WeatherRecordStaging { get; set; }
        public DbSet<WeatherCondition> WeatherConditionStaging { get; set; }
    }
}