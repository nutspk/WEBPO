using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.ViewModels;

namespace WEBPO.Core.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuViewModel>> GetMenusAsync(string roleText);
    }
}
