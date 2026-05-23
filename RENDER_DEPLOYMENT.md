# دليل النشر على منصة Render

## نظرة عامة
هذا الدليل يساعدك في نشر تطبيق Threat Intelligence Platform على منصة Render بنجاح.

## المتطلبات الأساسية

- حساب على [Render.com](https://render.com)
- مستودع GitHub متصل بحسابك
- متغيرات البيئة الضرورية معدة

## خطوات النشر

### 1. إعداد Render.com

1. قم بزيارة [Render.com](https://render.com) وسجل الدخول
2. اضغط على **"+ New"** واختر **"Web Service"**
3. اختر **"Deploy from a Git repository"**
4. ربط حسابك على GitHub وحدد المستودع `Digital-platform-`

### 2. تكوين Web Service

عند إنشاء الخدمة الجديدة:

#### المعلومات الأساسية:
- **Name**: `threat-intelligence-api` (أو اسم آخر)
- **Region**: اختر المنطقة الأقرب (`Ohio`, `Oregon`, إلخ)
- **Branch**: `main`
- **Runtime**: `Docker`

#### المتغيرات البيئية (Environment Variables):

أضف المتغيرات التالية:

```
ASPNETCORE_ENVIRONMENT = Production
ASPNETCORE_URLS = http://+:8080
JWT_SECRET = your-super-secret-jwt-key-change-this
API_KEY = your-api-key-change-this
DB_CONNECTION_STRING = (سيتم إعداده تلقائياً من قاعدة البيانات)
REDIS_URL = (سيتم إعداده تلقائياً من Redis)
EMAIL_SMTP_HOST = smtp.gmail.com
EMAIL_SMTP_PORT = 587
EMAIL_SMTP_USER = your-email@gmail.com
EMAIL_SMTP_PASSWORD = your-app-password
```

### 3. إنشاء قاعدة البيانات

1. في لوحة Render، اختر **"+ New"** ثم **"PostgreSQL"**
2. أدخل التفاصيل:
   - **Name**: `threat-intel-db`
   - **Region**: نفس منطقة الخدمة الرئيسية
   - **Database**: `threat_intelligence`
   - **User**: `postgres`

3. نسخ `Connection String` وإضافته إلى متغيرات البيئة

### 4. إنشاء Redis Cache

1. اختر **"+ New"** ثم **"Redis"**
2. أدخل التفاصيل:
   - **Name**: `threat-intel-cache`
   - **Region**: نفس المنطقة
   - **Plan**: Free (أو Starter حسب احتياجاتك)

### 5. تحديث ملف Program.cs

تأكد من أن تطبيقك يحتوي على:

```csharp
// Health check endpoint
app.MapGet("/health", () => Results.Ok("Healthy"))
    .WithName("Health Check")
    .WithOpenApi();

// اتصال قاعدة البيانات
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

// اتصال Redis
var redisUrl = Environment.GetEnvironmentVariable("REDIS_URL") 
    ?? builder.Configuration.GetConnectionString("Redis");

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

app.UseCors();
```

## متغيرات البيئة المهمة

### Required (إلزامي)
- `ASPNETCORE_ENVIRONMENT`: دائماً `Production`
- `ASPNETCORE_URLS`: دائماً `http://+:8080`
- `JWT_SECRET`: مفتاح سري قوي (32+ حرف)
- `API_KEY`: مفتاح API سري

### Database (قاعدة البيانات)
```
DB_HOST=your-db-host.render.com
DB_PORT=5432
DB_NAME=threat_intelligence
DB_USER=postgres
DB_PASSWORD=your-db-password
DATABASE_URL=postgresql://postgres:password@host:5432/threat_intelligence
```

### Redis
```
REDIS_URL=redis://:password@host:6379
REDIS_HOST=your-redis-host.render.com
REDIS_PORT=6379
REDIS_PASSWORD=your-redis-password
```

### Email
```
EMAIL_SMTP_HOST=smtp.gmail.com
EMAIL_SMTP_PORT=587
EMAIL_SMTP_USER=your-email@gmail.com
EMAIL_SMTP_PASSWORD=your-app-password
EMAIL_FROM=noreply@threat-intelligence.com
```

### Security (الأمان)
```
SECURE_COOKIES=true
ALLOW_ANONYMOUS_REGISTRATION=false
MAX_LOGIN_ATTEMPTS=5
LOCKOUT_DURATION_MINUTES=30
```

## ترحيل قاعدة البيانات

بعد النشر الأول، قد تحتاج لترحيل قاعدة البيانات:

### Option 1: Automatic (تلقائي)
أضف أمر pre-deploy في `render.yaml`:
```yaml
preDeployCommand: "dotnet ef database update"
```

### Option 2: Manual (يدوي)
1. تصل عبر SSH إلى Render
2. قم بتشغيل الأوامر:
```bash
dotnet ef database update
```

## استكشاف الأخطاء

### خطأ: "Health check failed"
- تأكد من وجود `/health` endpoint في البرنامج
- تحقق من logs: `Logs` tab في Render Dashboard

### خطأ: "Database connection failed"
- تحقق من `DATABASE_URL` يحتوي على البيانات الصحيحة
- تأكد من أن قاعدة البيانات في نفس المنطقة

### خطأ: "Port not listening"
- تأكد من أن التطبيق يستمع على المنفذ `8080`
- استخدم متغير البيئة `ASPNETCORE_URLS=http://+:8080`

### خطأ: "Out of memory"
- قد تحتاج لنسخة مدفوعة من Render
- قلل عدد المعالجات المتزامنة في الإعدادات

## المراقبة والصيانة

### Logs
- انقر على **"Logs"** في Render Dashboard لمراقبة تطبيقك

### Metrics
- انقر على **"Metrics"** لرؤية الأداء والموارد

### Auto-deploy
- يتم التحديث تلقائياً عند الدفع إلى فرع `main`

## النسخ الاحتياطية

### PostgreSQL Backup
```sql
-- في Render Dashboard تحت Database
-- اضغط "Create Backup"
```

### Redis Backup
- استخدم الأوامر:
```bash
redis-cli BGSAVE
```

## تكاليف Render

| الخدمة | المجاني | الحد الأدنى المدفوع |
|--------|--------|------------------|
| Web Service | 0.5 CPU, 0.5GB RAM | $7/شهر |
| PostgreSQL | 1GB | $15/شهر |
| Redis | 0.5GB | $5/شهر |

## نصائح مهمة

✅ **افعل:**
- استخدم متغيرات البيئة للحساسة البيانات
- فعّل HTTPS (Render يفعله تلقائياً)
- راقب logs و metrics بانتظام
- عمل نسخ احتياطية منتظمة

❌ **لا تفعل:**
- لا تخزن بيانات حساسة في الكود
- لا تستخدم قاعدة بيانات محلية
- لا تترك المجلدات العامة مفتوحة
- لا تستخدم كلمات مرور ضعيفة

## البدائل للخدمات غير المتوفرة

### SQL Server → PostgreSQL
```csharp
// تحديث DbContext
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);
```

### RabbitMQ → Managed Queue (بديل)
استخدم Render's built-in support أو AWS SQS

### Elasticsearch → PostgreSQL Full-Text Search
```sql
SELECT * FROM documents 
WHERE to_tsvector(content) @@ plainto_tsquery('search term');
```

## الدعم والمساعدة

- Render Documentation: https://render.com/docs
- Community Support: https://render.com/community
- Email Support: support@render.com

---
تم تحديث الملف آخر مرة: مايو 2026
