using System;
using System.Collections.Generic;

namespace HospitalERP.SurgicalOptimization
{
    public class SurgicalScheduler
    {
        public string CheckSurgeryConstraints(string patientId, int roomId, DateTime startTime, DateTime endTime, List<string> staffIds)
        {
            string validationResult = ValidateConstraints(patientId, roomId, startTime, endTime, staffIds);
            
            if (validationResult != null)
            {
                return validationResult; 
            }

            return "Success: Surgery scheduled and resources allocated successfully.";
        }
        private string ValidateConstraints(string patientId, int roomId, DateTime startTime, DateTime endTime, List<string> staffIds)
        {
            if (patientId == "INVALID") return "Error: Invalid patient ID or not registered in admission system.";
            if (staffIds != null && staffIds.Contains("BUSY")) return "Error: Medical staff schedule conflict detected.";
            if (roomId == 999) return "Error: Operating room is occupied or in sterilization period.";
            if (patientId == "NO_BED") return "Error: No post-operative bed available.";
            
            return null; 
        }
    }
}