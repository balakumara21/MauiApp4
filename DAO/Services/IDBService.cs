using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public interface IDBService
    {
        QueryFactory DB { get; }

    }
}
