using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public interface IDatabaseFactory
    {
        IRepository CreateUserRepository();
    }
}
