# Database Schema - Module 5 (Surgical Optimization)

## 1. Entity-Relationship Diagram (ERD)
* الأداة المستخدمة: Draw.io
* رابط تعديل المخطط على Draw.io: [اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/#G1yZqmGC88vFT4IEP5lTKCrpYUxhzKq57N#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
* **رابط تعديل مخطط الأصناف (Class Diagram):**  [
* اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/#G1yZqmGC88vFT4IEP5lTKCrpYUxhzKq57N#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
---

## 2. Tables List & Fields

### Table 1: Operating_Rooms (غرف العمليات)
يخزن بيانات غرف العمليات المتاحة وحالة تعقيمها.
* **PK - room_id** (Integer) -> معرف الغرفة الفريد
* room_number (Varchar) -> رقم الغرفة
* status (Varchar) -> حالة الغرفة (متاحة، مشغولة، قيد التنظيف)
* last_sterilization (Datetime) -> تاريخ ووقت آخر تعقيم

### Table 2: Surgeries / SurgicalBookings (جدول العمليات والجدولة)
الجدول الأساسي لتنسيق المواعيد ومنع تضارب الجدولة.
* **PK - surgery_id** (Integer) -> معرّف الحجز والعملية
* **FK - DigitalID** (Varchar) -> معرف المريض الرقمي الموحد (مشترك مع موديول 1 القبول)
* patient_name (Varchar) -> اسم المريض
* surgery_type (Varchar) -> نوع العملية الجراحية
* start_time (Datetime) -> وقت بدء العملية
* end_time (Datetime) -> وقت انتهاء العملية (لحساب وقت التعقيم الإلزامي)
* **FK - room_id** (Integer) -> الغرفة المحجوزة (References Operating_Rooms)
* **FK - bed_id** (Integer) -> السرير المحجوز للمريض (مرتبط بموديول 3 الإقامة والأسرة)
* status (Varchar) -> حالة العملية (مجدولة، قيد التنفيذ، منتهية)

### Table 3: Sterilization_Logs (سجلات التعقيم الإلزامي)
يضمن تتبع وقت التعقيم الإلزامي بين العمليات الجراحية لمنع التلوث.
* **PK - log_id** (Integer) -> معرّف السجل
* **FK - room_id** (Integer) -> رقم الغرفة التي تم تعقيمها
* start_time (Datetime) -> وقت بدء التعقيم
* end_time (Datetime) -> وقت انتهاء التعقيم

### Table 4: Surgery_Resources (الموارد والفريق الطبي الكامل)
يتحقق من توفر كامل الطاقم الطبي والأدوات الجاهزة قبل العملية.
* **PK - resource_id** (Integer) -> معرّف المورد
* **FK - surgery_id** (Integer) -> رقم العملية التابع لها
* staff_id (Integer) -> الرقم الوظيفي لعضو الفريق الطبي
* staff_role (Varchar) -> دور عضو الفريق (جراح، طبيب تخدير، ممرض)
* equipment_needed (Varchar) -> الأدوات والمستلزمات الطبية المطلوبة

---

## 3. Relational Database Schema (الصيغة المنطقية الحرفية للـ Schema)

* `Operating_Rooms (` **room_id (PK)**, room_number, status, last_sterilization `)`
* `SurgicalBookings (` **surgery_id (PK)**, patient_name, surgery_type, start_time, end_time, status, *DigitalID (FK)*, *room_id (FK)*, *bed_id (FK)* `)`
* `Sterilization_Logs (` **log_id (PK)**, start_time, end_time, *room_id (FK)* `)`
* `Surgery_Resources (` **resource_id (PK)**, staff_id, staff_role, equipment_needed, *surgery_id (FK)* `)`

---

# Section 1: Process Modeling & Workflow (Raghad Brejawi)

## 1.1 Surgery Scheduling Activity Diagram
* رابط تعديل المخطط على Draw.io: [اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/?src=about#G1n_gHUHRdaoX0f3uAjbqCSA8GBcrKjpql#%7B%22pageId%22%3A%22neiKABBSU_3DcmXe3jSo%22%7D)

![Activity Diagram](activity_diagram.png)

### Flow of Events (توصيف مسار الأحداث):
1. المسار الأساسي (Basic Flow):
   * يبدأ الجراح أو المجدوِل بإدخال بيانات المريض وتفاصيل الجراحة المطلوبة.
   * يقوم النظام بتشغيل دالة التحقق والجدولة Validate And Schedule Surgery().
   * يتم فحص توفر الطاقم الطبي والغرفة (AreStaff & OR Available)، ثم التحقق من وقت التعقيم الفاصل (Verify Buffer Time 30-40mins).
   * عند استيفاء الشروط، يتم تأكيد الحجز وتحويل الحالة إلى (Scheduled) وإرسال إشعارات للواجهات.

2. المسارات البديلة (Alternative Flows):
   * في حال عدم توفر الموارد، يطلب النظام من المستخدم تغيير الوقت أو الغرفة.
   * في حال عدم كفاية وقت التعقيم، يتم رفض الحجز مباشرة وتحويل الحالة إلى ملغي (Cancelled).

## 1.2 Relational Schema Integration (ERD)
![Process ERD](process_erd.png)

---

## 5. توثيق موديول جدولة العمليات الجراحية (رغد بريجاوي)

يغطي هذا القسم التصميم الهيكلي لقاعدة البيانات (ERD) وتدفق العمليات (Activity Diagram) الخاص بجدولة وحجز العمليات الجراحية، مع توضيح التحققات التلقائية ونقاط التكامل مع الموديولات الأخرى لمنع تكرار البيانات.

---

### 5.1 مخطط علاقات الكيانات (Entity-Relationship Diagram - ERD)

* الأداة المستخدمة: Draw.io
* رابط تعديل المخطط المباشر: اضغط هنا لفتح المخطط القابل للتعديل على Draw.io

#### عرض مخطط قاعدة البيانات (ERD):
!Entity Relationship Diagram

#### 5.1.1 توصيف جداول قاعدة البيانات (Tables List)

| اسم الجدول | وصف الهدف من الجدول |
| :--- | :--- |
| SurgicalBookings | الجدول الأساسي والمحوري للموديول، ويقوم بتخزين كافة بيانات حجوزات العمليات الجراحية، المواعيد، وحالة الحجز، ومعرفات الفريق الطبي المشرف. |

#### 5.1.2 نقاط التكامل والبيانات المشتركة (Shared Data & Integration Points)
بناءً على معايير الجودة ومنع التكرار المالي واللوجستي، يرتبط جدولنا بالجداول المركزية التالية عبر العلاقات (One-to-Many):

* جدول المرضى Patients عبر المعرف PatientDigitalID (FK1):
  * الجهة المشتركة: موديول القبول والترميز الطبي (Module 1) للتحقق من هوية المريض وملفه الصحي قبل الجراحة.
* جدول الخدمات الطبية MedicalServices عبر المعرف ServiceID (FK2):
  * الجهة المشتركة: موديول الفوترة والتأمين (Module 2) لاحتساب تكلفة العملية الجراحية وضمان التكامل المالي التلقائي.
* جدول الأسرة Beds عبر المعرف BedID (FK3):
  * الجهة المشتركة: موديول إدارة الأسرة والغرف (Module 1) لضمان حجز وتوفير سرير للمريض فور خروجه من غرفة العمليات.
* جدول الغرف Rooms عبر المعرف RoomID (FK4):
  * الجهة المشتركة: موديول إدارة الأسرة والعمليات للتحقق من جهوزية الغرفة وحجزها لوجستياً.

---

### 5.2 نمذجة العمليات ومخطط النشاط (Activity Diagram)

يوضح المخطط التالي دورة حياة طلب حجز العملية والتحققات البرمجية التي تجريها الدالة ValidateAndScheduleSurgery عبر الطبقات الثلاث (SurgicalScheduler, System, Notification System):

#### عرض مخطط النشاط (Activity Diagram):
!Surgical Scheduling Activity Diagram

#### 5.2.1 جدول قواعد العمل وشروط التحقق (Business Rules)

| معرف القاعدة | اسم القاعدة | وصف شرط التحقق البرمجي | الإجراء البرمجي ورسالة الخطأ |
| :--- | :--- | :--- | :--- |
| BR_OR_01 | تضارب الموارد | يمنع حجز عمليتين جراحيتين في نفس غرفة العمليات (OR) وفي نفس الوقت تماماً. | "خطأ: غرفة العمليات المختارة محجوزة بالفعل لعملية أخرى في هذا الوقت." |
| BR_OR_02 | توفر الطاقم الطبي | يجب أن يكون الطبيب الجراح وطبيب التخدير المختارين متاحين وغير مرتبطين بعملية أخرى. | "خطأ: الكادر الطبي المختار غير متاح لوجود تضارب في مواعيده الجراحية." |
| BR_OR_03 | وقت التعقيم الإلزامي | يجب وجود فارق زمني (Buffer Time) لا يقل عن 30-45 دقيقة بين العمليات المتتالية في نفس الغرفة لتنظيفها وتعقيمها. | "خطأ: الوقت الفاصل غير كافٍ لإتمام عملية تعقيم الغرفة بين العمليات." |
| BR_OR_04 | فحص ما قبل العملية | لا يمكن اعتماد حالة الحجز كـ (Scheduled) إلا بعد التحقق من اكتمال قائمة الفحوصات الأولية للمريض. | "فشل التحقق: الفحوصات الطبية الإلزامية للمريض أو موافقة طبيب التخدير غير مكتملة." |

#### 5.2.2 سيناريوهات الفحص البرمجي (Validation Test Cases)

* السيناريو الأول: نجاح عملية الحجز (تدفق سليم - Valid Input)
  * الحالة: يطلب الجراح حجز غرفة العمليات (A) يوم السبت الساعة 10:00 صباحاً.
  * الشروط المسبقة: الغرفة شاغرة، الطاقم الطبي متاح، وقائمة فحوصات المريض (Pre-Op) مكتملة وموقعة.
  * استجابة النظام: تقوم الدالة بتمرير الحجز بنجاح، ويتم تخزين السجل في قاعدة البيانات بحالة Status = "Scheduled"، وتفعيل واجهات الإشعارات للموديولات الأخرى (الفواتير والأسرة).

* السيناريو الثاني: فشل الحجز بسبب خرق وقت التعقيم (تدفق خاطئ - Invalid Input)
  * الحالة: محاولة حجز غرفة العمليات (A) لعملية ثانية تبدأ الساعة 11:15 صباحاً، علماً أن العملية السابقة تنتهي الساعة 11:00 صباحاً.
 
  
* الشروط المسبقة: العملية الأولى تنتهي الساعة 11:00 تماماً (الفارق المتاح 15 دقيقة فقط).
  * استجابة النظام: يكتشف النظام خرق قاعدة التعقيم الإلزامية (BR_OR_03)، فيقوم بإلغاء العملية برمجياً فوراً وتغيير الحالة إلى "Cancelled"، ويظهر تنبيهاً للمستخدم برفض الحجز لعدم كفاية وقت التعقيم.

## 4. SQL DDL Commands (أكواد بناء قاعدة البيانات)

```sql
-- 1. جدول غرف العمليات
CREATE TABLE Operating_Rooms (
    room_id INT PRIMARY KEY AUTO_INCREMENT,
    room_number VARCHAR(50) NOT NULL UNIQUE,
    status VARCHAR(50) DEFAULT 'Available',
    last_sterilization DATETIME
);

-- 2. جدول العمليات والجدولة
CREATE TABLE SurgicalBookings (
    surgery_id INT PRIMARY KEY AUTO_INCREMENT,
    DigitalID VARCHAR(50) NOT NULL, 
    patient_name VARCHAR(100) NOT NULL,
    surgery_type VARCHAR(100) NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    room_id INT,
    bed_id INT, 
    status VARCHAR(50) DEFAULT 'Scheduled',
    FOREIGN KEY (room_id) REFERENCES Operating_Rooms(room_id) ON DELETE SET NULL,
    CONSTRAINT chk_surgery_time CHECK (end_time > start_time)
);

-- 3. جدول سجلات التعقيم
CREATE TABLE Sterilization_Logs (
    log_id INT PRIMARY KEY AUTO_INCREMENT,
    room_id INT NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    FOREIGN KEY (room_id) REFERENCES Operating_Rooms(room_id) ON DELETE CASCADE
);

-- 4. جدول الموارد والفريق الطبي
CREATE TABLE Surgery_Resources (
    resource_id INT PRIMARY KEY AUTO_INCREMENT,
    surgery_id INT NOT NULL,
    staff_id INT NOT NULL,
    staff_role VARCHAR(50) NOT NULL,
    equipment_needed VARCHAR(255),
    FOREIGN KEY (surgery_id) REFERENCES SurgicalBookings(surgery_id) ON DELETE CASCADE
);




يم الإلزامية (BR_OR_03)، فيقوم بإلغاء العملية برمجياً فوراً وتغيير الحالة إلى "Cancelled"، ويظهر تنبيهاً للمستخدم برفض الحجز لعدم كفاية وقت التعقيم.
