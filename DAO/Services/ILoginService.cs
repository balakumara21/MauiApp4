using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public interface ILoginService
    {
        Task<UserInfo> GetUser(UserInfo userInfo);
    }
}
