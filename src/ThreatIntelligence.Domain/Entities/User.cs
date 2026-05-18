using System;
using System.Collections.Generic;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// المستخدم - موظف في المؤسسة
    /// User - Employee in the organization
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        
        // المصادقة
        public UserRole Role { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsMfaEnabled { get; set; }
        public string MfaSecret { get; set; }
        public bool IsActive { get; set; }
        
        // أمان
        public int FailedLoginAttempts { get; set; }
        public DateTime? LastFailedLoginAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string LastLoginIp { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockedUntil { get; set; }
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastPasswordChangeAt { get; set; }
        public DateTime? DeletedAt { get; set; } // Soft Delete
        
        // الإشعارات
        public bool NotifyByEmail { get; set; }
        public bool NotifyBySms { get; set; }
        public bool NotifyBySlack { get; set; }
        
        // العلاقات
        public virtual Organization Organization { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
    
    public enum UserRole
    {
        Admin = 1,           // مسؤول كامل الصلاحيات
        SecurityManager = 2, // مدير الأمان
        Analyst = 3,         // محلل التهديدات
        Viewer = 4          // عارض فقط (قراءة)
    }
}
