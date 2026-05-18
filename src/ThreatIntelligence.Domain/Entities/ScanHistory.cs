using System;

namespace ThreatIntelligence.Domain.Entities
{
    /// <summary>
    /// سجل المسح - تاريخ عمليات المسح والفحص
    /// Scan History - History of scanning operations
    /// </summary>
    public class ScanHistory
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid? MonitoredAssetId { get; set; }
        public ScanType ScanType { get; set; }
        public ScanSource ScanSource { get; set; }
        
        // تفاصيل المسح
        public string QueryText { get; set; } // ما تم البحث عنه
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int DurationSeconds { get; set; }
        
        // النتائج
        public int ThreatCountFound { get; set; }
        public int ItemsScanned { get; set; }
        public ScanStatus Status { get; set; }
        
        // الأخطاء
        public string ErrorMessage { get; set; }
        public bool IsSuccessful { get; set; }
        
        // معلومات إضافية
        public string DataSnapshot { get; set; } // JSON - محفوظة النتائج
        public bool IsManualTrigger { get; set; }
        public string TriggeredBy { get; set; }
        
        // التواريخ
        public DateTime CreatedAt { get; set; }
        
        // العلاقات
        public virtual Organization Organization { get; set; }
    }
    
    public enum ScanType
    {
        DarkWebScan = 1,              // مسح الإنترنت المظلم
        LeakedDatabaseScan = 2,       // مسح قاعدة البيانات المسربة
        GitHubScan = 3,              // مسح GitHub
        GitLabScan = 4,              // مسح GitLab
        BitbucketScan = 5,           // مسح Bitbucket
        PastebinScan = 6,            // مسح Pastebin
        TwitterScan = 7,             // مسح Twitter
        LinkedInScan = 8,            // مسح LinkedIn
        FacebookScan = 9,            // مسح Facebook
        RedditScan = 10,             // مسح Reddit
        PhishingDetection = 11,      // كشف الاحتيال
        BrandMonitoring = 12,        // مراقبة العلامة التجارية
        MalwareDetection = 13,       // كشف البرامج الضارة
        SslCertificateCheck = 14,    // فحص شهادة SSL
        DnsRecordCheck = 15,         // فحص سجلات DNS
        WhoisCheck = 16,             // فحص معلومات النطاق
        PortScan = 17,               // مسح المنافذ
        VulnerabilityScan = 18,      // مسح الثغرات
        ComplianceCheck = 19,        // فحص الامتثال
        GeneralWebSearch = 20        // بحث عام على الويب
    }
    
    public enum ScanSource
    {
        HaveibeenPwned = 1,
        Shodan = 2,
        GitHubApi = 3,
        BingSearch = 4,
        GoogleSearch = 5,
        Twitter = 6,
        LinkedInApi = 7,
        CustomSource = 8,
        InternalDatabase = 9,
        ExternalApi = 10
    }
    
    public enum ScanStatus
    {
        Pending = 1,
        Running = 2,
        Completed = 3,
        Failed = 4,
        Timeout = 5,
        Cancelled = 6
    }
}
