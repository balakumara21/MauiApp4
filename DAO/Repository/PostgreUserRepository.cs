using DAO.Models;
using DAO.Repository;
using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public class PostgreUserRepository : IRepository
    {
        private readonly QueryFactory _db;

        public PostgreUserRepository(string connectionString, Compiler compiler)
        {
            var connection = new SqlConnection(connectionString);
            _db = new QueryFactory(connection, compiler);
        }

        public async Task AddUser(UserInfo userInfo)
        {
            var query = await _db.Query("UserInfo").InsertAsync(userInfo);
        }

        public async Task<UserInfo> GetUser(UserInfo userInfo)
        {
            var result = await _db.Query("UserInfo").GetAsync<UserInfo>();
            var user = result.Where(u => u.Username == userInfo.Username && u.Password == userInfo.Password).FirstOrDefault();

            return user;
        }

    }
}
