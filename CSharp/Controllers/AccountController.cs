using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSharp.com;
using CSharp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSameDomain")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        {
            string usr=HttpContext.Request.Cookies["userName"];
            if(usr!=null){
                if(userName==""||userName==null){
                    userName=usr.ToString();
                }
            }
            string pass=HttpContext.Request.Cookies["password"];
            if(pass!=null){
                if(password==null||password==""){
                    password=pass.ToString();
                }
            }
            var user = userService.Login(userName, password);
            if (user != null)
            {

                user.AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(user);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (ReturnUrl==""||ReturnUrl==null)
                {
                    return RedirectToAction("index","Values", new { id = 1,type="4" });//到Values的get
                }
                if (HttpContext.Request.Method == "POST")
                {
                     return Ok(new string[] { "ok", "Try Again" });
                }
                return Redirect(ReturnUrl);
            }
            // ViewBag.Errormessage = "登录失败，用户名密码不正确";
            // return View();//Views/Accont/Login.cshtml
            
             return Ok(new string[] { "error", "not username" });
        }
         [HttpPost]
        public async Task<IActionResult> Logout(string returnurl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(returnurl ?? "~/");
        }
    }
}