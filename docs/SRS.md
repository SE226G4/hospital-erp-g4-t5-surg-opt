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

## 5. User Stories & Interface Logic (Maryam Hamwi)
* **رابط مخطط التتابع المعماري (Sequence Diagram):** [اضغط هنا للاطلاع على المخطط](https://drive.google.com/file/d/1Tp552sqs07dr1AhAjJiO3CbHDzHxVEiv/view?usp=sharing)

### 5.1 Agile User Stories
* **US1 (Surgery Scheduling):** As a Surgeon, I want to schedule a surgical operation for a patient through the UI, So that the operating room and time slot are reserved efficiently. *(Mapped to GitHub Issue #1)*
* **US2 (Conflict Prevention):** As an Operations Coordinator, I want the system to automatically block any surgery scheduling if there is a time conflict for the selected operating room, So that we can prevent scheduling overlaps and delays. *(Mapped to GitHub Issue #2)*
* **US3 (Mandatory Sterilization):** As a Surgeon, I want the system to automatically include a mandatory sterilization time buffer after each surgery in the room, So that patient safety is ensured and the room is prepared for the next operation. *(Mapped to GitHub Issue #3)*
* **US4 (Resource Check):** As an Operations Coordinator, I want the system to verify the availability of the full medical team and the readiness of surgical tools before confirming the schedule. *(Mapped to GitHub Issue #4)*
* **US5 (Bed Reservation):** As an Operations Coordinator, I want the system to check and secure a post-operative bed for the patient via the IPD-BED system. *(Mapped to GitHub Issue #5)*
* **US6 (Unified Patient File):** As a Surgeon, I want the system to automatically fetch the patient’s medical data and risk profile from the ADM-MC system using their ID. *(Mapped to GitHub Issue #6)*

### 5.2 Interface Logic (منطق استجابة الواجهة)
* **Form Validation:** زر "إرسال الطلب" (Submit) يظل معطلاً (Disabled) ولا يمكن الضغط عليه حتى يتم ملء جميع الحقول الإلزامية.
* **Loading State:** عند الضغط على زر الحجز، تتحول الواجهة لحالة الانتظار ويظهر مؤشر تحميل (Loading Spinner) لمنع تكرار إرسال الطلبات المتطابقة أثناء المعالجة.
* **Dynamic Feedback:** في حالة النجاح، تظهر رسالة نجاح خضراء تعرض تفاصيل الحجز المؤكد. في حالة الفشل، يظل النموذج مفتوحاً مع إظهار رسالة خطأ حمراء منبثقة (مثال: "تضارب في الوقت: الغرفة بحاجة لـ 45 دقيقة تعقيم").
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

### 6.2 Visual Diagrams Display (عرض المخططات الهندسية)

#### A. Class Diagram
![Class Diagram لقسم الجراحة](../docs/classDiagram.drawio (1).png)

#### B. Entity-Relationship Diagram (ERD)
![Entity Relationship Diagram](../ERD.png)

---

