# CONTRIBUTING.md

## 🤝 دليل المساهمة

شكراً لاهتمامك بالمساهمة في منصة Threat Intelligence! هذا الدليل يشرح كيفية المساهمة بشكل فعّال.

---

## 🎯 كيفية المساهمة

### 1️⃣ استنساخ المستودع

```bash
git clone https://github.com/Alteyeb12/Digital-platform-.git
cd Digital-platform-
```

### 2️⃣ إنشاء فرع جديد

```bash
# للميزات الجديدة
git checkout -b feature/your-feature-name

# للإصلاحات
git checkout -b fix/your-fix-name

# للتحسينات
git checkout -b improvement/your-improvement-name
```

### 3️⃣ القيام بالتغييرات

- اتبع معايير الكود الموضحة أدناه
- اكتب اختبارات للميزات الجديدة
- حدّث التوثيق

### 4️⃣ الالتزام بالتغييرات

```bash
git add .
git commit -m "Type: Description"

# أمثلة:
# git commit -m "feat: Add dark web scanning feature"
# git commit -m "fix: Resolve SQL connection timeout"
# git commit -m "docs: Update API documentation"
# git commit -m "test: Add unit tests for vulnerability scanner"
```

### 5️⃣ دفع التغييرات

```bash
git push origin your-branch-name
```

### 6️⃣ إنشاء Pull Request

1. اذهب إلى المستودع على GitHub
2. اضغط على "New Pull Request"
3. اختر فرعك
4. املأ نموذج الطلب
5. انتظر المراجعة

---

## 📝 معايير الكود

### C# Code Style

```csharp
// استخدم الأسماء الواضحة
public async Task<VulnerabilityReport> ScanApplicationAsync(string targetUrl)
{
    // استخدم async/await
    var results = await _scanService.PerformScanAsync(targetUrl);
    
    // أضف معالجة الأخطاء
    if (results == null)
        throw new InvalidOperationException("Scan failed");
    
    return results;
}

// استخدم Nullable Reference Types
public string? GetConfigValue(string key)
{
    return _configuration[key];
}

// استخدم Records للبيانات البسيطة
public record VulnerabilityData(string Id, string Name, int Severity);
```

### Naming Conventions

- **الفئات**: PascalCase (`ThreatAnalyzer`)
- **الخصائص**: PascalCase (`PublicProperty`)
- **الحقول الخاصة**: _camelCase (`_privateField`)
- **الثوابت**: UPPER_CASE (`const int MAX_RETRIES = 3`)
- **المتغيرات المحلية**: camelCase (`localVariable`)

---

## 🧪 الاختبارات

### كتابة الاختبارات

```csharp
[TestFixture]
public class ThreatAnalyzerTests
{
    private ThreatAnalyzer _analyzer;
    
    [SetUp]
    public void Setup()
    {
        _analyzer = new ThreatAnalyzer();
    }
    
    [Test]
    public async Task AnalyzeThreat_WithValidData_ReturnsResult()
    {
        // Arrange
        var threatData = new ThreatData { Severity = 10 };
        
        // Act
        var result = await _analyzer.AnalyzeAsync(threatData);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Score, Is.GreaterThan(0));
    }
}
```

### تشغيل الاختبارات

```bash
dotnet test
```

---

## 📚 التوثيق

### XML Documentation

```csharp
/// <summary>
/// تحليل التهديد وحساب درجة المخاطر
/// </summary>
/// <param name="threat">بيانات التهديد المراد تحليلها</param>
/// <returns>نتيجة التحليل مع درجة المخاطر</returns>
/// <exception cref="ArgumentNullException">إذا كان التهديد فارغًا</exception>
public async Task<ThreatAnalysisResult> AnalyzeThreatAsync(Threat threat)
{
    // ...
}
```

---

## 🔍 عملية المراجعة

### قبل الالتزام

- ✅ قم بتشغيل `dotnet build`
- ✅ قم بتشغيل `dotnet test`
- ✅ تحقق من لا توجد أخطاء إملائية
- ✅ تحديث الملفات المرتبطة

### أثناء المراجعة

- قد يطلب المراجعون تغييرات
- اتبع التعليقات بصبر
- لا تتردد في طرح الأسئلة

---

## 🐛 الإبلاغ عن الأخطاء

### إنشاء Issue

1. اذهب إلى "Issues"
2. اضغط "New Issue"
3. اختر "Bug report"
4. ملء النموذج:

```markdown
**الوصف:**
شرح واضح للمشكلة

**خطوات إعادة الإنتاج:**
1. قم بـ...
2. ثم...
3. لاحظت...

**السلوك المتوقع:**
ما يجب أن يحدث

**السلوك الفعلي:**
ما يحدث فعليًا

**البيئة:**
- نظام التشغيل: ...
- إصدار .NET: ...
```

---

## 🎨 طلب ميزة جديدة

### إنشاء Feature Request

```markdown
**الوصف:**
شرح الميزة المطلوبة

**المشكلة التي تحل:**
سياق المشكلة

**الحل المقترح:**
كيفية تطبيق الحل

**بدائل:**
حلول بديلة إن وجدت
```

---

## 📦 أنواع المساهمات المرحب بها

- 🐛 إصلاح الأخطاء
- ✨ ميزات جديدة
- 📚 تحسين التوثيق
- 🧪 إضافة الاختبارات
- 🔍 تحسينات الأداء
- 🔐 تحسينات الأمان
- 🎨 تحسينات الواجهة

---

## 📞 التواصل

- 💬 [Discussions](https://github.com/Alteyeb12/Digital-platform-/discussions)
- 🐛 [Issues](https://github.com/Alteyeb12/Digital-platform-/issues)
- 📧 [البريد الإلكتروني](mailto:altybalnhlawy7@gmail.com)

---

## 📜 الترخيص

بالمساهمة، فإنك توافق على أن تكون مساهماتك مرخصة تحت نفس الترخيص المستخدم في المشروع.

**شكراً لمساهمتك!** 🙏
