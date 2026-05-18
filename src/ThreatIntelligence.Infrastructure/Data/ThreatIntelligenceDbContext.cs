using Microsoft.EntityFrameworkCore;
using ThreatIntelligence.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ThreatIntelligence.Infrastructure.Data
{
    /// <summary>
    /// سياق قاعدة البيانات الرئيسي
    /// Main Database Context
    /// </summary>
    public class ThreatIntelligenceDbContext : DbContext
    {
        public ThreatIntelligenceDbContext(DbContextOptions<ThreatIntelligenceDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MonitoredAsset> MonitoredAssets { get; set; }
        public DbSet<ThreatAlert> ThreatAlerts { get; set; }
        public DbSet<AlertAction> AlertActions { get; set; }
        public DbSet<AlertNotification> AlertNotifications { get; set; }
        public DbSet<ScanHistory> ScanHistories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Organization Configuration
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Website).HasMaxLength(500);
                entity.Property(e => e.Status).HasConversion<int>();
                entity.Property(e => e.SubscriptionPlan).HasConversion<int>();
                entity.HasMany(e => e.Users).WithOne(u => u.Organization).HasForeignKey(u => u.OrganizationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.MonitoredAssets).WithOne(a => a.Organization).HasForeignKey(a => a.OrganizationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ThreatAlerts).WithOne(a => a.Organization).HasForeignKey(a => a.OrganizationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ScanHistories).WithOne(s => s.Organization).HasForeignKey(s => s.OrganizationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.AuditLogs).WithOne(a => a.Organization).HasForeignKey(a => a.OrganizationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.PasswordSalt).IsRequired();
                entity.Property(e => e.Role).HasConversion<int>();
                entity.HasMany(e => e.AuditLogs).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.SetNull);
                entity.HasIndex(e => new { e.OrganizationId, e.Email }).IsUnique();
            });

            // MonitoredAsset Configuration
            modelBuilder.Entity<MonitoredAsset>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Value).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Type).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
                entity.Property(e => e.Priority).HasConversion<int>();
                entity.Property(e => e.ScanFrequency).HasConversion<int>();
                entity.HasMany(e => e.ThreatAlerts).WithOne(a => a.MonitoredAsset).HasForeignKey(a => a.MonitoredAssetId).OnDelete(DeleteBehavior.Cascade);
            });

            // ThreatAlert Configuration
            modelBuilder.Entity<ThreatAlert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.ThreatType).HasConversion<int>();
                entity.Property(e => e.Severity).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
                entity.HasMany(e => e.AlertActions).WithOne(a => a.Alert).HasForeignKey(a => a.AlertId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.AlertNotifications).WithOne(n => n.Alert).HasForeignKey(n => n.AlertId).OnDelete(DeleteBehavior.Cascade);
            });

            // AlertAction Configuration
            modelBuilder.Entity<AlertAction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ActionType).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
            });

            // AlertNotification Configuration
            modelBuilder.Entity<AlertNotification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Channel).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
                entity.Property(e => e.RecipientAddress).IsRequired().HasMaxLength(500);
            });

            // ScanHistory Configuration
            modelBuilder.Entity<ScanHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ScanType).HasConversion<int>();
                entity.Property(e => e.ScanSource).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
            });

            // AuditLog Configuration
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Action).HasConversion<int>();
                entity.Property(e => e.Status).HasConversion<int>();
                entity.HasIndex(e => new { e.OrganizationId, e.CreatedAt });
                entity.HasIndex(e => new { e.UserId, e.CreatedAt });
            });
        }
    }
}
