## 5. توثيق موديول جدولة العمليات الجراحية - مخطط حالات الاستخدام (Use Case Specification)

**Prepared By:** Qamar Idrees (Functional Requirements)

يغطي هذا القسم التصميم اللوجستي وحالات الاستخدام الأساسية الخاصة بالطبيب والمجدوِل داخل موديول "تحسين غرف العمليات"، مع توثيق المتطلبات الوظيفية والروابط الحية للمخطط.

---

### 5.1 مخطط حالات الاستخدام (Use Case Diagram)

* **الأداة المستخدمة:** Draw.io (Diagrams.net)
* **رابط التعديل للمخطط:** [اضغط هنا لفتح مخطط حالات الاستخدام على Draw.io](https://drive.google.com/file/d/1R4uqBBfvsWN8nCmqOo2FKa0HadRnxaCP/view?usp=sharing)

#### 5.1.1 جدول توصيف حالة الاستخدام الرئيسية (Use Case Specification Table)

| عنصر التوصيف | الشرح الهندسي واللوجستي للحالة |
| :--- | :--- |
| **اسم حالة الاستخدام** | جدولة عملية جراحية جديدة (`Schedule New Surgical Operation`) |
| **الفاعلون الأساسيون** | طبيب العمليات الجراحية (`Operating Surgeon`) / مدير جدولة العمليات (`Surgical Schedule Manager`) |
| **الشروط المسبقة (Pre-conditions)** | أن يكون المريض مسجلاً بشكل قانوني في المستشفى ويمتلك رقماً تعريفياً صالحاً (`Patient ID`). |
| **التدفق الأساسي للنظام (Basic Flow)** | 1. يقوم المستخدم بإدخال تفاصيل الجراحة، وتحديد الغرفة والوقت المقترح.<br>2. يستعلم النظام آلياً عن ملف المخاطر الصحية للمريض من نظام `ADM-MC`.<br>3. يرسل النظام طلباً لنظام الأسرة لتأمين سرير إقامة لما بعد الجراحة.<br>4. يحسب النظام وقت التعقيم المطلق بناءً على نوع العملية ويحظره في الجدول لمنع التداخل.<br>5. يتحقق النظام من عدم وجود تضارب في مواعيد الطاقم والأدوات.<br>6. يتم تأكيد الموعد النهائي وحفظه في قاعدة البيانات بحالة `Scheduled`. |
| **العلاقات المضمنة (Includes)** | التحقق من ملف المخاطر الصحية، حجز سرير الإقامة، حظر وقت التعقيم، والتحقق من جاهزية الموارد والفريق اللوجستي. |

---

### 5.2 المتطلبات الوظيفية للنظام (Functional Requirements)

#### أولاً: إدارة واجهة الجدولة (Surgery Scheduling Management)
* **FR-1:** يجب على النظام توفير واجهة رسومية تتيح للموظف المخول إدخال طلب جراحي جديد يشمل (رقم المريض، غرفة العمليات المستهدفة، التاريخ، والوقت المقترح).
* **FR-2:** يجب على النظام رفض ومنع حفظ أي حجز جراحي في قاعدة البيانات المركزية ما لم يتم التحقق بنجاح من كافة القيود اللوجستية والطبية الإجبارية.

#### ثانياً: التحقق من المخاطر الصحية (Health Risk Profile Verification)
* **FR-3:** يجب على النظام بناء اتصال برمي آمن مع نظام القبول والترميز الطبي (`ADM-MC`) لقراءة الرموز الطبية والتشخيصية المسجلة للمريض بشكل مؤتمت.
* **FR-4:** يجب على النظام تحليل ملف المخاطر الصحية المسترجع للتأكد من ملاءمة المريض طبياً لإجراء الجراحة وعدم وجود موانع حرجة.

#### ثالثاً: حجز وإدارة أسرة الإقامة (Post-Op Bed Reservation)
* **FR-5:** يجب على النظام إرسال استعلام فوري لنظام إدارة أسرة الأقسام الداخلية (`IPD-BED`) للتحقق من وجود سرير شاغر ومجهز لاستقبال المريض فور انتهاء جراحته.
* **FR-6:** يجب على النظام إرسال أمر حجز مؤكد وتخصيص السرير وربطه بملف العملية الجراحية لضمان اللوجستيات الطبية.

#### رابعاً: إدارة فترات التعقيم وحظر المواعيد (Mandatory Sterilization Blocking)
* **FR-7:** يجب على النظام احتساب وقت التعقيم والتطهير الإلزامي لغرفة العمليات بشكل ديناميكي بناءً على نوع العملية الجراحية المسجلة برمزها الطبي.
* **FR-8:** يجب على النظام حظر (`Block`) الغرفة تماماً في جدول المواعيد خلال نافذة التعقيم المحسوبة لمنع أي تداخل أو ازدواجية في الحجوزات.

#### خامساً: حوكمة جاهزية الموارد والطاقم (Resource Readiness Verification)
* **FR-9:** يجب على النظام إجراء فحص تضارب المواعيد للتأكد من أن الطاقم الطبي المحدد (الجراحين، أطباء التخدير، وممرضي الغرفة) متاحون بالكامل وغير مرتبطين بعمليات أخرى في نفس الوقت.
* **FR-10:** يجب على النظام التحقق من جاهزية وتوفر الأدوات والأجهزة الطبية المعقمة والمطلوبة لطبيعة هذه الجراحة المحددة قبل إعطاء أمر التأكيد النهائي
كود المخطط<mxfile host="app.diagrams.net">
  <diagram name="‫الصفحة-1‬" id="ui3EWzeWK9kD22Ej-Au4">
    <mxGraphModel dx="1861" dy="564" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="827" pageHeight="1169" math="0" shadow="0">
      <root>
        <mxCell id="0" />
        <mxCell id="1" parent="0" />
        <mxCell id="2" parent="1" style="shape=umlActor;verticalLabelPosition=bottom;verticalAlign=top;html=1;outlineConnect=0;" value="&#xa;Surgical Schedule Manager&#xa;&#xa;" vertex="1">
          <mxGeometry height="60" width="30" x="50" y="80" as="geometry" />
        </mxCell>
        <mxCell id="3" parent="1" style="swimlane;whiteSpace=wrap;html=1;startSize=23;" value="System Boundary" vertex="1">
          <mxGeometry height="540" width="530" x="190" y="30" as="geometry" />
        </mxCell>
        <mxCell id="4" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Scheduling New Surgery" vertex="1">
          <mxGeometry height="60" width="120" x="205" y="40" as="geometry" />
        </mxCell>
        <mxCell id="5" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Verify Patient Risk Profile" vertex="1">
          <mxGeometry height="60" width="120" x="10" y="270" as="geometry" />
        </mxCell>
        <mxCell id="6" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Validate Sterilization Interval" vertex="1">
          <mxGeometry height="60" width="120" x="50" y="390" as="geometry" />
        </mxCell>
        <mxCell id="9" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Request &amp;amp; Reserve Post-Operative Bed" vertex="1">
          <mxGeometry height="60" width="120" x="400" y="270" as="geometry" />
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-14" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Check Surgical Tools Availability" vertex="1">
          <mxGeometry height="60" width="120" x="205" y="390" as="geometry" />
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-16" edge="1" parent="3" source="4" style="html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;curved=0;rounded=0;exitX=0.628;exitY=1.017;exitDx=0;exitDy=0;entryX=0.5;entryY=0;entryDx=0;entryDy=0;exitPerimeter=0;" target="Bh3eeANw92jz3nsG1cFQ-14" value="&amp;lt;&amp;lt;include&amp;gt;&amp;gt;">
          <mxGeometry relative="1" x="0.8141" y="1" as="geometry">
            <mxPoint as="offset" />
            <mxPoint x="260" y="510" as="sourcePoint" />
            <mxPoint x="180" y="510" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-19" edge="1" parent="3" source="4" style="html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;curved=0;rounded=0;exitX=0.5;exitY=1;exitDx=0;exitDy=0;entryX=0.625;entryY=0.044;entryDx=0;entryDy=0;entryPerimeter=0;" target="6" value="&amp;lt;&amp;lt;include&amp;gt;&amp;gt;">
          <mxGeometry relative="1" x="0.7856" as="geometry">
            <mxPoint as="offset" />
            <Array as="points" />
            <mxPoint x="80" y="410" as="sourcePoint" />
            <mxPoint x="90" y="420" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-20" edge="1" parent="3" source="4" style="html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;curved=0;rounded=0;exitX=0.766;exitY=1;exitDx=0;exitDy=0;entryX=0.5;entryY=0;entryDx=0;entryDy=0;exitPerimeter=0;" target="9" value="&amp;lt;&amp;lt;include&amp;gt;&amp;gt;">
          <mxGeometry relative="1" x="0.4575" y="3" as="geometry">
            <mxPoint as="offset" />
            <mxPoint x="230" y="210" as="sourcePoint" />
            <mxPoint x="290" y="660" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-21" edge="1" parent="3" source="4" style="html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;curved=0;rounded=0;exitX=0.335;exitY=0.977;exitDx=0;exitDy=0;entryX=0.418;entryY=-0.056;entryDx=0;entryDy=0;entryPerimeter=0;exitPerimeter=0;" target="5" value="&amp;lt;&amp;lt;include&amp;gt;&amp;gt;">
          <mxGeometry relative="1" x="0.4851" y="3" as="geometry">
            <mxPoint as="offset" />
            <mxPoint x="100" y="170" as="sourcePoint" />
            <mxPoint x="160" y="620" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-41" parent="3" style="ellipse;whiteSpace=wrap;html=1;" value="Check Medical Team Availability" vertex="1">
          <mxGeometry height="60" width="120" x="350" y="390" as="geometry" />
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-42" edge="1" parent="3" source="4" style="html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;curved=0;rounded=0;exitX=0.693;exitY=0.99;exitDx=0;exitDy=0;entryX=0.417;entryY=0;entryDx=0;entryDy=0;exitPerimeter=0;entryPerimeter=0;" target="Bh3eeANw92jz3nsG1cFQ-41" value="&amp;lt;&amp;lt;include&amp;gt;&amp;gt;">
          <mxGeometry relative="1" x="0.8141" y="1" as="geometry">
            <mxPoint as="offset" />
            <mxPoint x="310" y="280" as="sourcePoint" />
            <mxPoint x="350" y="569" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="8" parent="1" style="shape=umlActor;verticalLabelPosition=bottom;verticalAlign=top;html=1;outlineConnect=0;" value="Admission &amp;amp; Medical Coding System" vertex="1">
          <mxGeometry height="60" width="30" x="50" y="310" as="geometry" />
        </mxCell>
        <mxCell id="10" edge="1" parent="1" source="2" style="endArrow=none;html=1;rounded=0;exitX=1;exitY=0.3333333333333333;exitDx=0;exitDy=0;exitPerimeter=0;" value="">
          <mxGeometry height="50" relative="1" width="50" as="geometry">
            <Array as="points" />
            <mxPoint x="90" y="130" as="sourcePoint" />
            <mxPoint x="390" y="100" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="vwpjKL40l5mFPN0KpK_x-20" parent="1" style="shape=umlActor;verticalLabelPosition=bottom;verticalAlign=top;html=1;outlineConnect=0;" value="ٍSurgeon" vertex="1">
          <mxGeometry height="70" width="30" x="50" y="190" as="geometry" />
        </mxCell>
        <mxCell id="vwpjKL40l5mFPN0KpK_x-21" parent="1" style="shape=umlActor;verticalLabelPosition=bottom;verticalAlign=top;html=1;outlineConnect=0;" value="Internal Bed Management System" vertex="1">
          <mxGeometry height="60" width="30" x="827" y="310" as="geometry" />
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-23" edge="1" parent="1" source="vwpjKL40l5mFPN0KpK_x-21" style="endArrow=none;html=1;rounded=0;entryX=1;entryY=0.5;entryDx=0;entryDy=0;exitX=0;exitY=0.3333333333333333;exitDx=0;exitDy=0;exitPerimeter=0;" target="9" value="">
          <mxGeometry height="50" relative="1" width="50" as="geometry">
            <Array as="points" />
            <mxPoint x="710" y="290" as="sourcePoint" />
            <mxPoint x="580" y="433" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-29" edge="1" parent="1" source="5" style="endArrow=none;html=1;rounded=0;entryX=1;entryY=0.3333333333333333;entryDx=0;entryDy=0;entryPerimeter=0;exitX=0;exitY=0.5;exitDx=0;exitDy=0;" target="8" value="">
          <mxGeometry height="50" relative="1" width="50" as="geometry">
            <Array as="points" />
            <mxPoint x="190" y="320" as="sourcePoint" />
            <mxPoint x="110" y="320" as="targetPoint" />
          </mxGeometry>
        </mxCell>
        <mxCell id="Bh3eeANw92jz3nsG1cFQ-24" edge="1" parent="1" source="4" style="endArrow=none;html=1;rounded=0;entryX=1;entryY=0.3333333333333333;entryDx=0;entryDy=0;entryPerimeter=0;exitX=0;exitY=1;exitDx=0;exitDy=0;" target="vwpjKL40l5mFPN0KpK_x-20" value="">
          <mxGeometry height="50" relative="1" width="50" as="geometry">
            <Array as="points" />
            <mxPoint y="190" as="sourcePoint" />
            <mxPoint x="110" y="303" as="targetPoint" />
          </mxGeometry>
        </mxCell>
      </root>
    </mxGraphModel>
  </diagram>
</mxfile>
