using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Entities;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Core.Interfaces
{
    public interface IContactPersonService
    {
        Task<IEnumerable<MS_PIC>> GetAllPerson();
        Task<MS_PIC> GetPersonById(int id);
        Task<IPagedList<MS_PIC>> FindPerson(ContactPersonViewModel userSearch, DataTablesRequest dtReq);
        Task<int> Add(MS_PIC entity);
        Task<int> Update(MS_PIC entity);
        Task<int> Delete(MS_PIC entity);
        Task<int> DeleteById(int userId);
    }
}
