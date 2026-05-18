using System;
using System.Threading.Tasks;
using ThreatIntelligence.Domain.Entities;

namespace ThreatIntelligence.Domain.Repositories
{
    /// <summary>
    /// وحدة العمل - توحيد جميع المستودعات
    /// Unit of Work - Centralize all repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Organization> Organizations { get; }
        IRepository<User> Users { get; }
        IRepository<MonitoredAsset> MonitoredAssets { get; }
        IRepository<ThreatAlert> ThreatAlerts { get; }
        IRepository<AlertAction> AlertActions { get; }
        IRepository<AlertNotification> AlertNotifications { get; }
        IRepository<ScanHistory> ScanHistories { get; }
        IRepository<AuditLog> AuditLogs { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
