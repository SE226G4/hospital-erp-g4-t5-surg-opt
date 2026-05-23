using System;
using System.Collections.Generic;

namespace HospitalERP.SurgicalOptimization
{
    public class SurgicalScheduler
    {
        // الدالة الشاملة للتحقق من متطلبات جدولة العملية الجراحية
        public string CheckSurgeryConstraints(string patientId, int roomId, DateTime startTime, DateTime endTime, List<string> staffIds)
        {
            // 1. التحقق من صحة بيانات المريض من نظام القبول
            if (!IsPatientValidInAdmissionSystem(patientId))
            {
                return "خطأ: رقم المريض غير صحيح أو غير مسجل في نظام القبول.";
            }

            // 2. فحص تضارب مواعيد الطاقم الطبي (الجراحين وأطباء التخدير)
            if (IsMedicalStaffBusy(staffIds, startTime, endTime))
            {
                return "خطأ: هناك تضارب في مواعيد الطاقم الطبي المحدد لهذه العملية.";
            }

            // 3. حساب وقت التعقيم الإلزامي الفاصل وحظر الغرفة لمنع التداخل
            DateTime sterilizationEndTime = endTime.AddMinutes(45); // إضافة 45 دقيقة تعقيم إلزامية
            if (IsRoomOccupied(roomId, startTime, sterilizationEndTime))
            {
                return "خطأ: الغرفة مستغلة أو تقع ضمن فترة التعقيم لعملية أخرى.";
            }

            // 4. التحقق من حجز وتوفر سرير للمريض في جناح الإقامة لما بعد الجراحة
            if (!IsPostOpBedAvailable(patientId))
            {
                return "خطأ: لا يوجد سرير شاغر متاح في جناح الإقامة لما بعد الجراحة.";
            }

            // 5. في حال اجتياز جميع الشروط يتم تأكيد الحجز بنجاح
            SaveSurgeryToDatabase(patientId, roomId, startTime, sterilizationEndTime, "Scheduled");
            return "نجاح: تم تأكيد حجز العملية وحظر الغرفة لوقت التعقيم وتخصيص السرير.";
        }

        // دالات التحقق المساعدة (تُربط لاحقاً بقواعد البيانات والأنظمة الأخرى)
        private bool IsPatientValidInAdmissionSystem(string id) => true;
        private bool IsMedicalStaffBusy(List<string> ids, DateTime start, DateTime end) => false;
        private bool IsRoomOccupied(int id, DateTime start, DateTime end) => false;
        private bool IsPostOpBedAvailable(string id) => true;
        private void SaveSurgeryToDatabase(string id, int rId, DateTime start, DateTime end, string status) { }
    }
}
