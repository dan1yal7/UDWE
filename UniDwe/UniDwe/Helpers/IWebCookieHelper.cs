using Microsoft.AspNetCore.Mvc;
using UniDwe.Migrations;
using UniDwe.Repositories;
using UniDwe.Session;

namespace UniDwe.Helpers
{
    public interface IWebCookieHelper
    {
        void AddSecure(string cookieName, string value, int days = 0);
        string? Get(string cookieName);
    }

    public class WebCookieHelper : IWebCookieHelper
    { 
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebCookieHelper(IHttpContextAccessor httpContexAccessor)
        {
            _httpContextAccessor = httpContexAccessor;
        }
        public void AddSecure(string cookieName, string value, int days = 0)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            if (days > 0) { options.Expires = DateTimeOffset.UtcNow.AddDays(days); }
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        public string? Get(string cookieName)
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies.FirstOrDefault(m => m.Key == cookieName);
            if (cookie != null && cookie.Value.Value != null)
            {
              return cookie.Value.Value;
            }
            return null;
        }
    }
}
