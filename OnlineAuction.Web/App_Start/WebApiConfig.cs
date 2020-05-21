using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;

namespace OnlineAuction.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "IdAndId",
                routeTemplate: "api/{controller}/{id1}/{id2}",
                defaults: new { },
                constraints: new
                {
                    id1 = new IntRouteConstraint(),
                    id2 = new IntRouteConstraint()
                }
            );
        }
    }
}
