# Software Requirements Specification (SRS) - Module 5 (Surgical Optimization)
**Hospital ERP System | Module Code: SURG-05 (Group: G4 | Team: T5)**
**Team Leader:** Elaf Mallohi

## 1. Module Overview
هذا الموديول مسؤول عن إدارة وتحسين غرف العمليات الجراحية؛ حيث يقوم بجدولة العمليات، ومنع تضارب المواعيد، والتحقق من جاهزية الموارد (الأطباء، الأسرّة، والأدوات)، بالإضافة إلى إدارة أوقات التعقيم الإلزامية لضمان سلامة المرضى.

---

## 2. Use Case Specification
* **Primary Actor:** Operating Surgeon / Surgical Schedule Manager
* **Pre-conditions:** أن يكون المريض مسجلاً بشكل قانوني في المستشفى ويمتلك رقماً تعريفياً صالحاً (PatientDigitalID).

### 2.1 Flow of Events (التدفق الأساسي)
1. يقوم المستخدم بإدخال تفاصيل الجراحة، وتحديد الغرفة والوقت المقترح.
2. يستعلم النظام آلياً عن ملف المخاطر الصحية للمريض من نظام ADM-MC (Module 1).
3. يرسل النظام طلباً لنظام الأسرة (Module 3) لتأمين سرير إقامة لما بعد الجراحة.
4. يحسب النظام وقت التعقيم المطلق بناءً على نوع العملية ويحظره في الجدول لمنع التداخل.
5. يتحقق النظام من عدم وجود تضارب في مواعيد الطاقم والأدوات.
6. يتم تأكيد الموعد النهائي وحفظه.

---

## 3. Functional Requirements (FRs)

### A. Surgery Scheduling Management
* **FR-1:** يجب على النظام توفير واجهة رسومية تتيح للموظف المخول إدخال طلب جراحي جديد يشمل (رقم المريض الرقمي، غرفة العمليات المستهدفة، التاريخ، والوقت المقترح).
* **FR-2:** يجب على النظام رفض ومنع حفظ أي حجز جراحي في قاعدة البيانات المركزية ما لم يتم التحقق بنجاح من كافة القيود اللوجستية والطبية الإجبارية.

### B. Health Risk Profile Verification (Module 1 Integration)
* **FR-3:** يجب على النظام بناء اتصال برمي آمن مع نظام القبول والترميز الطبي لقراءة الرموز الطبية والتشخيصية المسجلة للمريض بشكل مؤتمت.
* **FR-4:** يجب على النظام تحليل ملف المخاطر الصحية المسترجع للتأكد من ملاءمة المريض طبياً لإجراء الجراحة وعدم وجود موانع حرجة.

### C. Inpatient Department Bed Management (Module 3 Integration)
* **FR-5:** يجب على النظام إرسال استعلام فوري لنظام إدارة أسرة الأقسام الداخلية للتحقق من وجود سرير شاغر ومجهز لاستقبال المريض فور انتهاء جراحته.
* **FR-6:** يجب على النظام إرسال أمر حجز مؤكد وتخصيص السرير وربطه بملف العملية الجراحية لضمان اللوجستيات الطبية.

### D. Mandatory Sterilization Blocking
* **FR-7:** يجب على النظام احتساب وقت التعقيم والتطهير الإلزامي لغرفة العمليات بشكل ديناميكي بناءً على نوع العملية الجراحية المسجلة برمزها الطبي.
* **FR-8:** يجب على النظام حظر (Block) الغرفة تماماً في جدول المواعيد خلال نافذة التعقيم المحسوبة لمنع أي تداخل أو ازدواجية في الحجوزات.

### E. Resource Readiness Verification
* **FR-9:** يجب على النظام إجراء فحص تضارب المواعيد للتأكد من أن الطاقم الطبي المحدد (الجراحين، أطباء التخدير، وممرضي الغرفة) متاحون بالكامل وغير مرتبطين بعمليات أخرى في نفس الوقت.
* **FR-10:** يجب على النظام التحقق من جاهزية وتوفر الأدوات والأجهزة الطبية المعقمة والمطلوبة لطبيعة هذه الجراحة المحددة قبل إعطاء أمر التأكيد النهائي.

