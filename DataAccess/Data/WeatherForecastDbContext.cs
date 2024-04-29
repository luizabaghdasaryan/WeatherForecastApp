using DataAccess.Configuration;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
          : base(options)
        {
        }
        public DbSet<DailyForecast> DailyForecasts => Set<DailyForecast>();
        public DbSet<HourlyForecast> HourlyForecasts => Set<HourlyForecast>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<HourlyForecast> Summaries => Set<HourlyForecast>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyForecast>()
                .Property(df => df.Date)
                .HasColumnType("date");

            modelBuilder.Entity<DailyForecast>()
                .HasIndex(df => new { df.Date, df.RegionId})
                .IsUnique();

            modelBuilder.Entity<HourlyForecast>()
                .HasIndex(hf => new { hf.ForecastId, hf.Hour })
                .IsUnique();

            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new SummaryConfiguration());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=WeatherForecast;Trusted_Connection=True;");
        //}
    }
}