
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBPO.Domain.UnitOfWork.Collections;
using System.Threading;

namespace WEBPO.Domain.Repositories
{
    public class PublicMessageRepository : Repository<TR_PUB>, IRepository<TR_PUB>
    {
        public PublicMessageRepository(AppDbContext dbContext) : base(dbContext) {}

        public async Task<IPagedList<TR_PUB>> GetMessageList(int pmNo = 0, string vendorCode = "", DateTime? pmDate = null, DateTime? startDate = null,
                                                                DateTime? endDate = null, int pageIndex =0, int pageSize=20)
        {
            IQueryable<TR_PUB> query = dbSet;

            //query = query.Include(c => c.MsVs);

            query = from t in query.AsNoTracking()
                    join v in dbContext.MsVendor on t.IVsCd equals v.IVsCd into vt
                    from t1 in vt.DefaultIfEmpty()
                    select new TR_PUB {
                        IPubNo = t.IPubNo,
                        IVsCd = t.IVsCd,
                        IRegDate = t.IRegDate,
                        ISubject = t.ISubject,
                        IMessage = t.IMessage,
                        IStartDate = t.IStartDate,
                        IEndDate = t.IEndDate,
                        IUserId = t.IUserId,
                        IReadFlg = t.IReadFlg,
                        IAllFlg = t.IAllFlg,
                        IEntryDate = t.IEntryDate,
                        IUpdDate = t.IUpdDate,
                        IUpdUserId = t.IUpdUserId,
                        MsVs = t1
                    };

            query = query.Where(x => x.IVsCd == vendorCode || x.IVsCd == "");

            if (pmDate != null)
                query = query.Where(x => x.IRegDate == pmDate.GetValueOrDefault());

            if (startDate != null) {
                if (endDate == null) endDate = startDate;
                query = query.Where(x => x.IStartDate.GetValueOrDefault() >= startDate.GetValueOrDefault() 
                    && x.IStartDate.GetValueOrDefault() >= endDate.GetValueOrDefault());
            }
                
            if (pmNo > 0) 
                query = query.Where(x => x.IPubNo == pmNo);

            return await query.OrderBy(x => x.IRegDate).ToPagedListAsync(pageIndex, pageSize, 1);

            //return await query.AsNoTracking().OrderBy(x=>x.IRegDate).Skip(pageSize * (pageIndex)).Take(pageSize).ToListAsync();

        }

    }
}
