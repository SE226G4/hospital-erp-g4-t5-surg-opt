Database Schema - Module 5 (Surgical Optimization)(Lilas Hamadah & Raghad Brejawi)

1. Entity-Relationship Diagram (ERD)(lilas Hamadah)

الأداة المستخدمة: Draw.io
رابط تعديل المخطط على Draw.io: [اضغط هنا لفتح المخطط القابل للتعديل]
(https://app.diagrams.net/#G1yZqmGC88vFT4IEP5lTKCrpYUxhzKq57N#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
رابط تعديل مخطط الأصناف (Class Diagram): [
اضغط هنا لفتح المخطط القابل للتعديل](https://app.diagrams.net/#G1yZqmGC88vFT4IEP5lTKCrpYUxhzKq57N#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
2. Tables List & Fields

Table 1: Operating_Rooms (غرف العمليات)

يخزن بيانات غرف العمليات المتاحة وحالة تعقيمها.
PK - room_id (Integer) -> معرف الغرفة الفريد
room_number (Varchar) -> رقم الغرفة
status (Varchar) -> حالة الغرفة (متاحة، مشغولة، قيد التنظيف)
last_sterilization (Datetime) -> تاريخ ووقت آخر تعقيم
Table 2: Surgeries / SurgicalBookings (جدول العمليات والجدولة)

الجدول الأساسي لتنسيق المواعيد ومنع تضارب الجدولة.
PK - surgery_id (Integer) -> معرّف الحجز والعملية
FK - DigitalID (Varchar) -> معرف المريض الرقمي الموحد (مشترك مع موديول 1 القبول)
patient_name (Varchar) -> اسم المريض
surgery_type (Varchar) -> نوع العملية الجراحية
start_time (Datetime) -> وقت بدء العملية
end_time (Datetime) -> وقت انتهاء العملية (لحساب وقت التعقيم الإلزامي)
FK - room_id (Integer) -> الغرفة المحجوزة (References Operating_Rooms)
FK - bed_id (Integer) -> السرير المحجوز للمريض (مرتبط بموديول 3 الإقامة والأسرة)
status (Varchar) -> حالة العملية (مجدولة، قيد التنفيذ، منتهية)
Table 3: Sterilization_Logs (سجلات التعقيم الإلزامي)

يضمن تتبع وقت التعقيم الإلزامي بين العمليات الجراحية لمنع التلوث.
PK - log_id (Integer) -> معرّف السجل
FK - room_id (Integer) -> رقم الغرفة التي تم تعقيمها
start_time (Datetime) -> وقت بدء التعقيم
end_time (Datetime) -> وقت انتهاء التعقيم
Table 4: Surgery_Resources (الموارد والفريق الطبي الكامل)

يتحقق من توفر كامل الطاقم الطبي والأدوات الجاهزة قبل العملية.
PK - resource_id (Integer) -> معرّف المورد
FK - surgery_id (Integer) -> رقم العملية التابع لها
staff_id (Integer) -> الرقم الوظيفي لعضو الفريق الطبي
staff_role (Varchar) -> دور عضو الفريق (جراح، طبيب تخدير، ممرض)
equipment_needed (Varchar) -> الأدوات والمستلزمات الطبية المطلوبة
3. Relational Database Schema (الصيغة المنطقية الحرفية للـ Schema)

Operating_Rooms ( room_id (PK), room_number, status, last_sterilization )
SurgicalBookings ( surgery_id (PK), patient_name, surgery_type, start_time, end_time, status, DigitalID (FK), room_id (FK), bed_id (FK) )
Sterilization_Logs ( log_id (PK), start_time, end_time, room_id (FK) )
Surgery_Resources ( resource_id (PK), staff_id, staff_role, equipment_needed, surgery_id (FK) )
Section 1: Process Modeling & Workflow (Raghad Brejawi)

1.1 Surgery Scheduling Activity Diagram

رابط تعديل المخطط على Draw.io:[اضغط هنا لفتح المخطط القابل للتعديل هنا]
https://app.diagrams.net/?src=about#G1n_gHUHRdaoX0f3uAjbqCSA8GBcrKjpql#%7B%22pageId%22%3A%22neiKABBSU_3DcmXe3jSo%22%7D
Flow of Events (توصيف مسار الأحداث):

المسار الأساسي (Basic Flow):
يبدأ الجراح أو المجدوِل بإدخال بيانات المريض وتفاصيل الجراحة المطلوبة.
يقوم النظام بتشغيل دالة التحقق والجدولة Validate And Schedule Surgery().
يتم فحص توفر الطاقم الطبي والغرفة (AreStaff & OR Available)، ثم التحقق من وقت التعقيم الفاصل (Verify Buffer Time 30-40mins).
عند استيفاء الشروط، يتم تأكيد الحجز وتحويل الحالة إلى (Scheduled) وإرسال إشعارات للواجهات.
المسارات البديلة (Alternative Flows):
في حال عدم توفر الموارد، يطلب النظام من المستخدم تغيير الوقت أو الغرفة.
في حال عدم كفاية وقت التعقيم، يتم رفض الحجز مباشرة وتحويل الحالة إلى ملغي (Cancelled).
1.2 Relational Schema Integration (ERD)(Raghad Brejawi)

5. توثيق موديول جدولة العمليات الجراحية (رغد بريجاوي)

يغطي هذا القسم التصميم الهيكلي لقاعدة البيانات (ERD) وتدفق العمليات (Activity Diagram) الخاص بجدولة وحجز العمليات الجراحية، مع توضيح التحققات التلقائية ونقاط التكامل مع الموديولات الأخرى لمنع تكرار البيانات.
5.1 مخطط علاقات الكيانات (Entity-Relationship Diagram - ERD)

الأداة المستخدمة: Draw.io
رابط تعديل المخطط المباشر: اضغط هنا لفتح المخطط القابل للتعديل على Draw.io
عرض مخطط قاعدة البيانات (ERD):

!Entity Relationship Diagram
5.1.1 توصيف جداول قاعدة البيانات (Tables List)

اسم الجدولوصف الهدف من الجدولSurgicalBookingsالجدول الأساسي والمحوري للموديول، ويقوم بتخزين كافة بيانات حجوزات العمليات الجراحية، المواعيد، وحالة الحجز، ومعرفات الفريق الطبي المشرف.
5.1.2 نقاط التكامل والبيانات المشتركة (Shared Data & Integration Points)

بناءً على معايير الجودة ومنع التكرار المالي واللوجستي، يرتبط جدولنا بالجداول المركزية التالية عبر العلاقات (One-to-Many):
جدول المرضى Patients عبر المعرف PatientDigitalID (FK1):الجهة المشتركة: موديول القبول والترميز الطبي (Module 1) للتحقق من هوية المريض وملفه الصحي قبل الجراحة.
جدول الخدمات الطبية MedicalServices عبر المعرف ServiceID (FK2):الجهة المشتركة: موديول الفوترة والتأمين (Module 2) لاحتساب تكلفة العملية الجراحية وضمان التكامل المالي التلقائي.
جدول الأسرة Beds عبر المعرف BedID (FK3):الجهة المشتركة: موديول إدارة الأسرة والغرف (Module 1) لضمان حجز وتوفير سرير للمريض فور خروجه من غرفة العمليات.
جدول الغرف Rooms عبر المعرف RoomID (FK4):الجهة المشتركة: موديول إدارة الأسرة والعمليات للتحقق من جهوزية الغرفة وحجزها لوجستياً.
5.2 نمذجة العمليات ومخطط النشاط (Activity Diagram)

يوضح المخطط التالي دورة حياة طلب حجز العملية والتحققات البرمجية التي تجريها الدالة ValidateAndScheduleSurgery عبر الطبقات الثلاث (SurgicalScheduler, System, Notification System):
عرض مخطط النشاط (Activity Diagram):

!Surgical Scheduling Activity Diagram
5.2.1 جدول قواعد العمل وشروط التحقق (Business Rules)

معرف القاعدةاسم القاعدةوصف شرط التحقق البرمجيالإجراء البرمجي ورسالة الخطأBR_OR_01تضارب الموارديمنع حجز عمليتين جراحيتين في نفس غرفة العمليات (OR) وفي نفس الوقت تماماً."خطأ: غرفة العمليات المختارة محجوزة بالفعل لعملية أخرى في هذا الوقت."BR_OR_02توفر الطاقم الطبييجب أن يكون الطبيب الجراح وطبيب التخدير المختارين متاحين وغير مرتبطين بعملية أخرى."خطأ: الكادر الطبي المختار غير متاح لوجود تضارب في مواعيده الجراحية."BR_OR_03وقت التعقيم الإلزامييجب وجود فارق زمني (Buffer Time) لا يقل عن 30-45 دقيقة بين العمليات المتتالية في نفس الغرفة لتنظيفها وتعقيمها."خطأ: الوقت الفاصل غير كافٍ لإتمام عملية تعقيم الغرفة بين العمليات."BR_OR_04فحص ما قبل العمليةلا يمكن اعتماد حالة الحجز كـ (Scheduled) إلا بعد التحقق من اكتمال قائمة الفحوصات الأولية للمريض."فشل التحقق: الفحوصات الطبية الإلزامية للمريض أو موافقة طبيب التخدير غير مكتملة."
5.2.2 سيناريوهات الفحص البرمجي (Validation Test Cases)

السيناريو الأول: نجاح عملية الحجز (تدفق سليم - Valid Input)
الحالة: يطلب الجراح حجز غرفة العمليات (A) يوم السبت الساعة 10:00 صباحاً.
الشروط المسبقة: الغرفة شاغرة، الطاقم الطبي متاح، وقائمة فحوصات المريض (Pre-Op) مكتملة وموقعة.
استجابة النظام: تقوم الدالة بتمرير الحجز بنجاح، ويتم تخزين السجل في قاعدة البيانات بحالة Status = "Scheduled"، وتفعيل واجهات الإشعارات للموديولات الأخرى (الفواتير والأسرة).
السيناريو الثاني: فشل الحجز بسبب خرق وقت التعقيم (تدفق خاطئ - Invalid Input)
الحالة: محاولة حجز غرفة العمليات (A) لعملية ثانية تبدأ الساعة 11:15 صباحاً، علماً أن العملية السابقة تنتهي الساعة 11:00 صباحاً.
الشروط المسبقة: العملية الأولى تنتهي الساعة 11:00 تماماً (الفارق المتاح 15 دقيقة فقط).
استجابة النظام: يكتشف النظام خرق قاعدة التعقيم الإلزامية (BR_OR_03)، فيقوم بإلغاء العملية برمجياً فوراً وتغيير الحالة إلى "Cancelled"، ويظهر تنبيهاً للمستخدم برفض الحجز لعدم كفاية وقت التعقيم.
4. SQL DDL Commands (أكواد بناء قاعدة البيانات)(Lilas Hamadah)

-- 1. جدول غرف العملياتCREATE TABLE Operating_Rooms (
    room_id INT PRIMARY KEY AUTO_INCREMENT,
    room_number VARCHAR(50) NOT NULL UNIQUE,
    status VARCHAR(50) DEFAULT 'Available',
    last_sterilization DATETIME
);-- 2. جدول العمليات والجدولةCREATE TABLE SurgicalBookings (
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
);-- 3. جدول سجلات التعقيمCREATE TABLE Sterilization_Logs (
    log_id INT PRIMARY KEY AUTO_INCREMENT,
    room_id INT NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    FOREIGN KEY (room_id) REFERENCES Operating_Rooms(room_id) ON DELETE CASCADE
);-- 4. جدول الموارد والفريق الطبيCREATE TABLE Surgery_Resources (
    resource_id INT PRIMARY KEY AUTO_INCREMENT,
    surgery_id INT NOT NULL,
    staff_id INT NOT NULL,
    staff_role VARCHAR(50) NOT NULL,
    equipment_needed VARCHAR(255),
    FOREIGN KEY (surgery_id) REFERENCES SurgicalBookings(surgery_id) ON DELETE CASCADE
);




يم الإلزامية (BR_OR_03)، فيقوم بإلغاء العملية برمجياً فوراً وتغيير الحالة إلى "Cancelled"، ويظهر تنبيهاً للمستخدم برفض الحجز لعدم كفاية وقت التعقيم.

Sequence diagram$User Stories$Agile$Interface Logic(Maryam Alhamwi)

رابط للاطلاع على المخطط: https://drive.google.com/file/d/1Tp552sqs07dr1AhAjJiO3CbHDzHxVEiv/view?usp=sharing 1-User Stories 1.User Stories for Scheduling operation and control in time:​As a Surgeon,​I want to schedule a surgical operation for a patient through the UI,​So that the operating room and time slot are reserved efficiently.
​US2 (Conflict Prevention):​As an Operations Coordinator,​I want the system to automatically block any surgery scheduling if there is a time conflict for the selected operating room,
​So that we can prevent scheduling overlaps and delays.
​US3 (Mandatory Sterilization):​As a Surgeon,​I want the system to automatically include a mandatory sterilization time buffer after each surgery in the room,
​So that patient safety is ensured and the room is prepared for the next operation.
​2. User Stories for System Integration & Verification:​US4 (Resource Availability Check):​As an Operations Coordinator,​I want the system to verify the availability of the full medical team and the readiness of surgical tools before confirming the schedule,
​So that we ensure no operation starts with missing personnel or equipment.
​US5 (Bed Reservation - IPD Integration):​As an Operations Coordinator,​I want the system to check and secure a post-operative bed for the patient via the Inpatient & Bed Management system (IPD-BED),​So that we guarantee a place for the patient immediately after the surgery.
​US6 (Unified Patient File - ADM Integration):​As a Surgeon,​I want the system to automatically fetch the patient’s medical data and risk profile from the Admission & Medical Coding system (ADM-MC) using their National ID,​So that we rely on a unified record and prevent data duplication or identification errors.
2-Agile Methods: Feature 1: Surgery Scheduling & Conflict Prevention
Scenario 1: Successful Surgery Booking : Given the surgeon is logged into the Surgical Optimization system. And the Operating Room (OR 1) is vacant on Wednesday at 10:00 AM. When the surgeon enters the surgery details and clicks "Process Surgery Request". Then the system should successfully reserve the room and display a "Success Message".
Scenario 2: Room Time Conflict : Given the Operating Room (OR 1) is already reserved from 10:00 AM to 12:00 PM. When the coordinator tries to book another surgery in OR 1 at 11:00 AM. Then the system must reject the booking. And display an error message: "Time slot conflict detected".
Feature 2: Mandatory Sterilization Time Buffer
Scenario 1: Auto-inserting Sterilization Period : Given a surgery is being scheduled in OR 2 from 01:00 PM to 03:00 PM. When the surgery booking is confirmed. Then the system must automatically block the next 30 minutes (03:00 PM - 03:30 PM) for mandatory sterilization. And prevent any other bookings during this buffer.
Feature 3: Cross-Module Integration & Resource Verification
Scenario 1: Unified Patient File Verification - ADM Integration : Given the system is integrated with the Admission & Medical Coding system (ADM-MC). When entering a National ID for a patient whose profile or risk check is incomplete. Then the system must block the scheduling request. And prompt an error: "Incomplete Patient Risk Profile".

Scenario 2: Resource and Medical Team Availability Check : Given the coordinator is finalizing a surgery schedule. When the system verifies resources and finds any required medical staff unavailable OR surgical tools unready. Then the system must halt the confirmation. And display a notification specifying the missing personnel or equipment.
Scenario 3: Post-Op Bed Security - IPD Integration : Given the system is checking bed availability via the Inpatient & Bed Management system (IPD-BED). When there are no available or ready beds in the post-operative ward. Then the system should halt the booking. And notify the user that no post-op beds are available.
3- Interface Logic (منطق استجابة الواجهة - UI Logic)
Form Validation (التحقق من المدخلات):زر "إرسال الطلب" (Submit) يظل معطلاً (Disabled) ولا يمكن الضغط عليه حتى يتم ملء جميع الحقول الإلزامية (اسم المريض، المعرّف الرقمي، نوع العملية، ووقت البدء).
Loading State (حالة الانتظار):عند الضغط على زر الحجز، تتحول الواجهة لحالة الانتظار ويظهر مؤشر تحميل (Loading Spinner) مع تعطيل الزر مؤقتاً، وذلك لمنع المستخدم من تكرار الضغط وإرسال طلبين متطابقين أثناء معالجة البيانات في الـ SURG_OPT_CONTROLLER.
Dynamic Feedback (الاستجابة الديناميكية):في حالة النجاح: يختفي نموذج الإدخال وتظهر رسالة نجاح خضراء واضحة تعيد عرض تفاصيل الحجز المؤكد.
في حالة الفشل: يظل النموذج مفتوحاً مع إظهار رسالة خطأ حمراء منبثقة (مثل: "تضارب في الوقت: الغرفة بحاجة لـ 45 دقيقة تعقيم") لكي يستطيع المستخدم تعديل الوقت فوراً دون إعادة كتابة البيانات.
