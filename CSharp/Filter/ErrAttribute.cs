using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSharp.Filter
{
    public class ErrAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            // 构建错误信息对象
            var dic = new Dictionary<string, object>
            {
                ["err_code"] = 80250,
                ["err_msg"] = ex.Message,
                ["err_sol"] = ""
            };
            // 设置结果
            context.Result = new JsonResult(dic);
            context.ExceptionHandled = true;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}