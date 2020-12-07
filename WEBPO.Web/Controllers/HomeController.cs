using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WEBPO.Domain.UnitOfWork;
using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WEBPO.Web.Models;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Services;
using WEBPO.Core.Persistances;
using System.Reflection;
using WEBPO.Core.ViewModels;
using Newtonsoft.Json;

namespace WEBPO.Web.Controllers
{
    public class HomeController : GenericController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMenuService menuService;
        private readonly IUserResolverService user;

        public HomeController(IUnitOfWork unitOfWork, IMenuService menuService, ILogger<HomeController> logger, IUserResolverService user)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.menuService = menuService;
            this.user = user;
        }

        public async Task<IActionResult> Index()
        {
            var repo = await menuService.GetMenusAsync(user.RoleText);

            //var admin = Project.GetUser();

            //string message = "";

            //foreach (PropertyInfo property in admin.GetType().GetProperties())
            //{
            //    var Key = property.Name;
            //    var Value = property.GetValue(admin, null);
            //    message += string.Format("{0} = {1} ", Key, Value);
            //}

            string foo = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            string fullUrl = string.Format("{0}://{1}{2}{3}", Request.Scheme, 
                                            Request.Host.Value, Request.PathBase, $"ChangePassword/verify?token=");
            ViewBag.Message = fullUrl;

            var enc = Project.EncodeBase64("user");
            var token = Project.Encrypt("user");
            
            ViewBag.Message = $"token is = { token }  <br/> enc is = {enc}";

            //ViewBag.Message = message;
            return View(repo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/[controller]/Error/{code:int}")]
        public IActionResult Error(string code)
        {
            ViewBag.StatusCode = code;
            return View();
        }
    }
}
