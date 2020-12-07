using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<MS_VS>> GetAllVendor();
        Task<SelectList> GetVendorSelectList(string id = "");
        Task<MS_VS> FindVendor(string vendorCode, string vendorName = "");
        Task<int> Add(MS_VS entity);
        Task<int> Update(MS_VS entity);
        Task<int> UpdateById(string vendorCode, MS_VS entity);
        Task<int> Delete(string vendorCode);
    }
}
