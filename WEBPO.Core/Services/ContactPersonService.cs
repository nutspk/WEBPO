using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Domain.UnitOfWork;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Core.Services
{
    public class ContactPersonService : IContactPersonService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly ContactPersonRepository Repo;
        private readonly IUserResolverService user;
        public ContactPersonService(IUnitOfWork unitOfWork, IUserResolverService userResolverService)
        {
            user = userResolverService;
            UnitOfWork = unitOfWork;
            UnitOfWork.ActionBy(user.UserID);
            Repo = (ContactPersonRepository)UnitOfWork.GetRepository<MS_PIC>(hasCustomRepository: true);
        }

        public async Task<IEnumerable<MS_PIC>> GetAllPerson()
        {
            return await Repo.GetAll();
        }

        public async Task<MS_PIC> GetPersonById(int id)
        {
            int.TryParse(id.ToString(), out int contactid);
            return await Repo.GetFirstOrDefaultAsync(x => x.IPicId == contactid);
        }

        public async Task<IPagedList<MS_PIC>> FindPerson(ContactPersonViewModel search, DataTablesRequest dtReq)
        {
            return await Repo.GetContactList(contactName: search.ContactName, vendorCode: search.VendorCode, 
                                              department: search.DepartmentName, 
                                              pageIndex: dtReq.pageIndex, pageSize: dtReq.pageSize);
        }

        public async Task<int> Add(MS_PIC entity)
        {
            await Repo.Add(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> Update(MS_PIC entity)
        {
            Repo.Update(entity);
            return await UnitOfWork.SaveChangesAsync();
        }
        
        public async Task<int> Delete(MS_PIC entity)
        {
            Repo.Delete(entity);
            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteById(int id)
        {
            int.TryParse(id.ToString(), out int contactid);

            Repo.DeleteById(contactid);

            return await UnitOfWork.SaveChangesAsync();
        }

    }
}
