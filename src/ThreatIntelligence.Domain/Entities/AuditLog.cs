using System;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// سجل التدقيق - تتبع أمني شامل لجميع الأنشطة
    /// Audit Log - Comprehensive security tracking of all activities
    /// </summary>
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid? UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserIpAddress { get; set; }
        public string UserAgent { get; set; }
        
        // الإجراء
        public AuditAction Action { get; set; }
        public string Entity { get; set; } // نوع الكيان (User, Organization, Alert, etc.)
        public string EntityId { get; set; }
        
        // التفاصيل
        public string Description { get; set; }
        public string ChangeBefore { get; set; } // JSON - القيم قبل التغيير
        public string ChangeAfter { get; set; }  // JSON - القيم بعد التغيير
        public AuditStatus Status { get; set; }
        
        // معلومات إضافية
        public string Comments { get; set; }
        public string SensitivityLevel { get; set; }
        public bool IsSecurityRelated { get; set; }
        
        // التاريخ
        public DateTime CreatedAt { get; set; }
        
        // العلاقات
        public virtual Organization Organization { get; set; }
        public virtual User User { get; set; }
    }
    
    public enum AuditAction
    {
        // المستخدم
        UserCreated = 1,
        UserUpdated = 2,
        UserDeleted = 3,
        UserLoggedIn = 4,
        UserLoggedOut = 5,
        UserPasswordChanged = 6,
        UserMfaEnabled = 7,
        UserMfaDisabled = 8,
        UserLocked = 9,
        UserUnlocked = 10,
        
        // المؤسسة
        OrganizationCreated = 11,
        OrganizationUpdated = 12,
        OrganizationDeleted = 13,
        SubscriptionPlanChanged = 14,
        SubscriptionCancelled = 15,
        
        // الأصول
        AssetCreated = 21,
        AssetUpdated = 22,
        AssetDeleted = 23,
        AssetMonitoringEnabled = 24,
        AssetMonitoringDisabled = 25,
        
        // التنبيهات
        AlertCreated = 31,
        AlertAcknowledged = 32,
        AlertResolved = 33,
        AlertActionTaken = 34,
        AlertEscalated = 35,
        
        // المسح
        ScanTriggered = 41,
        ScanCompleted = 42,
        ScanFailed = 43,
        
        // الإشعارات
        NotificationSent = 51,
        NotificationFailed = 52,
        
        // الإعدادات
        SettingsChanged = 61,
        ApiKeyCreated = 62,
        ApiKeyRevoked = 63,
        
        // الأمان
        SecurityPolicyUpdated = 71,
        FailedLoginAttempt = 72,
        UnauthorizedAccessAttempt = 73,
        SuspiciousActivityDetected = 74,
        
        // الأخرى
        ReportGenerated = 81,
        ExportCompleted = 82,
        BackupCompleted = 83
    }
    
    public enum AuditStatus
    {
        Success = 1,
        Failed = 2,
        PartialSuccess = 3
    }
}
