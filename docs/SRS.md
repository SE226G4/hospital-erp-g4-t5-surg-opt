Software Requirements Specification (SRS)

Project: Hospital ERP System

Module/Subsystem: Surgical Optimization - Module 5

Version: 1.0

Date: 2026-05-081

1. Introduction

1.1 Purpose

الغرض من هذا المستند هو تحديد المتطلبات البرمجية لموديول "تحسين غرف العمليات".يهدف النظام إلى تنسيق جدولة العمليات الجراحية لضمان كفاءة العمل ومنع أي تضارب في المواعيد.  

1.2 Scope

نظام لجدولة العمليات الجراحية، يمنع تضارب المواعيد، ويتأكد من توفر الأسرة والفريق الطبي والتعقيم قبل البدء. 


2.1 Overall Description

2. Product Perspective

 
هذا الموديول جزء من نظام المشفى المتكامل، ويعتمد بشكل أساسي على بيانات المريض من نظام القبول (Module 1)
2.2 Product Functions

جدولة العمليات الجراحية ومنع تضارب المواعيد. 


التحقق من توفر الفريق الطبي والأسرة قبل الحجز


إدارة وقت التعقيم الإلزامي بين العمليات

2.3 User Characteristics

المستخدمون هم الأطباء الجراحون، طاقم التمريض، وإدارة غرف العمليات

3. Specific Requirements (Agile Approach)
 
3.2 System Features & User Stories

3.2.1 Feature: Surgical Scheduling

Description: 
نظام لجدولة المواعيد يمنع التداخل

User Stories:


بصفبصفتي جراحاً، أريد جدولة عملية جراحية بحيث يمنع النظام أي تضارب في المواعيد. (GitHub Issue: #1)


بصفتي مسؤول تعقيم، أريد من النظام احتساب وقت التعقيم الإلزامي بين العمليات. (GitHub Issue: #4)
3.2.2 Feature: Resource Validation

Description: التحقق من المتطلبات قبل الحجز.  


User Stories:


بصفتي منسقاً، أريد ألا يسمح النظام بحجز عملية إلا بعد التأكد من توفر الفريق الطبي والأسرة (نظام الأسرة). (GitHub Issue: #2, #3)


بصفتي مبرمجاً، أريد أن يعتمد النظام حصراً على بيانات المريض من نظام القبول. (GitHub Issue: #5)

## 4. Appendices
### Appendix B: GitHub Traceability Checklist
* [x] Every User Story in Section 3.2 has a corresponding GitHub Issue.
* [x] Every GitHub Issue has an appropriate label.
* [x] Pull Requests reference the Issue IDs (e.g., Closes #1).
