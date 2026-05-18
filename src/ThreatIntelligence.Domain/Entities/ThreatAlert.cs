using System;
using System.Collections.Generic;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// تنبيه التهديد - يمثل تهديد مكتشف
    /// Threat Alert - Represents a detected threat
    /// </summary>
    public class ThreatAlert
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid MonitoredAssetId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ThreatType ThreatType { get; set; }
        public ThreatSeverity Severity { get; set; }
        
        // تفاصيل التهديد
        public string Source { get; set; } // من أين تم اكتشاف التهديد
        public string SourceUrl { get; set; } // رابط المصدر
        public DateTime DiscoveredAt { get; set; }
        public DateTime? FirstOccurredAt { get; set; }
        
        // الحالة
        public AlertStatus Status { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string ResolutionNotes { get; set; }
        
        // الإجراءات
        public bool ActionTaken { get; set; }
        public DateTime? ActionTakenAt { get; set; }
        
        // المعلومات الإضافية
        public string Evidence { get; set; } // الإثباتات (JSON)
        public bool IsNotified { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public string ImpactAssessment { get; set; } // تقييم التأثير
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // العلاقات
        public virtual Organization Organization { get; set; }
        public virtual MonitoredAsset MonitoredAsset { get; set; }
        public virtual ICollection<AlertAction> AlertActions { get; set; }
        public virtual ICollection<AlertNotification> AlertNotifications { get; set; }
    }
    
    public enum ThreatType
    {
        EmailLeaked = 1,              // بريد إلكتروني مسرب
        PasswordLeaked = 2,            // كلمة مرور مسربة
        ApiKeyExposed = 3,            // مفتاح API مكشوف
        SecretExposed = 4,            // سر/رمز مكشوف
        PhishingDomain = 5,           // نطاق انتحالي
        BrandMisuse = 6,              // إساءة استخدام العلامة التجارية
        IdentityTheft = 7,            // انتحال الشخصية
        DataBreach = 8,               // تسريب بيانات
        MalwareDetected = 9,          // برنامج ضار مكتشف
        SourceCodeLeaked = 10,        // أكواد مشروع مسربة
        SslCertificateExpired = 11,   // شهادة SSL منتهية
        DomainTakeover = 12,          // استيلاء على النطاق
        DnsHijacking = 13,            // اختطاف DNS
        ReputationDamage = 14,        // ضرر السمعة
        EmployeeDataExposed = 15,     // بيانات موظفين مكشوفة
        DatabaseExposed = 16,         // قاعدة بيانات مكشوفة
        ConfigFileLeaked = 17,        // ملف إعدادات مسرب
        PrivateKeyExposed = 18,       // مفتاح خاص مكشوف
        UnauthorizedAccess = 19       // وصول غير مصرح
    }
    
    public enum ThreatSeverity
    {
        Critical = 1,
        High = 2,
        Medium = 3,
        Low = 4,
        Info = 5
    }
    
    public enum AlertStatus
    {
        New = 1,
        Acknowledged = 2,
        InProgress = 3,
        Resolved = 4,
        FalsePositive = 5,
        Ignored = 6
    }
}
