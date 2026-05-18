using System;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// كيان سجل التدقيق / Audit Log Entity
    /// يسجل جميع الأنشطة الحساسة في النظام
    /// </summary>
    public class AuditLog
    {
        public Guid Id { get; set; }
        
        public Guid OrganizationId { get; set; }
        
        public Guid? UserId { get; set; } // قد يكون فارغاً للعمليات التلقائية
        
        public string ActionType { get; set; } // See AuditActionTypes below
        
        public string ResourceType { get; set; } // Organization, User, Asset, Alert, etc.
        
        public string ResourceId { get; set; } // معرف المورد المتأثر
        
        public string ResourceName { get; set; } // اسم المورد
        
        public string Action { get; set; } // Create, Update, Delete, View, etc.
        
        public string Description { get; set; } // وصف الإجراء
        
        public string OldValues { get; set; } // القيم القديمة (JSON)
        
        public string NewValues { get; set; } // القيم الجديدة (JSON)
        
        public string IpAddress { get; set; } // عنوان IP للطلب
        
        public string UserAgent { get; set; } // معلومات المتصفح
        
        public DateTime CreatedAt { get; set; }
        
        public string Status { get; set; } // Success, Failure
        
        public string ErrorMessage { get; set; } // رسالة الخطأ إن وجدت
        
        public int? StatusCode { get; set; } // رمز الحالة HTTP
        
        public long? DurationMs { get; set; } // مدة العملية بالميلي ثانية
        
        public bool IsSuccessful { get; set; }
        
        public bool IsSensitive { get; set; } = false; // إذا كانت عملية حساسة جداً
        
        public string EntityState { get; set; } // Added, Modified, Deleted, Unchanged
        
        // Navigation Properties
        public virtual Organization Organization { get; set; }
        
        public virtual User User { get; set; }
    }
    
    /// <summary>
    /// أنواع إجراءات التدقيق
    /// Audit Action Types
    /// </summary>
    public static class AuditActionTypes
    {
        // User Management
        public const string UserCreated = "UserCreated";
        public const string UserUpdated = "UserUpdated";
        public const string UserDeleted = "UserDeleted";
        public const string UserLoggedIn = "UserLoggedIn";
        public const string UserLoggedOut = "UserLoggedOut";
        public const string UserPasswordChanged = "UserPasswordChanged";
        public const string UserLocked = "UserLocked";
        public const string UserUnlocked = "UserUnlocked";
        public const string MfaEnabled = "MfaEnabled";
        public const string MfaDisabled = "MfaDisabled";
        
        // Asset Management
        public const string AssetAdded = "AssetAdded";
        public const string AssetUpdated = "AssetUpdated";
        public const string AssetDeleted = "AssetDeleted";
        public const string AssetMonitoringEnabled = "AssetMonitoringEnabled";
        public const string AssetMonitoringDisabled = "AssetMonitoringDisabled";
        
        // Alert Management
        public const string AlertAcknowledged = "AlertAcknowledged";
        public const string AlertResolved = "AlertResolved";
        public const string AlertStatusChanged = "AlertStatusChanged";
        
        // Action Management
        public const string ActionCreated = "ActionCreated";
        public const string ActionCompleted = "ActionCompleted";
        public const string ActionFailed = "ActionFailed";
        
        // Access Control
        public const string AccessGranted = "AccessGranted";
        public const string AccessRevoked = "AccessRevoked";
        public const string PermissionChanged = "PermissionChanged";
        
        // Configuration
        public const string SettingsChanged = "SettingsChanged";
        public const string NotificationPreferencesChanged = "NotificationPreferencesChanged";
        public const string SubscriptionChanged = "SubscriptionChanged";
        
        // System
        public const string ScanStarted = "ScanStarted";
        public const string ScanCompleted = "ScanCompleted";
        public const string DataExported = "DataExported";
        public const string ReportGenerated = "ReportGenerated";
        
        // Security
        public const string SecurityIncidentReported = "SecurityIncidentReported";
        public const string SuspiciousActivityDetected = "SuspiciousActivityDetected";
    }
}
