using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.data;
using WDT2020.MS3.PVThang.Backend.Models;
using WDT2020.MS3.PVThang.Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020.MS3.PVThang.Backend.Controllers
{
    public class EmployeesController : BaseEntityController<Employee>
    {
        // GET api/<EmployeesController>/5&2
        [HttpGet("{page}&{number_employee}&{departmentId}&{positionId}&{filterText}")]
        public IActionResult Get(int page, int number_employee, String departmentId, String positionId, String filterText)
        {
            if (departmentId != null) departmentId = departmentId.Trim();
            else departmentId = "";
            if (positionId != null) positionId = positionId.Trim();
            else positionId = "";
            if (filterText != null) filterText = filterText.Trim();
            else filterText = "";
            int start = number_employee * (page - 1);
            Object input = new { Start = start, Number = number_employee, DepartmentId = departmentId, PositionId = positionId, FilterText = filterText };
            return Ok(new ServiceResult()
            {
                Data = _databaseConnector.Get("Proc_GetEmployees", input),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        // GET: api/<EmployeesController>
        [HttpGet("count/{departmentId}&{positionId}&{filterText}")]
        public IActionResult Get(String departmentId, String positionId, String filterText)
        {
            if (departmentId != null) departmentId = departmentId.Trim();
            else departmentId = "";
            if (positionId != null) positionId = positionId.Trim();
            else positionId = "";
            if (filterText != null) filterText = filterText.Trim();
            else filterText = "";
            Object input = new { DepartmentId = departmentId, PositionId = positionId, FilterText = filterText };
            return Ok(new ServiceResult()
            {
                Data = _databaseConnector.GetFirst<int>("Proc_CountEmployees", input),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Post([FromBody] Employee template)
        {
            template.EmployeeId = new Guid();
            var employeeService = new TemplateService<Employee>();
            var res = employeeService.Insert(template);
            switch(res.Code)
            {
                case Enum.ResultCode.BadRequest:
                    return BadRequest(res);
                default:
                    return Ok(res);
            }
        }

        public override IActionResult Put([FromBody] Employee template)
        {
            var employeeService = new TemplateService<Employee>();
            ServiceResult res = employeeService.Update(template);
            switch (res.Code)
            {
                case Enum.ResultCode.BadRequest:
                    return BadRequest(0);
                default:
                    return Ok(1);
            }
        }
    }
}
