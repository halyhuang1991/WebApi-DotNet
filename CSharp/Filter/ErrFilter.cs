using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSharp.Filter
{
    public class ErrFilter:IExceptionFilter, IFilterMetadata
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
           if(filterContext.ExceptionHandled == false)
            {
                string msg = filterContext.Exception.Message;
                filterContext.Result = new ContentResult
                {
                    Content = msg,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "text/html;charset=utf-8"
                };
            }
            filterContext.ExceptionHandled = true; //异常已处理了
        }
    }
}