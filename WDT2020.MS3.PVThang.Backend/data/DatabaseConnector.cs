using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WDT2020.MS3.PVThang.Backend.Models;

namespace WDT2020.MS3.PVThang.Backend.data
{
    public class DatabaseConnector<T>
    {
        public String _connectionString;
        IDbConnection _dbConnection;
        public DatabaseConnector()
        {
            _connectionString = "User Id=nvmanh;Password=12345678;Database=MS3_06_PVTHANG_CuckCuk;Port=3306;Host=103.124.92.43;Character Set=utf8";
            _dbConnection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Lấy dữ liệu theo procedure có tên procName với đầu vào là input
        /// </summary>
        /// <param name="procName">Tên procedure</param>
        /// <param name="input">Object chứa các biến truyền vào</param>
        /// <returns>Danh sách Object</returns>
        public IEnumerable<T> Get(String procName, Object input)
        {
            return _dbConnection.Query<T>(procName, input, commandType: CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// Lấy dữ liệu theo procedure có tên procName với đầu vào là id
        /// </summary>
        /// <param name="procName">Tên procedure</param>
        /// <param name="input">Object chứa proprety Id</param>
        /// <returns>Object</returns>
        public T GetById(String procName, Object id)
        {
            return _dbConnection.Query<T>(procName, id, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        /// <summary>
        /// Thêm dữ liệu theo procedure có tên procName với đầu vào là input
        /// </summary>
        /// <param name="procName">Tên procedure</param>
        /// <param name="input">Object chứa các biến truyền vào</param>
        /// <returns>Số dòng thay đổi</returns>
        public int Insert(String procName ,T input)
        {
            return _dbConnection.Execute(procName, input, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Sửa dữ liệu theo procedure có tên procName với đầu vào là input
        /// </summary>
        /// <param name="procName">Tên procedure</param>
        /// <param name="input">Object chứa các biến truyền vào</param>
        /// <returns>Số dòng thay đổi</returns>
        public int Update(String procName ,T input)
        {
            return _dbConnection.Execute(procName, input, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xóa dữ liệu theo procedure có tên procName với đầu vào là id
        /// </summary>
        /// <param name="procName">Tên procedure</param>
        /// <param name="input">Object chứa proprety Id</param>
        /// <returns>Số dòng thay đổi</returns>
        public int Delete(String procName, Object id)
        {
            return _dbConnection.Execute(procName, id, commandType: CommandType.StoredProcedure);
        }

       

        //
        public TResult GetFirst<TResult>(String procName, Object input)
        {
            return _dbConnection.Query<TResult>(procName, input, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<TResult> GetList<TResult>(String procName, Object input)
        {
            return _dbConnection.Query<TResult>(procName, input, commandType: CommandType.StoredProcedure).ToList();
        }

        public int Change(String procName, Object input)
        {
            return _dbConnection.Execute(procName, input, commandType: CommandType.StoredProcedure);
        }
    }
}
