using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services
{
    public static class DBServiceFactory
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string dbtype, string dbconnection)
        {
            if(dbtype=="MSSql")
            {
                services.AddSingleton<IDBService>(new SQLServerDBServices(dbconnection));
                
            }
            else if (dbtype == "Mongo")
            {
                services.AddSingleton<IDBService>(new MongoServerDBServices(dbconnection));

            }
            return services;
        }
    }
}
