using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WDT2020.MS3.PVThang.Backend.Models
{
    public class Employee
    {
        public Guid EmployeeId { get ; set; }
        public String EmployeeId_ { get { return EmployeeId.ToString(); } }

        [Required("Mã nhân viên")]
        [CheckDuplicate("Mã nhân viên")]
        public String EmployeeCode { get; set; }

        [Required("Họ và tên")]
        public String FullName { get; set; }
        public int? Gender { get; set; }
        public String GenderName { get { 
            switch (Gender)
            {
                case 0: return "Nam";
                case 1: return "Nữ";
                case 2: return "Khác";
                default: return "Không xác định";
            }
        } }
        public DateTime? DateOfBirth { get; set; }

        [Required("Số CMND")]
        public String IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public String IdentityPlace { get; set; }

        [Required("Email")]
        public String Email { get; set; }

        [Required("Số điện thoại")]
        public String PhoneNumber { get; set; }
        public Guid PositionId { get; set; }
        public String PositionId_ { get { return PositionId.ToString(); } }
        public Guid DepartmentId { get; set; }
        public String DepartmentId_ { get { return DepartmentId.ToString(); } }
        public String PersonalTaxCode { get; set; }
        public int? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public int? JobStatus { get; set; }
        public String JobStatusName { get {
            switch (Gender)
            {
                case 0: return "Đang làm việc";
                case 1: return "Đã nghỉ việc";
                case 2: return "Đang thử việc";
                default: return "Không xác định";
            }
        } }
        public String PositionName { get; set; }
        public String DepartmentName { get; set; }
    }
}
