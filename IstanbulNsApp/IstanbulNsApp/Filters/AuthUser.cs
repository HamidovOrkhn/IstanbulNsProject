using IstanbulNsApp.Resources.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IstanbulNsApp.Resources.Enums.RoleEnum;

namespace IstanbulNsApp.Filters
{
    public class AuthUser : Attribute, IActionFilter
    {
        int UserType;
        public AuthUser(int utype)
        {
            UserType = utype;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("UserFilter");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (UserType == (int)Roles.Admin)
            {
                if (context.HttpContext.Session.GetString("AdminKey") is null)
                {
                    context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Home" },
                    { "action", "Index" },
                       { "area",""}
                   });
                }
            }
            else
            {
                if (context.HttpContext.Session.GetString("SubAdminKey") is null && context.HttpContext.Session.GetString("AdminKey") is null)
                {
                    context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Home" },
                    { "action", "Index" },
                        { "area",""}
                   });
                }
            }

        }
    }
}
