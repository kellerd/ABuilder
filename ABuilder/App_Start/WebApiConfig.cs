using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ABuilder
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "SingleModelStat",
                routeTemplate: "api/SingleModel_Stat/{SingleModelId}/{StatId}",
                defaults: new { controller = "SingleModel_Stat", SingleModelId = RouteParameter.Optional, StatId  = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
