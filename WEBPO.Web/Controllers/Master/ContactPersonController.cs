using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WEBPO.Core.Interfaces;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using WEBPO.Web.Filters;

namespace WEBPO.Web.Controllers.Master
{
    public class ContactPersonController : Controller
    {
        private readonly IContactPersonService ContPersonService;
        private readonly IVendorService VsService;
        private readonly IMapper Mapper;
        private readonly IUserResolverService UserResolver;

        public ContactPersonController(IContactPersonService contrService, IVendorService vsService, 
                                        IMapper mapper, IUserResolverService userResolver)
        {
            ContPersonService = contrService;
            VsService = vsService;
            Mapper = mapper;
            UserResolver = userResolver;
        }

        private async Task InitCtrlAsync() {
            if (UserResolver.IsAdmin)  {
                ViewData["VendorList"] = await VsService.GetVendorSelectList();
            } else {
                var select = await VsService.GetVendorSelectList(UserResolver.VendorCode);
                ViewData["VendorList"] = select;
            }
        }


        [HttpGet("api/[controller]/GetPersons")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetContactPerson(ContactPersonViewModel personViewModel, DataTablesRequest dtReq)
        {
            var personList = await ContPersonService.FindPerson(personViewModel, dtReq);
            var personVM = Mapper.Map<IEnumerable<ContactPersonCreateViewModel>>(personList.Items);

            return Json(new DataTablesResponse(dtReq.draw, personVM, personList.TotalCount, personList.TotalCount).ToJson());
        }

        public async Task<IActionResult> Index()
        {
            await InitCtrlAsync();

            return View();
        }

        public async Task<IActionResult> CreateAsync()
        {
            await InitCtrlAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactPersonCreateViewModel personVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var person = Mapper.Map<MS_PIC>(personVM);

                    var row = await ContPersonService.Add(person);

                    if (row == 0)
                        return Json(JsonResponse.Success("No data created", null, MessageInfoType.Info));
                    else
                        return Json(JsonResponse.Success("The contact person was successfully created", null, MessageInfoType.Success));
                }

                return Json(JsonResponse.Error("Insufficient data", null, MessageInfoType.Error));

            }
            catch (Exception ex)
            {
                return Json(JsonResponse.Error(ex.Message, null, MessageInfoType.Error));
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            int.TryParse(id, out int conId);
            var person = await ContPersonService.GetPersonById(conId);

            if (person == null) return NotFound();

            var personVM = Mapper.Map<ContactPersonCreateViewModel>(person);

            await InitCtrlAsync();

            return View(personVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactPersonCreateViewModel personVM)
        {
            try
            {
                if (id.ToString() != personVM.ContactId)
                    return Json(JsonResponse.Error("The contact person do not found", null, MessageInfoType.Error));

                if (ModelState.IsValid)
                {
                    var user = Mapper.Map<MS_PIC>(personVM);

                    var row = await ContPersonService.Update(user);

                    if (row == 0)
                        return Json(JsonResponse.Success("No data updated", null, MessageInfoType.Info));
                    else
                        return Json(JsonResponse.Success("The user was successfully updated", null, MessageInfoType.Success));
                }

                return Json(JsonResponse.Error("Insufficient data", null, MessageInfoType.Error));
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.Error(ex.Message, null, MessageInfoType.Error));
            }
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                
                if (string.IsNullOrEmpty(id.ToString()))
                    return Json(JsonResponse.Error("Contact person Not found", null, MessageInfoType.Error));

                int.TryParse(id, out int conId);
                var row = await ContPersonService.DeleteById(conId);

                if (row == 0)
                    return Json(JsonResponse.Success("No data deleted", null, MessageInfoType.Info));
                else
                    return Json(JsonResponse.Success("The contact person was successfully deleted", null, MessageInfoType.Success));

            }
            catch (Exception ex)
            {
                return Json(JsonResponse.Success(ex.Message, null, MessageInfoType.Error));
            }
        }

    }
}
