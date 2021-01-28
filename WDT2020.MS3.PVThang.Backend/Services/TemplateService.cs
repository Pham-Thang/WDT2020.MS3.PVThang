using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.data;
using WDT2020.MS3.PVThang.Backend.Models;

namespace WDT2020.MS3.PVThang.Backend.Services
{
    public class TemplateService<Template>
    {
        DatabaseConnector<Template> _databaseConnector;
        ServiceResult _serviceResult;
        String _className;
        public TemplateService()
        {
            _databaseConnector = new DatabaseConnector<Template>();
            _serviceResult = new ServiceResult();
            _className = typeof(Template).Name;
        }

        public ServiceResult Insert(Template template)
        {
            //validate
            ValidateObject(template);
            if (_serviceResult.Code == Enum.ResultCode.BadRequest)
            {
                return _serviceResult;
            }
            //Insert
            _serviceResult.Data = _databaseConnector.Insert($"Proc_Insert{_className}", template);
            _serviceResult.Message = Properties.Resources.Success;
            _serviceResult.Code = Enum.ResultCode.Success;
            return _serviceResult;
        }

        public ServiceResult Update(Template template)
        {
            //validate
            ValidateObject(template);
            if (_serviceResult.Code == Enum.ResultCode.BadRequest)
            {
                return _serviceResult;
            }
            //Update
            _serviceResult.Data = _databaseConnector.Insert($"Proc_Update{_className}", template);
            _serviceResult.Message = Properties.Resources.Success;
            _serviceResult.Code = Enum.ResultCode.Success;
            return _serviceResult;
        }

        private void ValidateObject(Template template)
        {
            var properties = typeof(Template).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(template);
                //validate required
                if (property.IsDefined(typeof(Required), true) && (value == null || value.ToString().Trim() == String.Empty))
                {
                    var propertyName = property.GetCustomAttributes(typeof(Required), true).FirstOrDefault();
                    if (propertyName != null)
                    {
                        var message = (propertyName as Required).propertyName + Properties.Resources.ErrorRequired + ". ";
                        if ((propertyName as Required).errorMessage != null) message = (propertyName as Required).errorMessage;
                        _serviceResult.Message = _serviceResult.Message==null? message:(_serviceResult.Message + '\n' + message);
                    }
                    _serviceResult.Code = Enum.ResultCode.BadRequest;
                }
                //validate duplicate
                if (property.IsDefined(typeof(CheckDuplicate), true) && _databaseConnector.GetFirst<int>($"Proc_Check{property.Name}", template) > 0)
                {
                    var propertyName = property.GetCustomAttributes(typeof(CheckDuplicate), true).FirstOrDefault();
                    if (propertyName != null)
                    {
                        var message = (propertyName as CheckDuplicate).propertyName + Properties.Resources.ErrorRequired + ". ";
                        if ((propertyName as CheckDuplicate).errorMessage != null) message = (propertyName as CheckDuplicate).errorMessage;
                        _serviceResult.Message = _serviceResult.Message == null ? message : (_serviceResult.Message + '\n' + message);
                    }
                    _serviceResult.Code = Enum.ResultCode.BadRequest;
                }
            }
        }
    }
}
