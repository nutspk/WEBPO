using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Persistances;
using WEBPO.Core.ViewModels;
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using WEBPO.Web.Filters;

namespace WEBPO.Web.Controllers.Master
{
    [Authorization(Roles = ROLE.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService usrService;
        private readonly IVendorService vsService;
        private readonly IMapper mapper;
        public UserController(IUserService usrService, IVendorService vsService, IMapper mapper)
        {
            this.usrService = usrService;
            this.vsService = vsService;
            this.mapper = mapper;
        }

        [HttpGet("api/[controller]/GetUsers")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUsers(UserViewModel userViewModel, DataTablesRequest dtReq)
        {
            var userList = await usrService.FindUsers(userViewModel, dtReq);
            var userVM = mapper.Map<IEnumerable<UserCreateViewModel>>(userList.Items);
            
            return Json(new DataTablesResponse(dtReq.draw, userVM, userList.TotalCount, userList.TotalCount).ToJson());
        }

        public async Task<IActionResult> Index()
        {
            ViewData["VendorList"] = await vsService.GetVendorSelectList();
            return View();
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewData["VendorList"]  = await vsService.GetVendorSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel userVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oUser = await usrService.GetUserById(userVM.UserID);
                    if (oUser != null)
                        return Json(JsonResponse.Error("That User Id already exists.", userVM.UserID, MessageInfoType.Warning));

                    var user = mapper.Map<MS_USER>(userVM);

                    var row = await usrService.Add(user);

                    if (row == 0)
                        return Json(JsonResponse.Success("No data created", null, MessageInfoType.Info));
                    else
                        return Json(JsonResponse.Success("The user was successfully created", null, MessageInfoType.Success));
                }

                return Json(JsonResponse.Error("Insufficient data", null, MessageInfoType.Error));

            } catch (Exception ex) {
                return Json(JsonResponse.Error(ex.InnerException.Message, null, MessageInfoType.Error));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id == null) return NotFound();


                var user = await usrService.GetUserById(id);
                if (user == null) return NotFound();
                

                var userVM = mapper.Map<UserCreateViewModel>(user);
                ViewData["VendorList"] = await vsService.GetVendorSelectList();

                return View(userVM);
            } catch (Exception) {
                return View(null);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserCreateViewModel userVM)
        {
            try
            {
                if (id != userVM.UserID)
                    return Json(JsonResponse.Error("User ID do not found", null, MessageInfoType.Error));

                if (ModelState.IsValid)
                {
                    var user = mapper.Map<MS_USER>(userVM);

                    var row = await usrService.Update(user);

                    if (row == 0)
                        return Json(JsonResponse.Success("No data updated", null, MessageInfoType.Info));
                    else
                        return Json(JsonResponse.Success("The user was successfully updated", null, MessageInfoType.Success));
                }

                return Json(JsonResponse.Error("Update Failed", null, MessageInfoType.Error));
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
                if (string.IsNullOrEmpty(id))
                    return Json(JsonResponse.Error("User ID Not found", null, MessageInfoType.Error));

                await usrService.Delete(id);
                return Json(JsonResponse.Success("Delete Successfully", id, MessageInfoType.Success));
            } catch (Exception ex)  {
                return Json(JsonResponse.Success(ex.Message, null, MessageInfoType.Error));
            }
        }

    }
}
