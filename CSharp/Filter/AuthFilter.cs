using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSharp.Filter
{
    public class AuthFilter : AuthorizeFilter, IAllowAnonymousFilter
    {
        public  AuthFilter(AuthorizationPolicy policy) :base(policy)
        {
            this.Policy = Policy;
        }
        public AuthorizationPolicy Policy { get; }
        public bool IsHaveAllow(IList<IFilterMetadata> filers)
        {
            for (int i = 0; i < filers.Count; i++)
            {
                if (filers[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }
            return false;

        }
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //解析url
            // {/ Home / Index}
            var url = context.HttpContext.Request.Path.Value;
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            var list = url.Split("/");
            if (list.Length <= 0 || url == "/")
            {
                return;
            }
            var controllerName = list[1].ToString().Trim();
            var actionName = list[2].ToString().Trim();
            //验证
            var flag = false;
            if (flag)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}