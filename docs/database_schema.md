# Database Schema - Module 5 (Surgical Optimization)

## 1. Entity-Relationship Diagram (ERD)
* الأداة المستخدمة: Draw.io
* رابط تعديل المخطط على Draw.io: [اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/#)

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

## 3. Shared Data (Integration Points)
بناءً على متمتطلبات منع تكرار البيانات وضمان الجودة، يعتمد موديولنا على التكامل مع الفرق الأخرى كالتالي:

* **Shared Table: Patients (عبر المعرف DigitalID)**
  * **Shared With:** موديول القبول والترميز الطبي (Module 1) للتحقق من هوية المريض وملف المخاطر والحساسية قبل الجدولة.
  
* **Shared Table: MedicalServices (عبر حقل ربط التكاليف)**
  * **Shared With:** موديول الفوترة والتأمين (Module 2) لإرسال تكاليف العمليات والموارد تلقائياً لفاتورة المريض الموحدة.

* **Shared Bed Data (عبر المعرف bed_id)**
  * **Shared With:** موديول الإقامة وإدارة الأسرة (Module 3) لضمان توفر سرير جاهز للمريض فور انتهاء جراحته.
