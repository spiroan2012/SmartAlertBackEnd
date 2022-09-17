using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SmartAlertContext : DbContext
    {
        public SmartAlertContext(DbContextOptions<SmartAlertContext> options) : base(options)  { }

        public DbSet<Incident>? Incidents { get; set; }
        public DbSet<IncidentDetail>? IncidentDetails { get; set; }
        public DbSet<IncidentCategory>? Categories { get; set; }
        public DbSet<SmsMaster>? SmsMasters { get; set; }
        public DbSet<SmsDetail>? SmsDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<Incident>()
                .HasMany(i => i.Details)
                .WithOne(i => i.MasterIncident);

            modelBuilder.Entity<SmsMaster>()
                .HasMany(s => s.SmsDetails)
                .WithOne(s => s.SmsMaster);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
