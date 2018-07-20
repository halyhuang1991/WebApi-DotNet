using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace CSharp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSameDomain")]
    public class IndexController: Controller
    {
        [Authorize]
        [HttpPost]
        public ActionResult Test()
        {
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            if (!claimsIdentity.IsAuthenticated)
            {
                return Ok(new string[2] {"error", "未认证" });
            }else if(claimsIdentity==null){
                return Ok(new string[2] {"error", "未找到声明" });
            }
            else
            {
                IEnumerable<Claim> claim=claimsIdentity.Claims;
                return Ok(new string[2] {"ok", claimsIdentity.Name });
            }
        }

    }
}