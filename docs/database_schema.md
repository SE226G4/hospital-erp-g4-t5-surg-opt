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
