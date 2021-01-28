using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.Enum;

namespace WDT2020.MS3.PVThang.Backend.Models
{
    public class ServiceResult
    {
        public Object Data { get; set; }
        public String Message { get; set; }
        public ResultCode Code { get; set; }
    }
}
