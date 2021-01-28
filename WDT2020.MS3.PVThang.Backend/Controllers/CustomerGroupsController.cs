using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020.MS3.PVThang.Backend.Controllers
{
    public class CustomerGroupsController : BaseEntityController<CustomerGroup>
    {

        public override IActionResult Get(string id)
        {
            return Ok(new ServiceResult()
            {
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Post([FromBody] CustomerGroup template)
        {
            return Ok(new ServiceResult()
            {
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Put([FromBody] CustomerGroup template)
        {
            return Ok(new ServiceResult()
            {
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Delete(string id)
        {
            return Ok(new ServiceResult()
            {
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }
    }
}
