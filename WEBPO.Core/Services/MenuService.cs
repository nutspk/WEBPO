using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Domain.UnitOfWork;

namespace WEBPO.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MenuRepository repo;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            repo = (MenuRepository)_unitOfWork.GetRepository<MS_MNFUNC>(hasCustomRepository: true);
        }

        public async Task<IEnumerable<MenuViewModel>> GetMenusAsync(string roleText) {
            IList<MenuViewModel> menus = new List<MenuViewModel>();
            var menuList = await repo.GetMenuList(roleText);
            if (menuList != null) {
                var categories = menuList.GroupBy(x => x.ICatId)
                    .Select(x => new { 
                        CategoryId = x.Key,
                        CategoryName = x.Max(m => m.MsMnCat.ICatName) 
                    }).ToList();

                foreach (var categorie in categories)
                {
                    menus.Add(new MenuViewModel()
                    {
                        MenuId = categorie.CategoryId,
                        MenuName = categorie.CategoryName,
                        MenuList = menuList.Where(f => f.ICatId == categorie.CategoryId).ToList()
                    });
                }
            }
            return menus;
        }

    }
}
