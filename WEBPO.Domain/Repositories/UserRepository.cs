
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Domain.UnitOfWork.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WEBPO.Domain.Repositories
{
    public class UserRepository : Repository<MS_USER>, IRepository<MS_USER>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) {}

        public async Task<IPagedList<MS_USER>> GetUserList(string userId = null, string userName= null, string email = null, string vendorCode = null,
                                                            Func<IQueryable<MS_USER>, IOrderedQueryable<MS_USER>> orderBy = null, int pageIndex = 0, int pageSize = 20)
        {
            IQueryable<MS_USER> query = dbSet;

            query = query.AsNoTracking().Include(c => c.MS_VS);

            if (!string.IsNullOrEmpty(userId)) query = query.Where(x => x.IUserId == userId);
            if (!string.IsNullOrEmpty(userName)) query = query.Where(x => x.IUserName.Contains(userName));
            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.IMail.Contains(email));
            if (!string.IsNullOrEmpty(vendorCode)) query = query.Where(x => x.IVsCd == vendorCode);

            if (orderBy != null)
                return await orderBy(query).ToPagedListAsync(pageIndex, pageSize, 1);
            else
                return await query.OrderBy(x => x.IUserName).ThenBy(x => x.IUserType).ThenBy(x => x.IVsCd).ToPagedListAsync(pageIndex, pageSize, 1);
        }

        public async Task<MS_USER> GetUserById(string userId)
        {
            return await dbSet.AsNoTracking().Where(x=>x.IUserId == userId).Include(c => c.MS_VS).FirstOrDefaultAsync();
        }
    }
}
