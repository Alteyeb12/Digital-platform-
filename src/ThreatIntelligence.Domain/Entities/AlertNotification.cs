using System;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// إخطار التنبيه - إرسال التنبيهات عبر قنوات مختلفة
    /// Alert Notification - Sending alerts through different channels
    /// </summary>
    public class AlertNotification
    {
        public Guid Id { get; set; }
        public Guid AlertId { get; set; }
        public NotificationChannel Channel { get; set; }
        public string RecipientAddress { get; set; } // البريد، رقم الهاتف، webhook URL، إلخ
        
        // الرسالة
        public string Subject { get; set; }
        public string Message { get; set; }
        public string HtmlMessage { get; set; }
        
        // الحالة
        public NotificationStatus Status { get; set; }
        public int RetryCount { get; set; }
        public DateTime? SentAt { get; set; }
        public string DeliveryResponse { get; set; }
        public string ErrorMessage { get; set; }
        
        // المحاولات
        public DateTime? LastRetryAt { get; set; }
        public DateTime? NextRetryAt { get; set; }
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // العلاقات
        public virtual ThreatAlert Alert { get; set; }
    }
    
    public enum NotificationChannel
    {
        Email = 1,
        Sms = 2,
        PushNotification = 3,
        Slack = 4,
        MicrosoftTeams = 5,
        Discord = 6,
        Webhook = 7
    }
    
    public enum NotificationStatus
    {
        Pending = 1,
        Sent = 2,
        Failed = 3,
        Delivered = 4,
        Undeliverable = 5,
        Bounced = 6
    }
}
