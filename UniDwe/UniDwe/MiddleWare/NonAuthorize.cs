﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UniDwe.Services;

namespace UniDwe.MiddleWare
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class NonAuthorize : Attribute, IAsyncAuthorizationFilter
    {
        public NonAuthorize()
        {

        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            ICurrentUserService? currentUser = filterContext.HttpContext.RequestServices.GetService<ICurrentUserService>();
            if (currentUser == null)
            {
                throw new Exception("No user middleware");
            }

            bool isLogged = await currentUser.IsLoggedIn();
            if (isLogged)
            {
                filterContext.Result = new RedirectResult("/");
                return;
            }
        }
    }
}
