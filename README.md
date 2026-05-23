# Module Name: [Surgical Optimization System]
## Project: [Hospital ERP System]
**Module Code:** [SURG-05]
**Group:** G4 | Team: T5
---

## 📝 Module Overview
هذا الموديول مسؤول عن إدارة وتحسين غرف العمليات الجراحية. يقوم النظام بجدولة العمليات، ومنع تضارب المواعيد، والتحقق من جاهزية الموارد (الأطباء، الأسرّة، والأدوات)، بالإضافة إلى إدارة أوقات التعقيم الإلزامية لضمان سلامة المرضى.
---

## 👥 Team Members & Responsibilities
*This table is flexible. Assign tasks based on team size (4 to 6 members).*

| Member Name | Primary Responsibility | Assigned Tasks (Examples) | GitHub Profile |
| :--- | :--- | :--- | :--- |
| **Elaf Mallohi (Leader)** | Integration & Architecture | Master Component Diagram, Team Coordination | [elafmallohi](https://github.com/elafmallohi) |
| **Qamar Idrees** | Requirements & Analysis | Functional Requirements, Coding (Functions 1 & 4) | [QamarIdrees22](https://github.com/QamarIdrees22) |
| **Raghad Brejawi** | Process Modeling | Activity Diagrams, ERD Database Design | [raghadbrijawi](https://github.com/raghadbrijawi) |
| **Lilas hammada** | Data Design | ERD Database Design, Shared Tables Integration | [hammadililas12-create](https://github.com/hammadililas12-create) |
| **Asmaa Rahhal** | Interaction Design | Master Component Diagram, Logic Flow | [Asmaa20044](https://github.com/Asmaa20044) |
| **Maryam Hamwi** | UI/UX & Frontend | User Stories, Coding (Functions 1 & 4) | [maryamalhamwi](https://github.com/maryamalhamwi) |

---

## 🚀 Analysis & Design Progress
- [ ] **Requirement Elicitation:** Completed list of FRs/NFRs.
- [ ] **UML Behavioral Diagrams:** Use Case and Activity Diagrams.
- [ ] **UML Structural Diagrams:** ERD and Class Diagrams.
- [ ] **Dynamic Modeling:** Sequence Diagrams for core processes.
- [ ] **Interface Design:** Low-fidelity Wireframes.

---

## 🔗 Integration Points
*يتكامل نظامنا مع الأنظمة التالية لضمان دقة العمل:*
* **Inbound (وارد): يستقبل بيانات المريض من Admission Module (Module 1).
* **Inbound (وارد): يستقبل حالة توفر الأسرة من Bed Management (Module 3).
* * **Outbound (صادر): يرسل بيانات العمليات المنجزة إلى Billing Module (Module 2).

---
## 🛠 Tools Used
* **Modeling:** VS Code, .NET, Git.
* **Documentation:** Markdown / LaTeX.
* **Version Control:** GitHub.
## Use Case Diagram Documentation

### 1. Use Case Specification Table
| Element | Description |
| :--- | :--- |
| **Use Case Name** | Schedule New Surgical Operation |
| **Primary Actor**| Operating Surgeon / Surgical Schedule Manager |
| **Pre-conditions** | Patient must be registered in the hospital system with a valid Patient ID. |
| **Basic Flow** | 1. The user inputs surgery details, preferred operating room, and time.<br>2. The system automatically triggers the patient risk profile verification.<br>3. The system requests and reserves a post-operative bed.<br>4. The system calculates and blocks the mandatory sterilization interval.<br>5. The system verifies medical team and surgical tools availability.<br>6. The system confirms and saves the schedule. |
| **Included Cases** | Verify Patient Identity & Health Risk Profile, Request & Reserve Post-Operative Bed, Validate Sterilization Interval, Check Surgical Tools Availability, Check Medical Team Availability. |

---

### 2. Functional Requirements (FRs)

#### A. Surgery Scheduling Management
* **FR-1:** The system shall provide a user interface enabling the Operating Surgeon and Surgical Schedule Manager to input new surgery requests (Patient ID, Operating Room, Date, and Proposed Time).
* **FR-2:** The system shall prevent finalizing any surgery schedule in the database until all automated compliance checks and hospital logistics are fully validated.

#### B. Health Risk Profile Verification
* **FR-3:** The system shall integrate with the **Admission & Medical Coding System** to automatically retrieve and read the patient's medical history codes.
* **FR-4:** The system shall verify the patient's health risk profile to ensure there are no critical medical contraindications before securing the surgery slot.

#### C. Inpatient Department Bed Management
* **FR-5:** The system shall communicate with the **Internal Bed Management System** to check the live availability of a suitable post-operative bed.
* **FR-6:** The system shall automatically submit a confirmed bed reservation linked directly to the surgery schedule profile.

#### D. Mandatory Sterilization Blocking
* **FR-7:** The system shall calculate the mandatory sterilization interval required for the operating room based on the specific medical procedure code.
* **FR-8:** The system shall block the operating room schedule during the sterilization window to strictly prevent any overlapping or double-booking.

#### E. Resource Readiness Verification
* **FR-9:** The system shall cross-reference schedules to ensure the complete surgical team (surgeons, anesthesiologists, and nurses) is fully available without conflict.
* **FR-10:** The system shall verify the availability and readiness of critical medical tools and equipment required for the chosen surgery type at the designated time.
* التوصيف باللغة العربية
* ## توثيق مخطط حالات الاستخدام والمتطلبات الوظيفية

### 1. شرح المخطط الهيكلي (Use Case Diagram Description)
يهدف نظام تحسين غرف العمليات الجراحية (SURG-OPT) إلى أتمتة وحوكمة جدولة المواعيد الطبية لمنع الأخطاء البشرية وتجنب تضارب المواعيد أو نقص الموارد اللوجستية. 

* **الفاعلون الأساسيون (Primary Actors):** يتواجد على يسار النظام كل من طبيب العمليات الجراحية (Operating Surgeon) ومدير جدولة العمليات (Surgical Schedule Manager)، وهما المسؤولان عن بدء التفاعل مع النظام وإدخال بيانات الجدولة.
* **علاقات الإلزام (<<include>>):** لضمان سلامة المرضى وكفاءة التشغيل، تم تصميم الدائرة الرئيسية "جدولة عملية جراحية جديدة" بحيث لا يمكن إتمامها أو حفظها في قاعدة البيانات إلا بعد المرور إلزامياً بأربعة شروط برمجية متزامنة عبر علاقة الـ Include وهي: (التحقق من ملف المخاطر، حجز سرير الإقامة، حظر وقت التعقيم، والتحقق من جاهزية الفريق والأدوات).
* **الأنظمة الخارجية الداعمة (Supporting Systems):** يتكامل نظامنا مع الأنظمة الخارجية المتواجدة على اليمين لتبادل البيانات الحيوية: نظام القبول والترميز الطبي (ADM-MC) ونظام إدارة الأسرة الداخلي (IPD-BED).

---

### 2. جدول توصيف حالة الاستخدام الرئيسية (Use Case Specification Table)
| عنصر التوصيف | الشرح الهندسي واللوجستي للحالة |
| :--- | :--- |
| **اسم حالة الاستخدام** | جدولة عملية جراحية جديدة (`Schedule New Surgical Operation`) |
| **الفاعلون الأساسيون** | طبيب العمليات الجراحية / مدير جدولة العمليات |
| **الشروط المسبقة** | أن يكون المريض مسجلاً بشكل قانوني في المستشفى ويمتلك رقماً تعريفياً صالحاً (`Patient ID`). |
| **التدفق الأساسي للنظام** | 1. يقوم المستخدم بإدخال تفاصيل الجراحة، وتحديد الغرفة والوقت المقترح.<br>2. يستعلم النظام آلياً عن ملف المخاطر الصحية للمريض من نظام `ADM-MC`.<br>3. يرسل النظام طلباً لنظام الأسرة لتأمين سرير إقامة لما بعد الجراحة.<br>4. يحسب النظام وقت التعقيم المطلق بناءً على نوع العملية ويحظره في الجدول لمنع التداخل.<br>5. يتحقق النظام من عدم وجود تضارب في مواعيد الطاقم والأدوات.<br>6. يتم تأكيد الموعد النهائي وحفظه. |

---

### 3. المتطلبات الوظيفية للنظام (Functional Requirements)

#### أولاً: إدارة واجهة الجدولة (Surgery Scheduling Management)
* **FR-1:** يجب على النظام توفير واجهة رسومية تتيح للموظف المخول إدخال طلب جراحي جديد يشمل (رقم المريض، غرفة العمليات المستهدفة، التاريخ، والوقت المقترح).
* **FR-2:** يجب على النظام رفض ومنع حفظ أي حجز جراحي في قاعدة البيانات المركزية ما لم يتم التحقق بنجاح من كافة القيود اللوجستية والطبية الإجبارية.

#### ثانياً: التحقق من المخاطر الصحية (Health Risk Profile Verification)
* **FR-3:** يجب على النظام بناء اتصال برمي آمن مع نظام القبول والترميز الطبي لقراءة الرموز الطبية والتشخيصية المسجلة للمريض بشكل مؤتمت.
* **FR-4:** يجب على النظام تحليل ملف المخاطر الصحية المسترجع للتأكد من ملاءمة المريض طبياً لإجراء الجراحة وعدم وجود موانع حرجة.

#### ثالثاً: حجز وإدارة أسرة الإقامة (Post-Op Bed Reservation)
* **FR-5:** يجب على النظام إرسال استعلام فوري لنظام إدارة أسرة الأقسام الداخلية للتحقق من وجود سرير شاغر ومجهز لاستقبال المريض فور انتهاء جراحته.
* **FR-6:** يجب على النظام إرسال أمر حجز مؤكد وتخصيص السرير وربطه بملف العملية الجراحية لضمان اللوجستيات الطبية.

#### رابعاً: إدارة فترات التعقيم وحظر المواعيد (Mandatory Sterilization Blocking)
* **FR-7:** يجب على النظام احتساب وقت التعقيم والتطهير الإلزامي لغرفة العمليات بشكل ديناميكي بناءً على نوع العملية الجراحية المسجلة برمزها الطبي.
* **FR-8:** يجب على النظام حظر (Block) الغرفة تماماً في جدول المواعيد خلال نافذة التعقيم المحسوبة لمنع أي تداخل أو ازدواجية في الحجوزات.

#### خامساً: حوكمة جاهزية الموارد والطاقم (Resource Readiness Verification)
* **FR-9:** يجب على النظام إجراء فحص تضارب المواعيد للتأكد من أن الطاقم الطبي المحدد (الجراحين، أطباء التخدير، وممرضي الغرفة) متاحون بالكامل وغير مرتبطين بعمليات أخرى في نفس الوقت.
* **FR-10:** يجب على النظام التحقق من جاهزية وتوفر الأدوات والأجهزة الطبية المعقمة والمطلوبة لطبيعة هذه الجراحة المحددة قبل إعطاء أمر التأكيد النهائي.
