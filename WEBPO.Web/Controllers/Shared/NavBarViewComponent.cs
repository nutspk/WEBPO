using WEBPO.Domain.Data;
using WEBPO.Domain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Web.Filters;
using Microsoft.AspNetCore.Http;

namespace EDI.Controllers.Shared
{
    public class NavBarViewComponent : ViewComponent
    {
        private readonly IUserResolverService UserResolver;
        private readonly IHttpContextAccessor httpContext;
        public NavBarViewComponent(IUserResolverService userResolver, IHttpContextAccessor httpContext)
        {
            this.UserResolver = userResolver;
            this.httpContext = httpContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                if (UserResolver.IsAuthenticated) {
                    ViewBag.UserName = UserResolver.UserName;
                    ViewBag.CompanyName = UserResolver.CompanyName;
                };
            }
            catch (Exception) {

            }
            return View();
        }
    }

}
