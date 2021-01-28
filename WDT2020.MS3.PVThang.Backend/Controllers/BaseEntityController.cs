using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.data;
using WDT2020.MS3.PVThang.Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020.MS3.PVThang.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<T> : ControllerBase
    {
        protected DatabaseConnector<T> _databaseConnector;
        public String _className;

        public BaseEntityController()
        {
            _className = typeof(T).Name;
            _databaseConnector = new DatabaseConnector<T>();
        }
        // GET: api/<BaseEntityController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var procName = $"Proc_GetAll{_className}s";
            return Ok(new ServiceResult() {
                Data = _databaseConnector.GetList<T>(procName, new { }),
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        // GET api/<BaseEntityController>/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(String id)
        {
            var procName = $"Proc_Get{_className}ById";
            T result = _databaseConnector.GetFirst<T>(procName ,new { Id = id });
            return Ok(new ServiceResult()
            {
                Data = result,
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        // POST api/<BaseEntityController>
        [HttpPost]
        public virtual IActionResult Post([FromBody] T template)
        {
            var procName = $"Proc_Insert{_className}";
            int result = _databaseConnector.Change(procName, template);
            return Ok(new ServiceResult()
            {
                Data = result,
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }

        // PUT api/<BaseEntityController>/5
        [HttpPut]
        public virtual IActionResult Put([FromBody] T template)
        {
            var procName = $"Proc_Update{_className}";
            var result = _databaseConnector.Change(procName, template);
            return Ok(new ServiceResult()
            {
                Data = result,
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });

        }

        // DELETE api/<BaseEntityController>/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(String id)
        {
            var procName = $"Proc_Delete{_className}";
            var result = _databaseConnector.Change(procName, new { Id = id });
            return Ok(new ServiceResult()
            {
                Data = result,
                Message = Properties.Resources.Success,
                Code = Enum.ResultCode.Success
            });
        }
    }
}
