using DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp4.Shared.Services
{
    public interface IAPIGateway
    {
        Task<bool> postloginData(UserInfo userInfo);

        Task<string> GetToken(string ApiKey);

        Task<string> GetAServiceValue();

    }
}
