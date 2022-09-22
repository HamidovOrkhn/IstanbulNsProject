using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IstanbulNsApp.Filters
{
    public class RefreshApiToken : Attribute, IActionFilter
    {
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    Console.WriteLine("Api Token Middleware");       
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (context.HttpContext.Request.Cookies["UserKey"]!=null)
        //    {

        //        if (context.HttpContext.Request.Cookies["jwtToken"] == null)
        //        {
        //            HttpClient client = new HttpClient();
        //            client.BaseAddress = new Uri("http://localhost:5000");
        //            var resp = client.GetAsync("/api/v1/users/refresh/" + context.HttpContext.Request.Cookies["refreshToken"]).Result;

        //            if (resp.IsSuccessStatusCode)
        //            {
        //                var response = resp.Content.ReadAsAsync<TokenResponse>().Result;
        //                context.HttpContext.Session.SetString("JwtSession", response.jwtToken);
        //                context.HttpContext.Response.Cookies.Append("refreshToken", response.token, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
        //                context.HttpContext.Response.Cookies.Append("jwtToken", response.jwtToken, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMinutes(1) });

        //            }


        //        }
        //        else
        //        {
        //            if (context.HttpContext.Session.GetString("JwtSession") == null)
        //            {
        //                context.HttpContext.Session.SetString("JwtSession", context.HttpContext.Request.Cookies["jwtToken"]);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        context.Result = new RedirectToRouteResult(
        //           new RouteValueDictionary
        //           {
        //            { "controller", "Home" },
        //            { "action", "Index" }
        //           });
        //    }


        //if (context.HttpContext.Request.Cookies["UserKey"] == null)
        //{
        //    context.Result = new RedirectToRouteResult(
        //       new RouteValueDictionary
        //       {
        //        { "controller", "Home" },
        //        { "action", "Index" }
        //       });
        //}


        //}
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }

}

