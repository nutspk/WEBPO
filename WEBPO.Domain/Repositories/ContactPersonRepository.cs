
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Domain.Repositories
{
    public class ContactPersonRepository : Repository<MS_PIC>, IRepository<MS_PIC>
    {
        public ContactPersonRepository(AppDbContext dbContext) : base(dbContext) {}

        public async Task<IPagedList<MS_PIC>> GetContactList(string contactId = null, string contactName = null, string vendorCode = null, string email = null, string department = null,
                                                            Func<IQueryable<MS_PIC>, IOrderedQueryable<MS_PIC>> orderBy = null, int pageIndex = 0, int pageSize = 20)
        {
            
            IQueryable<MS_PIC> query = dbSet;

            int.TryParse(contactId, out int id);

            query = query.AsNoTracking().Include(c => c.MsVs);
            if (!string.IsNullOrEmpty(contactId)) query = query.Where(x => x.IPicId == id);
            if (!string.IsNullOrEmpty(contactName)) query = query.Where(x => x.IPicName.Contains(contactName));
            if (!string.IsNullOrEmpty(department)) query = query.Where(x => x.ISectionCd.Contains(department));
            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.IMail.Contains(email));
            if (!string.IsNullOrEmpty(vendorCode)) query = query.Where(x => x.IVsCd == vendorCode);

            if (orderBy != null)
                return await orderBy(query).ToPagedListAsync(pageIndex, pageSize, 1);
            else
                return await query.OrderBy(x => x.IVsCd)
                    .ThenBy(x => x.IPicId).ThenBy(x => x.IPicName).ToPagedListAsync(pageIndex, pageSize, 1);
        }
    }
}
