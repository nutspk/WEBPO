using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Persistances;
using WEBPO.Core.Services;
using WEBPO.Core.ViewModels;

namespace WEBPO.Web.Controllers.System
{
    public class PublicMessageController : GenericController
    {

        private readonly IPublicMessageService pmService;
        private readonly IUserResolverService user;
        public PublicMessageController(IPublicMessageService pmService, IUserResolverService user)
        {
            this.pmService = pmService;
            this.user = user;
        }

        // GET: PublicMessageController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (user.IsAdmin) {
                return View(nameof(Admin));
            }  else {
                return View();
            }
                
        }

        [Authorize(Roles= ROLE.Admin)]
        public async Task<IActionResult> Admin()
        {
            return View();
        }

        [HttpGet("api/[controller]/GetPublicMessage")]
        public async Task<IActionResult> GetPublicMessage(string vendorCode, DateTime? date, DataTablesRequest dtReq) {

            var msgList = await pmService.GetActiveMessage(vendorCode, dtReq);
            return Json(new DataTablesResponse(dtReq.draw, msgList.Items, msgList.TotalCount, msgList.TotalCount).ToJson());
        }

        // GET: PublicMessageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublicMessageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublicMessageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublicMessageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PublicMessageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublicMessageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublicMessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
