using System;
using System.Collections.Generic;
using Xunit;

namespace HospitalERP.SurgicalOptimization
{
    public class ProgramTests
    {
        [Fact]
        public void Test_PatientInvalid_ReturnsError()
        {
            var scheduler = new SurgicalScheduler();
            var result = scheduler.CheckSurgeryConstraints("INVALID", 1, DateTime.Now, DateTime.Now.AddHours(2), new List<string> { "D1" });
            Assert.Equal("Error: Invalid patient ID or not registered in admission system.", result);
        }

        [Fact]
        public void Test_StaffBusy_ReturnsError()
        {
            var scheduler = new SurgicalScheduler();
            var result = scheduler.CheckSurgeryConstraints("P100", 1, DateTime.Now, DateTime.Now.AddHours(2), new List<string> { "BUSY" });
            Assert.Equal("Error: Medical staff schedule conflict detected.", result);
        }

        [Fact]
        public void Test_RoomOccupied_ReturnsError()
        {
            var scheduler = new SurgicalScheduler();
            var result = scheduler.CheckSurgeryConstraints("P100", 999, DateTime.Now, DateTime.Now.AddHours(2), new List<string> { "D1" });
            Assert.Equal("Error: Operating room is occupied or in sterilization period.", result);
        }

        [Fact]
        public void Test_NoBedAvailable_ReturnsError()
        {
            var scheduler = new SurgicalScheduler();
            var result = scheduler.CheckSurgeryConstraints("NO_BED", 1, DateTime.Now, DateTime.Now.AddHours(2), new List<string> { "D1" });
            Assert.Equal("Error: No post-operative bed available.", result);
        }

        [Fact]
        public void Test_AllValid_ReturnsSuccess()
        {
            var scheduler = new SurgicalScheduler();
            var result = scheduler.CheckSurgeryConstraints("P100", 1, DateTime.Now, DateTime.Now.AddHours(2), new List<string> { "D1" });
            Assert.Equal("Success: Surgery scheduled and resources allocated successfully.", result);
        }
    }
}
