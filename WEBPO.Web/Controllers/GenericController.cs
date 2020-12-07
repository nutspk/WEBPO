using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEBPO.Core.ViewModels;

namespace WEBPO.Web.Controllers
{
    public class GenericController : Controller
    {
        public int defaultPageSize = 10;
        
        public RedirectToActionResult RedirectToAction<T>(string ActionName, object routeValues) where T : Controller
        {
            string controllerName = typeof(T).Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            return RedirectToAction(ActionName, controllerName, routeValues);
        }
        public RedirectToActionResult RedirectToAction<T>(string ActionName) where T : Controller
        {
            return RedirectToAction<T>(ActionName, null);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public ActionResult RedirectToPageError(string message)
        {
            return RedirectToAction("Index", "Error", new { message = message });
        }


        public List<SelectListItem> GetPageSize()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value="100", Text= "100" },
            };
        }
        protected string GetIPAddress()
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

}
