using System;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// إجراء التنبيه - إجراء يتم اتخاذه ردة فعل على التهديد
    /// Alert Action - Action taken in response to a threat
    /// </summary>
    public class AlertAction
    {
        public Guid Id { get; set; }
        public Guid AlertId { get; set; }
        public ActionType ActionType { get; set; }
        public ActionStatus Status { get; set; }
        
        // تفاصيل الإجراء
        public string Description { get; set; }
        public string TargetId { get; set; } // معرف الهدف (موظف، API key، إلخ)
        public string TargetValue { get; set; }
        
        // التنفيذ
        public bool IsAutomatic { get; set; }
        public bool IsExecuted { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public string ExecutionResult { get; set; }
        public string ErrorMessage { get; set; }
        
        // المسؤول
        public Guid? ExecutedByUserId { get; set; }
        
        // المعلومات الإضافية
        public int RetryCount { get; set; }
        public DateTime? NextRetryAt { get; set; }
        public string Notes { get; set; }
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // العلاقات
        public virtual ThreatAlert Alert { get; set; }
    }
    
    public enum ActionType
    {
        ChangePassword = 1,            // تغيير كلمة المرور
        RotateApiKey = 2,              // تدوير مفتاح API
        RotateSecret = 3,              // تدوير السر
        EnableMfa = 4,                 // تفعيل المصادقة الثنائية
        LockAccount = 5,               // قفل الحساب
        NotifyUser = 6,                // إخطار المستخدم
        ReportPhishing = 7,            // الإبلاغ عن الاحتيال
        TakeDownContent = 8,           // إزالة المحتوى
        BlockDomain = 9,               // حظر النطاق
        RevokeToken = 10,              // إلغاء الرمز
        DisableAccount = 11,           // تعطيل الحساب
        ChangeEmail = 12,              // تغيير البريد الإلكتروني
        RestrictAccess = 13,           // تقييد الوصول
        EnableLogging = 14,            // تفعيل التسجيل
        UpdateSecurityGroup = 15,      // تحديث مجموعة الأمان
        RequestAudit = 16,             // طلب تدقيق
        NotifySecurityTeam = 17,       // إخطار فريق الأمان
        UpdateFirewall = 18,           // تحديث جدار الحماية
        MonitorActivity = 19,          // مراقبة النشاط
        GenerateReport = 20            // إنشاء تقرير
    }
    
    public enum ActionStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Failed = 4,
        Cancelled = 5,
        Scheduled = 6
    }
}
