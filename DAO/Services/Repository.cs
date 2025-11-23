using DAO.Models;
using DAO.Repository;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public class Repository : IRepository
    {

        private readonly IDBService _dB;

        public Repository(IDBService dB)
        {
            _dB = dB;
        }

        public async Task AddUser(UserInfo userInfo)
        {
            var query=await _dB.DB.Query("UserInfo").InsertAsync(userInfo);
        }

        public async Task<UserInfo> GetUser(UserInfo userInfo)
        {
            var result = await _dB.DB.Query("UserInfo").GetAsync<UserInfo>();
            var user= result.Where(u=>u.Username==userInfo.Username&&u.Password==userInfo.Password).FirstOrDefault();
   
            return user;
        }
    }
}