---

## 4. Process Modeling & Workflow (Raghad Brejawi)
* **رابط تعديل الـ Activity Diagram على Draw.io:** [اضغط هنا لفتح المخطط](https://app.diagrams.net/?src=about#G1n_gHUHRdaoX0f3uAjbqCSA8GBcrKjpql)
* **عرض مخطط النشاط البنيوي:**
![Surgical Scheduling Activity Diagram](../activity-diagram.png)

### 4.1 Business Rules & Verification Logic
* **BR_OR_01 (تضارب الموارد):** يمنع حجز عمليتين جراحيتين في نفس غرفة العمليات وفي نفس الوقت تماماً.
* **BR_OR_02 (توفر الطاقم الطبي):** يجب أن يكون الطبيب الجراح وطبيب التخدير المختارين متاحين وغير مرتبطين بعملية أخرى.
* **BR_OR_03 (وقت التعقيم الإلزامي):** يجب وجود فارق زمني (Buffer Time) لا يقل عن 30-45 دقيقة بين العمليات المتتالية في نفس الغرفة لتنظيفها وتعقيمها.
* **BR_OR_04 (فحص ما قبل العملية):** لا يمكن اعتماد حالة الحجز كـ (Scheduled) إلا بعد التحقق من اكتمال قائمة الفحوصات الأولية للمريض.

### 4.2 Validation Test Cases (سيناريوهات الفحص البرمجي)
* **السيناريو الأول: نجاح عملية الحجز (تدفق سليم - Valid Input)**
  * *الحالة:* طلب حجز غرفة العمليات (A) يوم السبت الساعة 10:00 صباحاً.
  * *الاستجابة:* تمرير الحجز بنجاح وتخزينه بحالة `Status = "Scheduled"` وتفعيل واجهات الإشعارات.
* **السيناريو الثاني: فشل الحجز بسبب خرق وقت التعقيم (تدفق خاطئ - Invalid Input)**
  * *الحالة:* محاولة حجز الغرفة (A) لعملية ثانية تبدأ 11:15 صباحاً علماً أن العملية السابقة تنتهي 11:00 صباحاً (الفارق 15 دقيقة فقط).
  * *الاستجابة:* يكتشف النظام خرق قاعدة التعقيم الإلزامية (BR_OR_03)، فيقوم بإلغاء العملية برمجياً فوراً وتغيير الحالة إلى `Cancelled` مع إظهار تنبيه برفض الحجز.

---

## 5.Sequence diagram $ Agile$ User Stories & Interface Logic (Maryam Alhamwi)

رابط للاطلاع على المخطط: https://drive.google.com/file/d/1Tp552sqs07dr1AhAjJiO3CbHDzHxVEiv/view?usp=sharing

1-User Stories
1.User Stories for Scheduling operation and control in time:
​As a Surgeon,
​I want to schedule a surgical operation for a patient through the UI,
​So that the operating room and time slot are reserved efficiently.

​US2 (Conflict Prevention):
​As an Operations Coordinator,
​I want the system to automatically block any surgery scheduling if there is a time conflict for the selected operating room,  
​So that we can prevent scheduling overlaps and delays.

​US3 (Mandatory Sterilization):
​As a Surgeon,
​I want the system to automatically include a mandatory sterilization time buffer after each surgery in the room,  
​So that patient safety is ensured and the room is prepared for the next operation.

​2. User Stories for System Integration & Verification:
​US4 (Resource Availability Check):
​As an Operations Coordinator,
​I want the system to verify the availability of the full medical team and the readiness of surgical tools before confirming the schedule,  
​So that we ensure no operation starts with missing personnel or equipment.

​US5 (Bed Reservation - IPD Integration):
​As an Operations Coordinator,
​I want the system to check and secure a post-operative bed for the patient via the Inpatient & Bed Management system (IPD-BED),
​So that we guarantee a place for the patient immediately after the surgery.

​US6 (Unified Patient File - ADM Integration):
​As a Surgeon,
​I want the system to automatically fetch the patient’s medical data and risk profile from the Admission & Medical Coding system (ADM-MC) using their National ID,
​So that we rely on a unified record and prevent data duplication or identification errors.

2-Agile Methods:
 Feature 1: Surgery Scheduling & Conflict Prevention
 * Scenario 1: Successful Surgery Booking :
    Given the surgeon is logged into the Surgical Optimization system.
    And the Operating Room (OR 1) is vacant on Wednesday at 10:00 AM.
    When the surgeon enters the surgery details and clicks "Process Surgery Request".
    Then the system should successfully reserve the room and display a "Success Message".
* Scenario 2: Room Time Conflict :
    Given the Operating Room (OR 1) is already reserved from 10:00 AM to 12:00 PM.
    When the coordinator tries to book another surgery in OR 1 at 11:00 AM.
    Then the system must reject the booking.
    And display an error message: "Time slot conflict detected".

 Feature 2: Mandatory Sterilization Time Buffer
 * Scenario 1: Auto-inserting Sterilization Period :
    Given a surgery is being scheduled in OR 2 from 01:00 PM to 03:00 PM.
    When the surgery booking is confirmed.
    Then the system must automatically block the next 30 minutes (03:00 PM - 03:30 PM) for mandatory sterilization.
    And prevent any other bookings during this buffer.

Feature 3: Cross-Module Integration & Resource Verification
 * Scenario 1: Unified Patient File Verification - ADM Integration :
    Given the system is integrated with the Admission & Medical Coding system (ADM-MC).
    When entering a National ID for a patient whose profile or risk check is incomplete.
    Then the system must block the scheduling request.
    And prompt an error: "Incomplete Patient Risk Profile".

* Scenario 2: Resource and Medical Team Availability Check :
    Given the coordinator is finalizing a surgery schedule.
    When the system verifies resources and finds any required medical staff unavailable OR surgical tools unready.
    Then the system must halt the confirmation.
    And display a notification specifying the missing personnel or equipment.

* Scenario 3: Post-Op Bed Security - IPD Integration :
* Given the system is checking bed availability via the Inpatient & Bed Management system (IPD-BED).
    When there are no available or ready beds in the post-operative ward.
    Then the system should halt the booking.
    And notify the user that no post-op beds are available.


 3- Interface Logic (منطق استجابة الواجهة - UI Logic)

* Form Validation (التحقق من المدخلات):
  * زر "إرسال الطلب" (Submit) يظل معطلاً (Disabled) ولا يمكن الضغط عليه حتى يتم ملء جميع الحقول الإلزامية (اسم المريض، المعرّف الرقمي، نوع العملية، ووقت البدء).
* Loading State (حالة الانتظار):
  * عند الضغط على زر الحجز، تتحول الواجهة لحالة الانتظار ويظهر مؤشر تحميل (Loading Spinner) مع تعطيل الزر مؤقتاً، وذلك لمنع المستخدم من تكرار الضغط وإرسال طلبين متطابقين أثناء معالجة البيانات في الـ SURG_OPT_CONTROLLER.
* Dynamic Feedback (الاستجابة الديناميكية):
  * في حالة النجاح: يختفي نموذج الإدخال وتظهر رسالة نجاح خضراء واضحة تعيد عرض تفاصيل الحجز المؤكد.
  * في حالة الفشل: يظل النموذج مفتوحاً مع إظهار رسالة خطأ حمراء منبثقة (مثل: "تضارب في الوقت: الغرفة بحاجة لـ 45 دقيقة تعقيم") لكي يستطيع المستخدم تعديل الوقت فوراً دون إعادة كتابة البيانات.

---

## 6. Database Schema & Data Design (Lilas Hammada)
يغطي هذا القسم التصميم البنيوي والهندسي لموديول العمليات الجراحية (Module 5)، ويستعرض المخططات الأساسية التي تم تصميمها لضمان تكامل البيانات ومنع التضارب.

### 6.1 Database Architectural Artifacts (مخططات هندسة البيانات)
لضمان الفهم العميق لبنية قاعدة البيانات والمنطق البرمجي للموديول، تم إدراج الروابط والمخططات التالية:

* **رابط مخطط الـ Class Diagram على Draw.io:** [اضغط هنا لفتح المخطط البنيوي القابل للتعديل](https://app.diagrams.net/#G1TsQq9H9108A8VaF1gEA2z7dOgzhyVe7-#%7B%22pageId%22%3A%22RrjWv7MLcXQG55NYfEwk%22%7D)
  * *الهدف:* يوضح الكلاسات البرمجية، الخصائص (Attributes)، والأساليب (Methods) لعملية الجدولة والتعقيم وتفادي التضارب.

* **رابط مخطط الـ ERD على Draw.io:** [اضغط هنا لفتح مخطط الكيانات القابل للتعديل](https://app.diagrams.net/#G1ZOGdbe-M09ri5jk6DUtDgTZTZPSIE1VX#%7B%22pageId%22%3A%22wWFlRW2iCIbDgGKqphHK%22%7D)
  * *الهدف:* يوضح الكيانات (Entities) الأساسية مثل جدول الحجوزات، والمفاتيح الأساسية والأجنبية التي تربط الموديول مع جداول المرضى، الأسرة، والفوترة.

---
 ## 5. Module Integration & Interfaces (Asmaa)
يوضح هذا القسم هندسة الربط المشترك بين موديول **تحسين الجراحة (Surgical Optimization - Module 5)** والموديولات الأخرى في نظام الـ ERP للمستشفى، لضمان تدفق البيانات المؤتمت ومنع التكرار أو التضارب الهندسي.

* **رابط مخطط المكونات والتكامل المشترك (Component Diagram) على Draw.io:** [اضغط هنا لفتح مخطط تكامل الأنظمة التفاعلي](https://viewer.diagrams.net/?tags=%7B%7D&lightbox=1&highlight=0000ff&edit=_blank&layers=1&nav=1&dark=auto)

### 5.4.1 موديول القبول والترميز الطبي (`Admission Module - Module 1`)
* **الواجهة البرمجية المعتمدة:** `IPatientData`
* **طبيعة التكامل:** يقوم موديول الجراحة بالاستعلام الفوري باستخدام المعرّف الرقمي للمريض (`PatientDigitalID`) لجلب الرموز الطبية، فصيلة الدم، الحساسية، وملف المخاطر الصحية لضمان ملاءمة المريض طبياً للجراحة قبل تأكيد الحجز.

### 5.4.2 موديول إدارة الأسرة والأقسام الداخلية (`Bed Management Module - Module 3`)
* **الواجهة البرمجية المعتمدة:** `IBedAvailability`
* **طبيعة التكامل:** يتواصل النظام مع موديول الأسرة للتحقق التلقائي من توفر سرير شاغر ومجهز لفترة الإقامة ما بعد الجراحة (Post-Op Bed) وتخصيصه للمريض قبل إعطاء أمر الحجز النهائي للغرفة.

### 5.4.3 موديول الفوترة والحسابات (`Billing Module - Module 2`)
* **الواجهة البرمجية المعتمدة:** `ISurgeryBilling`
* **طبيعة التكامل:** فور اعتماد الحجز الجراحي وتغيير حالته إلى (Scheduled)، يرسل الموديول تفاصيل العملية (المعرّف الرقمي للمريض `PatientDigitalID` ومعرّف الخدمة الجراحية `ServiceID`) إلى موديول الحسابات لتبدأ عملية احتساب التكاليف والفوترة تلقائياً.

### 5.4.4 الربط مع قاعدة البيانات المركزية (`Central Database Connection`)
* **آلية الاتصال الهندسي:** يتم الاتصال برمجياً عبر الـ `SQL Connection` أو وسيط كائناتي (`Entity Framework Core`) للقراءة والكتابة على الجداول المركزية المشتركة الموضحة في مخطط المكونات:
  * جدول بيانات المرضى الموحد (`Patients`).
  * جدول الخدمات والرموز الطبية (`MedicalServices`).
  * جدول حجز العمليات وإدارة غرف العمليات (`SurgicalBookings`).

---

