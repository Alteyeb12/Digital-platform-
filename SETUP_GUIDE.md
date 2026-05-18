# 📖 Digital Platform - Setup Guide

**Language:** العربية | [English](#english-version)

## 🎯 دليل الإعداد الشامل

هذا الدليل يساعدك على إعداد وتشغيل منصة Threat Intelligence بسهولة.

---

## ✅ المتطلبات الأساسية

قبل البدء، تأكد من وجود:

### 🖥️ Windows
1. **Docker Desktop** - https://docs.docker.com/desktop/install/windows-install/
2. **.NET SDK** - https://dotnet.microsoft.com/download
3. **Git** - https://git-scm.com/download/win

### 🍎 macOS
1. **Docker Desktop** - https://docs.docker.com/desktop/install/mac-install/
2. **.NET SDK** - https://dotnet.microsoft.com/download
3. **Git** - https://git-scm.com/download/mac

### 🐧 Linux (Ubuntu/Debian)
```bash
# Install Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh

# Install Docker Compose
sudo apt-get install docker-compose

# Install .NET SDK
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh

# Install Git
sudo apt-get install git
```

---

## 🚀 البدء السريع

### الخطوة 1: استنساخ المستودع

```bash
git clone https://github.com/Alteyeb12/Digital-platform-.git
cd Digital-platform-
```

### الخطوة 2: الإعداد الآلي

**على Windows:**
```bash
setup.bat
```

**على Linux/Mac:**
```bash
chmod +x setup.sh
./setup.sh
```

### الخطوة 3: تشغيل التطبيق

```bash
# انتقل لمجلد المصدر
cd src

# استعادة الحزم
dotnet restore

# بناء الحل
dotnet build

# تشغيل التطبيق
dotnet run
```

---

## ⚙️ الإعداد اليدوي

إذا كنت تفضل الإعداد اليدوي، اتبع الخطوات التالية:

### 1️⃣ تشغيل خدمات Docker

```bash
docker-compose up -d
```

هذا سيشغل:
- 🗄️ **SQL Server** - قاعدة البيانات
- 🔴 **Redis** - التخزين المؤقت
- 🐰 **RabbitMQ** - معالج الرسائل
- 🔍 **Elasticsearch** - محرك البحث
- 📊 **Kibana** - لوحة التحكم

### 2️⃣ التحقق من حالة الخدمات

```bash
docker-compose ps
```

يجب أن ترى جميع الخدمات مع حالة "Up":

```
CONTAINER ID   IMAGE                          STATUS
xxx            mcr.microsoft.com/mssql...     Up (healthy)
xxx            redis:7-alpine                 Up (healthy)
xxx            rabbitmq:3.12-management      Up (healthy)
xxx            elasticsearch:8.0.0            Up (healthy)
xxx            kibana:8.0.0                   Up (healthy)
```

### 3️⃣ إعداد متغيرات البيئة

```bash
# نسخ ملف المثال
cp .env.example .env

# تعديل .env حسب احتياجاتك
# اترك القيم الافتراضية للتطوير المحلي
```

### 4️⃣ استعادة وبناء المشروع

```bash
cd src

# استعادة حزم NuGet
dotnet restore

# بناء الحل
dotnet build
```

### 5️⃣ تطبيق قاعدة البيانات

```bash
# إذا كان المشروع يستخدم Entity Framework
dotnet ef database update
```

### 6️⃣ تشغيل التطبيق

```bash
dotnet run
```

التطبيق سيكون متاحًا على: **http://localhost:5000**

---

## 🔌 روابط الوصول للخدمات

| الخدمة | الرابط | المستخدم | كلمة السر |
|--------|--------|---------|-----------|
| **API** | http://localhost:5000 | - | - |
| **Swagger UI** | http://localhost:5000/swagger | - | - |
| **SQL Server** | localhost:1433 | sa | P@ssw0rd1234 |
| **Redis** | localhost:6379 | - | - |
| **RabbitMQ** | http://localhost:15672 | guest | guest |
| **Elasticsearch** | http://localhost:9200 | - | - |
| **Kibana** | http://localhost:5601 | - | - |

---

## 📝 ملف .env

يحتوي ملف `.env` على جميع إعدادات التطبيق:

```bash
# قاعدة البيانات
DB_CONNECTION_STRING=Server=sqlserver;Database=ThreatIntelligence;User Id=sa;Password=P@ssw0rd1234;

# Redis
REDIS_CONNECTION_STRING=redis:6379

# RabbitMQ
RABBITMQ_USER=guest
RABBITMQ_PASSWORD=guest
RABBITMQ_HOST=rabbitmq

# مفاتيح API الخارجية
HAVEIBEEN_PWNED_API_KEY=your-api-key
SHODAN_API_KEY=your-api-key
```

### ⚠️ نصيحة أمان مهمة:
- لا تضع مفاتيح حقيقية في `.env` أثناء التطوير
- لا تسجل ملف `.env` في Git (مُدرج في `.gitignore`)
- غيّر كل كلمات المرور قبل النشر للإنتاج

---

## 🐛 استكشاف الأخطاء

### المشكلة: الخدمات لا تبدأ

```bash
# تحقق من حالة الخدمات
docker-compose ps

# اعرض السجلات
docker-compose logs -f

# اعد بناء الصور
docker-compose build --no-cache
docker-compose up -d
```

### المشكلة: خطأ في الاتصال بقاعدة البيانات

```bash
# تحقق من أن SQL Server يعمل
docker exec threat-intel-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "P@ssw0rd1234" -Q "SELECT 1"

# أعد تشغيل خدمة SQL Server
docker-compose restart sqlserver
```

### المشكلة: المنافذ مشغولة

إذا حصلت على خطأ "port already in use"، قم بأحد الآتي:

**الخيار 1:** أوقف التطبيقات الأخرى على المنفذ

**الخيار 2:** غيّر المنافذ في `docker-compose.yml`:

```yaml
sqlserver:
  ports:
    - "1434:1433"  # غيّر 1434 حسب الحاجة
```

### المشكلة: استهلاك الذاكرة العالي

قلل موارد Docker:
- افتح Docker Desktop
- اذهب إلى Settings → Resources
- قلل عدد CPUs والذاكرة

---

## 🔧 أوامر مفيدة

### أوامر Docker Compose

```bash
# عرض السجلات
docker-compose logs -f

# عرض سجلات خدمة محددة
docker-compose logs -f sqlserver

# إيقاف الخدمات
docker-compose stop

# بدء الخدمات
docker-compose start

# إعادة تشغيل الخدمات
docker-compose restart

# حذف الحاويات والبيانات
docker-compose down -v
```

### أوامر .NET

```bash
# استعادة الحزم
dotnet restore

# بناء الحل
dotnet build

# تشغيل الاختبارات
dotnet test

# تشغيل التطبيق بوضع التطوير
dotnet run

# نشر التطبيق
dotnet publish -c Release
```

### أوامر Entity Framework

```bash
# عرض الترحيلات المعلقة
dotnet ef migrations list

# إنشاء ترحيل جديد
dotnet ef migrations add MigrationName

# تطبيق الترحيلات
dotnet ef database update

# التراجع عن ترحيل
dotnet ef migrations remove
```

---

## 📚 المراجع الإضافية

- 📖 [توثيق .NET](https://docs.microsoft.com/dotnet)
- 🐳 [توثيق Docker](https://docs.docker.com)
- 🔍 [توثيق Elasticsearch](https://www.elastic.co/guide/en/elasticsearch/reference/current/index.html)
- 🐰 [توثيق RabbitMQ](https://www.rabbitmq.com/documentation.html)

---

## ❓ أسئلة شائعة

**س: هل يمكنني استخدام قاعدة بيانات مختلفة؟**
ج: نعم، عدّل `docker-compose.yml` و `.env` حسب احتياجاتك

**س: كيف أعطل جميع الخدمات؟**
ج: استخدم `docker-compose down`

**س: هل يمكنني تشغيل التطبيق بدون Docker؟**
ج: نعم، لكنك ستحتاج لتثبيت SQL Server و Redis و RabbitMQ يدويًا

**س: كيفية إعادة تعيين قاعدة البيانات؟**
ج: استخدم `docker-compose down -v` ثم `docker-compose up -d`

---

## 📞 الدعم

إذا واجهت مشاكل:
- 🐛 [أبلغ عن خطأ](https://github.com/Alteyeb12/Digital-platform-/issues)
- 💬 [شارك سؤالك](https://github.com/Alteyeb12/Digital-platform-/discussions)

---

## 🎉 تم الإعداد بنجاح!

بعد اتباع هذه الخطوات، يجب أن يكون لديك:
- ✅ جميع خدمات Docker تعمل
- ✅ قاعدة البيانات جاهزة
- ✅ التطبيق قابل للتشغيل

**استمتع بالتطوير!** 🚀

---

---

# English Version

[English version would go here - similar structure with English translations]

---

**Last Updated:** 2026-05-18
