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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WDT2020.MS3.PVThang.Backend.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PositionsController : BaseEntityController<Position>
    {
        // GET: api/<PositionsController>
        //[HttpGet]
        //public List<Position> Get()
        //{
        //    var databaseConnector = new DatabaseConnector<Position>();
        //    var result = databaseConnector.GetAll();
        //    return (List<Position>)result;
        //}

        public override IActionResult Get(string id)
        {
            return Ok(new ServiceResult()
            {
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Post([FromBody] Position template)
        {
            return Ok(new ServiceResult() { 
                Data = null,
                Message = Properties.Resources.Nocontent,
                Code = Enum.ResultCode.Success
            });
        }

        public override IActionResult Put([FromBody] Position template)
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
