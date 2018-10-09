using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSharp
{
    public class ActionFilter : IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("action执行之后");


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            Console.WriteLine("action执行之前");
        }
    }
}