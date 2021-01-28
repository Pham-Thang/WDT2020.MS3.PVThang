using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.Models;
using WDT2020.MS3.PVThang.Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020.MS3.PVThang.Backend.Controllers
{
    public class CustomersController : BaseEntityController<Customer>
    {

        [HttpGet("{page}&{number}&{filterText}")]
        public IActionResult Get(int page, int number, String filterText)
        {
            if (filterText == "-") filterText = "";
            int start = number * (page - 1);
            Object input = new { Start = start, Number = number, FilterText = filterText };
            return Ok(new ServiceResult()
            {
                Data = _databaseConnector.Get("Proc_GetCustomers", input),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        [HttpGet("GetCustomerCodeCodeMax")]
        public IActionResult GetCustomerCodeMax()
        {
            return Ok(new ServiceResult()
            {
                Data = _databaseConnector.GetFirst<Object>("Proc_GetCustomerCodeMax", new { }),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        // GET: api/<EmployeesController>
        [HttpGet("count/{filterText}")]
        public IActionResult Count(String filterText)
        {
            if (filterText == "-") filterText = "";
            Object input = new {FilterText = filterText };
            return Ok(new ServiceResult()
            {
                Data = _databaseConnector.GetFirst<int>("Proc_CountCustomers", input),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Post([FromBody] Customer template)
        {
            //var procName = $"Proc_Insert{_className}";
            //int result = _databaseConnector.Insert(procName, template);
            template.CustomerId = new Guid();
            var customerService = new TemplateService<Customer>();
            var res = customerService.Insert(template);
            switch (res.Code)
            {
                case Enum.ResultCode.BadRequest:
                    return BadRequest(res);
                default:
                    return Ok(res);
            }
        }

        public override IActionResult Put([FromBody] Customer template)
        {
            var customerService = new TemplateService<Customer>();
            var res = customerService.Update(template);
            switch (res.Code)
            {
                case Enum.ResultCode.BadRequest:
                    return BadRequest(res);
                default:
                    return Ok(res);
            }
        }
    }
}
