using System;
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
            config.Routes.MapHttpRoute(name: "User Authentication", routeTemplate: "api/user/authenticate", defaults: new { controller = "authenticate" });

            RegisterUserImageRoutes(config);

			RegisterUserAttributeCategoriesRoutes(config);
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
                routeTemplate: "api/user/{userId}",
                defaults: new { controller = "user" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

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
				routeTemplate: "api/user/attributes/{attributeId}",
				defaults: new { controller = "UserAttributes" },
				constraints: new { attributeId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attributes by Name",
				routeTemplate: "api/user/attributes/{attributeName}",
				defaults: new { controller = "UserAttributes" },
				constraints: new { attributeName = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attributes without id",
				routeTemplate: "api/user/attributes",
				defaults: new { controller = "userattributes"});
	}

		private static void RegisterUserAttributeCategoriesRoutes(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "User Attribute Categories by Id",
				routeTemplate: "api/user/attributes/category/{categoryId}",
				defaults: new { controller = "UserAttributeCategories" },
				constraints: new { categoryId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Categories by Id",
				routeTemplate: "api/user/attributes/category/{categoryName}",
				defaults: new { controller = "UserAttributeCategories" },
				constraints: new { categoryName = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Categories by Id",
				routeTemplate: "api/user/attributes/category",
				defaults: new { controller = "UserAttributeCategories" });
		}

        private static void RegisterUserAttributeValueRoutes(HttpConfiguration config)
        {
			config.Routes.MapHttpRoute(
					name: "User Attribute Values by Id and CategoryName",
					routeTemplate: "api/user/{userId}/attributes/{categoryName}",
					defaults: new { controller = "UserAttributeValue" },
					constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by Name and CategoryName",
				routeTemplate: "api/user/{name}/attributes/{categoryName}",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { name = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by Id",
				routeTemplate: "api/user/{userId}/attributes",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by name",
				routeTemplate: "api/user/{name}/attributes",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { name = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });
		}

        private static void RegisterUserImageRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "User Image Thumbnail by Id",
                routeTemplate: "api/user/{userId}/images/{imageId}/thumbnail",
                defaults: new { controller = "imagethumbnail" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

            config.Routes.MapHttpRoute(
                name: "User Image by Id",
                routeTemplate: "api/user/{userId}/images/{imageId}",
                defaults: new { controller = "image" },
                constraints: new 
                { 
                    userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")),
                    imageId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+"))
                });

            config.Routes.MapHttpRoute(
                name: "User Image",
                routeTemplate: "api/user/{userId}/images",
                defaults: new { controller = "image" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });
        }
    }
}
