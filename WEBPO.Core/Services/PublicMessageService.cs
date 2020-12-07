using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Domain.UnitOfWork;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Core.Services
{
    public class PublicMessageService : IPublicMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PublicMessageRepository repo;
        public PublicMessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            repo = (PublicMessageRepository)_unitOfWork.GetRepository<TR_PUB>(hasCustomRepository: true);
        }

        public Task<IPagedList<TR_PUB>> GetAllMessage(DateTime? datePublic, DataTablesRequest dtReq) {

            var date = datePublic.GetValueOrDefault();
            if (datePublic != null) {
                return repo.GetPagedListAsync(e => e.IRegDate == date, pageIndex: dtReq.pageIndex, pageSize: dtReq.pageSize);
            } else {
                return repo.GetPagedListAsync(pageIndex: dtReq.pageIndex, pageSize: dtReq.pageSize);
            }
            
        }

        public async Task<IPagedList<TR_PUB>> GetActiveMessage(string vendorCode, DataTablesRequest dtReq)
        {
            IList<PublicMsgViewModel> vm = new List<PublicMsgViewModel>();
            
            var date = DateTime.Now;
            var data = await repo.GetMessageList(vendorCode: vendorCode, pageIndex: dtReq.pageIndex, pageSize: dtReq.pageSize);
            return data;

        }

    }
}
