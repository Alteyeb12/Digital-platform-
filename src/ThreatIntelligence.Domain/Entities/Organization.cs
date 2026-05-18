using System;
using System.Collections.Generic;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// المؤسسة - تمثل شركة أو عميل مشترك
    /// Organization - Represents a company or subscriber client
    /// </summary>
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        
        // الاشتراك والموارد
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public int MaxEmails { get; set; }
        public int MaxDomains { get; set; }
        public int MaxApiKeys { get; set; }
        public int MaxUsers { get; set; }
        
        // الاستهلاك الحالي
        public int CurrentEmailCount { get; set; }
        public int CurrentDomainCount { get; set; }
        public int CurrentApiKeyCount { get; set; }
        public int CurrentUserCount { get; set; }
        
        // الحالة والتاريخ
        public OrganizationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? SubscriptionExpiresAt { get; set; }
        public DateTime? LastBillingDate { get; set; }
        public DateTime? NextBillingDate { get; set; }
        
        // الإعدادات
        public bool EnableMfa { get; set; }
        public bool EnableAuditLog { get; set; }
        public int AlertRetentionDays { get; set; } // عدد أيام الاحتفاظ بالتنبيهات
        public bool AutomaticallyExecuteActions { get; set; }
        
        // العلاقات
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<MonitoredAsset> MonitoredAssets { get; set; }
        public virtual ICollection<ThreatAlert> ThreatAlerts { get; set; }
        public virtual ICollection<ScanHistory> ScanHistories { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
    
    public enum SubscriptionPlan
    {
        Starter = 1,      // 500 جنيه
        Professional = 2, // 1500 جنيه
        Enterprise = 3    // حسب الطلب
    }
    
    public enum OrganizationStatus
    {
        Active = 1,
        Suspended = 2,
        Cancelled = 3,
        TrialMode = 4
    }
}
