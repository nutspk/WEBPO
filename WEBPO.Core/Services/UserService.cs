using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.UnitOfWork.Collections;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;
using WEBPO.Core.Persistances;
using Newtonsoft.Json;

namespace WEBPO.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IEmailService EmailService;
        //private readonly IRepository<MS_USER> Repo;
        private readonly UserRepository repo;
        private readonly ILogger<UserService> Logger;

        public UserService(IUnitOfWork unitOfWork, IRepository<MS_USER> repository, IEmailService emailService, ILogger<UserService> logger) {
            UnitOfWork = unitOfWork;
            repo = (UserRepository)unitOfWork.GetRepository<MS_USER>(hasCustomRepository: true);
            EmailService = emailService;
            Logger = logger;
        }
        public async Task<MS_USER> AuthenticateUser(string username, string password)
        {
            //var user = await repo.Find(x => 
            //    x.IUserId.ToLower() == username.ToLower() && 
            //    x.IUserPwd.ToLower() == password.ToLower(), null, nameof(MS_USER.MS_VS));
            var user = await repo.GetFirstOrDefaultAsync(x =>
                 x.IUserId.ToLower() == username.ToLower(), includeProperties: nameof(MS_USER.MS_VS));

             if (user == null || !BC.Verify(password, user.IUserPwd)) return null;
            
            return user;
        }

        public async Task<MS_USER> GetUserById(string userId)
        {
            return await repo.GetUserById(userId);
        }

        public async Task<IEnumerable<MS_USER>> GetAllUser()
        {
            return await repo.GetAll();
        }

        public async Task<IPagedList<MS_USER>> FindUsers(UserViewModel userSearch, DataTablesRequest dtReq){
           return await repo.GetUserList(userSearch.UserId, userSearch.UserName, userSearch.EmailAddress, userSearch.VendorCode, 
               pageIndex: dtReq.pageIndex, pageSize: dtReq.pageSize);
        }

        public async Task<int> Add(MS_USER entity)
        {
           if (!string.IsNullOrEmpty(entity.IUserPwd))
                entity.IUserPwd = BC.HashPassword(entity.IUserPwd, "");

            await repo.Add(entity);

            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> Update(MS_USER entity)
        {
            repo.Update(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(string userId)
        {
            repo.DeleteById(userId);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ForgetPassword(MS_USER entity)
        {
            string html = string.Empty;
            var builder = new StringBuilder();
            var pinCode = string.Empty;
            var rnd = new Random();
            bool resp = false;

            if (entity == null || string.IsNullOrEmpty(entity.IMail)) return false;

            try
            {
                for (int i = 0; i < 3; i++) 
                    pinCode += string.Format("{0:00_}", rnd.Next(00, 99));
                

                entity.IResetPin = pinCode.Replace("_", "");

                var rowUpdate = await Update(entity);

                if (rowUpdate > 0)
                {
                    var Request = new Microsoft.AspNetCore.Http.HttpContextAccessor().HttpContext.Request;
   
                    string param = Project.EncodeBase64(entity.IUserId);
                    var enc = Project.Encrypt(entity.IUserId);

                    string fullUrl = string.Format("{0}://{1}{2}{3}", 
                                Request.Scheme, Request.Host.Value, Request.PathBase, $"/Account/ForgetPassword&param={param}?verify={enc}");
                    
                    builder.AppendLine($"Here are the details outlining your new password. <br/>");
                    builder.AppendLine($"USER ID :  	{ entity.IUserId } <br/>");
                    builder.AppendLine($"PIN CODE :  	{ pinCode.Replace("_", " ") } <br/>");
                    builder.AppendLine($"Here are the details outlining your new password. <br/>");
                    builder.AppendLine($" <a href='{ fullUrl }' ></a>");
                    
                    resp = await EmailService.SendForResetPassword(new List<string> { entity.IMail }, 
                                                                "Your Password has been reset", 
                                                                builder.ToString());

                    string respText = (resp) ? "Successfully" : "Failured";

                    Logger.LogInformation($"Send email [{respText}] to [{entity.IMail}] pin code is [{pinCode}] ");
                }
                else
                    Logger.LogInformation($"Can not update pin code [{pinCode}] for user [{entity.IUserId}]");

            } catch (Exception ex) {
                Logger.LogError(ex.Message);
            }

            
            return resp;
        }
    }
}
