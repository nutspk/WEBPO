using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Domain.UnitOfWork;

namespace EDI.Controllers.Shared
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMenuService menuService;
        private readonly IUserResolverService user;
        public SideBarViewComponent(IUnitOfWork unitOfWork, IMenuService menuService, IUserResolverService user)
        {
            this.unitOfWork = unitOfWork;
            this.menuService = menuService;
            this.user = user;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var repo = await menuService.GetMenusAsync(user.RoleText);
            return View(repo);
        }
    }
}
