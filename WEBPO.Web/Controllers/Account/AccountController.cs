using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WEBPO.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using WEBPO.Web.Controllers.System;
using WEBPO.Core.Interfaces;
using Microsoft.Extensions.Logging;
using WEBPO.Core.Persistances;

namespace WEBPO.Web.Controllers.Auth
{
    public class AccountController : GenericController
    {
        private readonly IUserService UserService;
        private readonly IUserResolverService UserResolverService;

        public AccountController(IUserService userService, IUserResolverService userResolverService)
        {
            UserService = userService;
            UserResolverService = userResolverService;
        }

        public IActionResult Index(string returnUrl = null)
        {
            if (UserResolverService.IsAuthenticated) {
                return RedirectToAction<PublicMessageController>(nameof(PublicMessageController.Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await UserService.AuthenticateUser(model.UserName, model.Password);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, user.IUserId ?? ""),
                        new Claim(ClaimTypes.Name, user.IUserName ?? ""),
                        new Claim(ClaimTypes.Email, user.IMail ?? ""),
                        new Claim(ClaimTypes.Role, user.RoleText ?? ""),
                        new Claim("VendorCode", user.IVsCd ?? ""),
                        new Claim("CompanyName", user.MS_VS.IVsDesc ?? "")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                return RedirectToAction(nameof(Index));
            } catch (Exception) {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAjax(LoginViewModel model)
        {
            string resp = string.Empty;
            try
            {
                var user = await UserService.AuthenticateUser(model.UserName, model.Password);

                if (user == null)
                    return Json(JsonResponse.Error("Invalid Username or Password", null, MessageInfoType.Error));

                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.IUserId ?? ""),
                    new Claim(ClaimTypes.Name, user.IUserName ?? ""),
                    new Claim(ClaimTypes.Email, user.IMail ?? ""),
                    new Claim(ClaimTypes.Role, user.RoleText ?? ""),
                    new Claim("VendorCode", user.IVsCd ?? ""),
                    new Claim("CompanyName", user.MS_VS.IVsDesc ?? "")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                resp = JsonResponse.Success("Login Successfully", null, MessageInfoType.Success);

            } catch (Exception ex) {
                resp = JsonResponse.Error("Login Failure", ex.ToString(), MessageInfoType.Error);
            }
            return Json(resp);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ForgetPassword(string param, string verify)
        {
            bool valid = string.IsNullOrEmpty(param) || string.IsNullOrEmpty(verify);

            try
            {
                var userId = Project.DecodeBase64(param);

                ViewBag.Error = !Project.Decrypt(userId, verify);
                if (ViewBag.Error) return View();

                var model = new ChangePasswordViewModel() { 
                    UserId = userId,
                };

                return View(model);

            } catch (Exception ex) {
                valid = false;
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost("api/[controller]/ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordAsync(ChangePasswordViewModel passVM)
        {
            try
            {
                if (ModelState.IsValid) {
                    var user = await UserService.GetUserById(passVM.UserId);

                    if (user == null)
                        return Json(JsonResponse.Error("Invalid User", null, MessageInfoType.Error));

                    if (string.IsNullOrEmpty(user.IResetPin) || user.IResetPin != passVM.PinCode)
                        return Json(JsonResponse.Error("Invalid Pin Code", null, MessageInfoType.Error));


                    user.IUserPwd = BCrypt.Net.BCrypt.HashPassword(passVM.NewPassword);
                    user.IResetPin = null;
                    var row = await UserService.Update(user);

                    if (row == 0)
                        return Json(JsonResponse.Success("No data updated", null, MessageInfoType.Info));
                    else
                        return Json(JsonResponse.Success("Your password was successfully changed", null, MessageInfoType.Success));
                }

                return Json(JsonResponse.Error("Insufficient data", null, MessageInfoType.Error));

            }
            catch (Exception ex)
            {
                return Json(JsonResponse.Error(ex.InnerException.Message, null, MessageInfoType.Error));
            }

        }

        [HttpPost("api/[controller]/ForgetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPasswordAsync(string userId)
        {
            try
            {
                var user = await UserService.GetUserById(userId);

                if (user == null) return Json(JsonResponse.Error("Invalid User name", null, MessageInfoType.Error));

                if (string.IsNullOrEmpty(user.IMail)) 
                        return Json(JsonResponse.Error(@"Your email not found. 
                                                        Please contact your system administrator", null, MessageInfoType.Error));

                var resp = await UserService.ForgetPassword(user);
                if (resp)
                    return Json(JsonResponse.Success(@"Password Reset request was send successfully. 
                                                Please check your email to reset your password", null, MessageInfoType.Success));
                else
                    return Json(JsonResponse.Error(null, "", MessageInfoType.Error));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("api/[controller]/ChangePassword")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel passVM)
        {

            try
            {
                if (string.IsNullOrEmpty(passVM.OldPassword) || string.IsNullOrEmpty(passVM.NewPassword) ||
                    (passVM.NewPassword != passVM.ConfirmPassword))
                {
                    return Json(JsonResponse.Error("Confirm Password do not match", null, MessageInfoType.Error));
                }

                var user = await UserService.AuthenticateUser(UserResolverService.UserID, passVM.OldPassword);

                if (user == null) 
                    return Json(JsonResponse.Error("Invalid User or Password", null, MessageInfoType.Error));

                user.IUserPwd = BCrypt.Net.BCrypt.HashPassword(passVM.NewPassword);
                user.IResetPin = null;
                var row = await UserService.Update(user);

                if (row == 0)
                    return Json(JsonResponse.Success("No data updated", null, MessageInfoType.Info));
                else
                    return Json(JsonResponse.Success("Your password was successfully changed", null, MessageInfoType.Success));
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.Error(ex.InnerException.Message, null, MessageInfoType.Error));
            }

        }

    }
}
