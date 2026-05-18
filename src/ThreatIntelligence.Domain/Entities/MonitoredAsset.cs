using System;
using System.Collections.Generic;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// الأصل المراقب - البريد، النطاق، المفاتيح، إلخ
    /// Monitored Asset - Email, Domain, Keys, etc.
    /// </summary>
    public class MonitoredAsset
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; } // القيمة الفعلية (البريد، النطاق، المفتاح)
        public AssetType Type { get; set; }
        public AssetStatus Status { get; set; }
        public AssetPriority Priority { get; set; }
        
        // المسح
        public bool IsMonitoringActive { get; set; }
        public DateTime? LastScannedAt { get; set; }
        public DateTime? NextScanScheduledAt { get; set; }
        public ScanFrequency ScanFrequency { get; set; }
        
        // الإحصائيات
        public int ThreatCount { get; set; }
        public int CriticalThreatCount { get; set; }
        public int HighThreatCount { get; set; }
        public int MediumThreatCount { get; set; }
        public int LowThreatCount { get; set; }
        
        // الحالة
        public bool IsCompromised { get; set; }
        public DateTime? CompromisedAt { get; set; }
        public string Notes { get; set; }
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        // العلاقات
        public virtual Organization Organization { get; set; }
        public virtual ICollection<ThreatAlert> ThreatAlerts { get; set; }
    }
    
    public enum AssetType
    {
        Email = 1,
        Domain = 2,
        ApiKey = 3,
        Secret = 4,
        Token = 5,
        Username = 6,
        BrandName = 7,
        PhoneNumber = 8,
        IpAddress = 9,
        Certificate = 10
    }
    
    public enum AssetStatus
    {
        Active = 1,
        Inactive = 2,
        Compromised = 3,
        Suspended = 4
    }
    
    public enum AssetPriority
    {
        Critical = 1,
        High = 2,
        Medium = 3,
        Low = 4
    }
    
    public enum ScanFrequency
    {
        Hourly = 1,
        TwiceDaily = 2,
        Daily = 3,
        Weekly = 4,
        Monthly = 5
    }
}
