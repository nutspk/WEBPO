using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Domain.UnitOfWork;

namespace WEBPO.Core.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly IRepository<MS_USER> repo;
        private readonly IRepository<MS_VS> repo;

        public VendorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = unitOfWork.GetRepository<MS_VS>();
        }

        public Task<int> Add(MS_VS entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(string vendorCode)
        {
            throw new NotImplementedException();
        }

        public async Task<MS_VS> FindVendor(string vendorCode, string vendorName = "")
        {
            return await repo.GetByID(vendorCode);
        }

        public Task<IEnumerable<MS_VS>> GetAllVendor()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(MS_VS entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateById(string vendorCode, MS_VS entity)
        {
            throw new NotImplementedException();
        }

        public async Task<SelectList> GetVendorSelectList(string id = null)
        {
            if (string.IsNullOrEmpty(id)) {
                return new SelectList(await repo.GetAll(), nameof(MS_VS.IVsCd), nameof(MS_VS.VendorDescText), id);
            } else {
                return new SelectList(await repo.Find(x => x.IVsCd == id), nameof(MS_VS.IVsCd), nameof(MS_VS.VendorDescText), id);
            }
           
        }
    }
}
