using OnlineAuction.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace OnlineAuction.Web.ExceptionFilters
{
    public class ValidationExceptionAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple
        {
            get { return true; }
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Exception exception;
            if (actionExecutedContext.Exception != null &&
                    actionExecutedContext.Exception is BLL.Infrastructure.ValidationException)
            {
                exception = actionExecutedContext.Exception;//="<br/>"
                string str= exception.Message + exception.ToString();
                str = str.Replace("\n", "<br />");
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, str);
                actionExecutedContext.Exception = null;
                exception = null;
            }
            return Task.FromResult<object>(null);
        }
      
    }
}