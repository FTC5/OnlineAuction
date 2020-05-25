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
    public class LotNotFoundExaptionAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple
        {
            get { return true; }
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Exception exception;

            if (actionExecutedContext.Exception != null &&
                    actionExecutedContext.Exception is LotNotFoundExaption)
            {
                exception = actionExecutedContext.Exception;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, exception.Message);
                actionExecutedContext.Exception = null;
                exception = null;
            }
            return Task.FromResult<object>(null);
        }
    }
}