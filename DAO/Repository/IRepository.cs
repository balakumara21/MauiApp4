using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Repository
{
    public interface IRepository
    {
        Task AddUser(UserInfo userInfo);

        Task<UserInfo> GetUser(UserInfo userInfo);

      
    }
}
