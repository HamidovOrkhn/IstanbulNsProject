using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Filters;
using IstanbulNsApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using IstanbulNsApp.Libs;
using static IstanbulNsApp.Resources.Enums.RoleEnum;
using IstanbulNsApp.Areas.Admin.ViewModels;

namespace IstanbulNsApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _fc;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UserController(IHttpClientFactory fc, IStringLocalizer<SharedResource> localizer)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _localizer = localizer;
        }
        public IActionResult Login()
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("az-Latn-AZ")),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            return View();
        }
        [Validate]
        public IActionResult LoginConfirm([FromForm]UserLogin request)
        {
            var resp = new ServiceNode<UserLogin, LoginResp>(_localizer, _fc).PostClient(request, "/api/user/login");
            if (resp.IsCatched == 1)
            {
                TempData["UserError"] = resp.Message.ToString();
                return RedirectToAction("Login");
            }
            #region Create Sessions and Cookies
            LoginResp data = resp.Data;
            HttpContext.Session.SetString("jwttoken",data.jwtToken);
            HttpContext.Session.SetString("token", data.userData.token);
            HttpContext.Response.Cookies.Append("jwttoken", data.jwtToken, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMinutes(3) } );
            HttpContext.Session.SetString("UserId", data.userData.id.ToString());
            HttpContext.Session.SetString("Name", data.userData.name);
            HttpContext.Session.SetString("Email", data.userData.email);
            HttpContext.Session.SetString("SexId", data.userData.sexId.ToString());
            if (data.userData.role == 1)
            {
                HttpContext.Session.SetString("AdminKey", data.userData.id.ToString());               
            }
            else
            {
                HttpContext.Session.SetString("SubAdminKey", data.userData.id.ToString());
            }
            #endregion
            return RedirectToAction("Index","Home", new { Area = "Admin"});
        }

        [AuthUser((int)Roles.Admin)]
        public IActionResult Users()
        {
            List<User> users  = new ServiceNode<object, List<User>>(_fc).GetClient("/api/user/all").Data;
            return View(users);
        }
        [AuthUser((int)Roles.Admin)]
        public IActionResult Registrate()
        {
           // List<User> users = new ServiceNode<object, List<User>>(_fc).GetClient("/api/user/all").Data;
            return View();
        }
        [AuthUser((int)Roles.Admin)]
        [HttpPost]
        public IActionResult AddUser([FromForm] UserViewModel request)
        {
            User model = request;
            var resp = new ServiceNode<User, object>(_localizer, _fc).PostClient(model, "/api/user/register");
            if (resp.IsCatched == 1)
            {
                TempData["UserErrorRegistration"] = resp.Message.ToString();
                return RedirectToAction("Registrate");
            }
            TempData["R_Message_User"] = "Uğurlu Qeydiyyat";
            return RedirectToAction("Users");
        }
        [AuthUser((int)Roles.Admin)]
        public IActionResult DeActivate([FromQuery]int UserId)
        {
            var resp = new ServiceNode<object, object>(_fc).GetClient($"/api/user/disable/{UserId}").Data;
            TempData["R_Message_User"] = "User Deaktiv edildi";
            return RedirectToAction("Users");
        }
        [AuthUser((int)Roles.Admin)]
        public IActionResult Activate([FromQuery] int UserId)
        {
            var resp = new ServiceNode<object, object>(_fc).GetClient($"/api/user/enable/{UserId}").Data;
            TempData["R_Message_User"] = "User Aktivləşdirildi";
            return RedirectToAction("Users");
        }
        [AuthUser((int)Roles.Admin)]
        public IActionResult Delete([FromQuery]int UserId)
        {
            var resp = new ServiceNode<object, object>(_fc).GetClient($"/api/user/delete/{UserId}").Data;
            TempData["R_Message_User"] = "User Silindi";
            return RedirectToAction("Users");
        }






        [AuthUser((int)Roles.SubAdmin)]
        public IActionResult Logout()
        {
            #region Clear Sessions and Cookies
            HttpContext.Session.Remove("jwttoken");
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("SexId");
            HttpContext.Response.Cookies.Delete("jwttoken");
            if (HttpContext.Session.GetString("AdminKey") is object)
            {
                HttpContext.Session.Remove("AdminKey");
            }
            else
            {
                HttpContext.Session.Remove("SubAdminKey");
            }
            #endregion
            return RedirectToAction("Login");
        }
    }
}
