using LearningEngine.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.AppFilters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BaseAppException)
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = 400
                };
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Unexpected server exception",
                    StatusCode = 500
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
