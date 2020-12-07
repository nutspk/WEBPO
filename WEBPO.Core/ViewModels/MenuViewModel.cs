using System;
using System.Collections.Generic;
using System.Text;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.ViewModels
{
    public class MenuViewModel
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public ICollection<MS_MNFUNC> MenuList { get; set; } = new HashSet<MS_MNFUNC>();
    }
}
