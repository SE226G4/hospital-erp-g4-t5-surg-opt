# Database Schema - Module 5 (Surgical Optimization)
**Designers:** Lilas Hamadah & Raghad Brejawi

## 1. Entity-Relationship Diagram (ERD) & Class Diagram
* **الأداة المستخدمة:** Draw.io
* **رابط تعديل المخططات على Draw.io:** [اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/#G1yZqmGC88vFT4IEP5lTKCrpYUxhzKq57N#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
* **عرض مخطط قاعدة البيانات:**
![Entity Relationship Diagram](../erd-diagram.png)

## 2. Relational Database Schema (الصيغة المنطقية الحرفية)
* Operating_Rooms ( `room_id` (PK), `room_number`, `status`, `last_sterilization` )
* SurgicalBookings ( `surgery_id` (PK), `patient_name`, `surgery_type`, `start_time`, `end_time`, `status`, `PatientDigitalID` (FK), `room_id` (FK), `bed_id` (FK), `ServiceID` (FK) )
* Sterilization_Logs ( `log_id` (PK), `start_time`, `end_time`, `room_id` (FK) )
* Surgery_Resources ( `resource_id` (PK), `staff_id`, `staff_role`, `equipment_needed`, `surgery_id` (FK) )

## 3. Shared Data & Integration Points (نقاط منع التكرار)
بناءً على معايير الجودة ومنع التكرار اللوجستي والمالي في الـ ERP، يرتبط جدولنا بالجداول المركزية التالية:
1. **جدول المرضى (Patients) عبر المعرف `PatientDigitalID (FK)`:** جهة التكامل هي موديول القبول والترميز الطبي (Module 1) للتحقق من هوية المريض وملفه الصحي قبل الجراحة.
2. **جدول الخدمات الطبية (MedicalServices) عبر المعرف `ServiceID (FK)`:** جهة التكامل هي موديول الفوترة والتأمين (Module 2) لاحتساب تكلفة العملية الجراحية وضمان التكامل المالي التلقائي.
3. **جدول الأسرة (Beds) عبر المعرف `bed_id (FK)`:** جهة التكامل هي موديول إدارة الأسرة (Module 3) لضمان توفير سرير للمريض فور خروجه من العمليات.

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
    PatientDigitalID VARCHAR(50) NOT NULL, 
    patient_name VARCHAR(100) NOT NULL,
    surgery_type VARCHAR(100) NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    room_id INT,
    bed_id INT, 
    ServiceID INT,
    status VARCHAR(50) DEFAULT 'Scheduled',
    FOREIGN KEY (room_id) REFERENCES Operating_Rooms(room_id) ON DELETE SET NULL,
    CONSTRAINT chk_surgery_time CHECK (end_time > start_time)
);

-- 3. جدول سجلات التعقيم الإلزامي
CREATE TABLE Sterilization_Logs (
    log_id INT PRIMARY KEY AUTO_INCREMENT,
    room_id INT NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    FOREIGN KEY (room_id) REFERENCES Operating_Rooms(room_id) ON DELETE CASCADE
);

-- 4. جدول الموارد والفريق الطبي الكامل
CREATE TABLE Surgery_Resources (
    resource_id INT PRIMARY KEY AUTO_INCREMENT,
    surgery_id INT NOT NULL,
    staff_id INT NOT NULL,
    staff_role VARCHAR(50) NOT NULL,
    equipment_needed VARCHAR(255),
    FOREIGN KEY (surgery_id) REFERENCES SurgicalBookings(surgery_id) ON DELETE CASCADE
);
