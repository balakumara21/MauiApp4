using DAO.Repository;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public class SqlServerDatabaseFactory : IDatabaseFactory
    {
        private readonly string _connection;

        public SqlServerDatabaseFactory(string connection)
        {
            _connection = connection;
        }

        public IRepository CreateUserRepository()
        {
            return new SqlUserRepository(
                _connection,
                new SqlServerCompiler()
            );
        }
    }
}
