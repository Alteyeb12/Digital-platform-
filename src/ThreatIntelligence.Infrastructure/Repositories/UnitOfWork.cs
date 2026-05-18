using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using ThreatIntelligence.Domain.Entities;
using ThreatIntelligence.Domain.Repositories;
using ThreatIntelligence.Infrastructure.Data;

namespace ThreatIntelligence.Infrastructure.Repositories
{
    /// <summary>
    /// تطبيق وحدة العمل
    /// Unit of Work Implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThreatIntelligenceDbContext _context;
        private IDbContextTransaction _transaction;
        
        private IRepository<Organization> _organizationRepository;
        private IRepository<User> _userRepository;
        private IRepository<MonitoredAsset> _monitoredAssetRepository;
        private IRepository<ThreatAlert> _threatAlertRepository;
        private IRepository<AlertAction> _alertActionRepository;
        private IRepository<AlertNotification> _alertNotificationRepository;
        private IRepository<ScanHistory> _scanHistoryRepository;
        private IRepository<AuditLog> _auditLogRepository;

        public UnitOfWork(ThreatIntelligenceDbContext context)
        {
            _context = context;
        }

        public IRepository<Organization> Organizations => _organizationRepository ??= new Repository<Organization>(_context);
        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<MonitoredAsset> MonitoredAssets => _monitoredAssetRepository ??= new Repository<MonitoredAsset>(_context);
        public IRepository<ThreatAlert> ThreatAlerts => _threatAlertRepository ??= new Repository<ThreatAlert>(_context);
        public IRepository<AlertAction> AlertActions => _alertActionRepository ??= new Repository<AlertAction>(_context);
        public IRepository<AlertNotification> AlertNotifications => _alertNotificationRepository ??= new Repository<AlertNotification>(_context);
        public IRepository<ScanHistory> ScanHistories => _scanHistoryRepository ??= new Repository<ScanHistory>(_context);
        public IRepository<AuditLog> AuditLogs => _auditLogRepository ??= new Repository<AuditLog>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction?.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction?.RollbackAsync();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
