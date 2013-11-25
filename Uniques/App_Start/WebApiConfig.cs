﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using Uniques.Library.Mvc;

namespace Uniques
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(name: "User Image", routeTemplate: "api/user/{id}/image/{name}", defaults: new { controller = "image" });
            config.Routes.MapHttpRoute(name: "User Authentication", routeTemplate: "api/user/authenticate", defaults: new { controller = "authenticate" });

            RegisterUserAttributesRoutes(config);
            RegisterUserAttributeValueRoutes(config);

            RegisterUserRoutes(config);

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "_api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }

        private static void RegisterUserRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "User by Id ",
                routeTemplate: "api/user/{id}",
                defaults: new { controller = "user" },
                constraints: new { id = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

            config.Routes.MapHttpRoute(
                name: "User by Loginname",
                routeTemplate: "api/user/{loginname}",
                defaults: new { controller = "user" },
                constraints: new { name = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });
        }

        private static void RegisterUserAttributesRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "User Attributes by Id",
                routeTemplate: "api/user/attributes/{id}",
                defaults: new { controller = "UserAttributes" },
                constraints: new { id = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) } );

            config.Routes.MapHttpRoute(
                name: "User Attributes by Name",
                routeTemplate: "api/user/attributes/{name}",
                defaults: new { controller = "UserAttributes" },
                constraints: new { name = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

            config.Routes.MapHttpRoute(
                name: "User Attributes without id",
                routeTemplate: "api/user/attributes",
                defaults: new { controller = "userattributes"});
        }

        private static void RegisterUserAttributeValueRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "User Attribute Values by Id",
                routeTemplate: "api/user/{id}/attributes",
                defaults: new { controller = "UserAttributeValue" },
                constraints: new { id = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

            config.Routes.MapHttpRoute(
                name: "User Attribute Values by name",
                routeTemplate: "api/user/{name}/attributes",
                defaults: new { controller = "UserAttributeValue" },
                constraints: new { name = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });
        }
    }
}
