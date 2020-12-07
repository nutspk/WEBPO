using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Entities;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Core.Interfaces
{
    public interface IPublicMessageService
    {
        Task<IPagedList<TR_PUB>> GetAllMessage(DateTime? datePublic, DataTablesRequest dtReq);
        Task<IPagedList<TR_PUB>> GetActiveMessage(string vendorCode, DataTablesRequest dtReq);
    }
}
