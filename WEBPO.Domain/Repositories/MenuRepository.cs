
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WEBPO.Domain.Repositories
{
    public class MenuRepository : Repository<MS_MNFUNC>, IRepository<MS_MNFUNC>
    {
        public MenuRepository(AppDbContext dbContext) : base(dbContext) {}

        public async Task<IEnumerable<MS_MNFUNC>> GetMenuList(string roleText) {
            IQueryable<MS_MNFUNC> query = dbSet;

            query = query.Include(c => c.MsMnCat)
                .Where(f => f.IStatus == "Y" && f.ISysName == "WEBPO")
                .Where(f => f.MsMnUsrs.Any(u => u.IUserType == roleText))
                .OrderBy(f => f.MsMnCat.ISort)
                .ThenBy(f => f.ISort);
               
            return await query.AsNoTracking().ToListAsync();

        }
    }
}
