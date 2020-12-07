using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Core.Interfaces
{
    public interface IUserService
    {
        Task<MS_USER> AuthenticateUser(string username, string password);

        Task<bool> ForgetPassword(MS_USER entity);
        Task<IEnumerable<MS_USER>> GetAllUser();
        Task<MS_USER> GetUserById(string userId);
        Task<IPagedList<MS_USER>> FindUsers(UserViewModel userSearch, DataTablesRequest dtReq);
        Task<int> Add(MS_USER entity);
        Task<int> Update(MS_USER entity);
        Task<int> Delete(string userId);
    }
}
