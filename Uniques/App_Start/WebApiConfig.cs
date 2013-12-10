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
            config.Routes.MapHttpRoute(name: "User Authentication", routeTemplate: "api/users/authenticate", defaults: new { controller = "authenticate" });

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
				name: "Users by Filter ",
				routeTemplate: "api/users/{filter}",
				defaults: new { controller = "user", search = true },
				constraints: new { filter = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"^where\(.*\)$")) });

            config.Routes.MapHttpRoute(
                name: "User by Id ",
                routeTemplate: "api/users/{userId}",
                defaults: new { controller = "user" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"^\d+$")) });

            config.Routes.MapHttpRoute(
                name: "User by Loginname",
                routeTemplate: "api/users/{loginname}",
                defaults: new { controller = "user" },
				constraints: new { loginname = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z0-9]+")) });
		}

		private static void RegisterUserAttributesRoutes(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "User Attributes by Id",
				routeTemplate: "api/users/attributes/{attributeId}",
				defaults: new { controller = "UserAttributes" },
				constraints: new { attributeId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attributes by Name",
				routeTemplate: "api/users/attributes/{attributeName}",
				defaults: new { controller = "UserAttributes" },
				constraints: new { attributeName = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attributes without id",
				routeTemplate: "api/users/attributes",
				defaults: new { controller = "userattributes"});
	}

		private static void RegisterUserAttributeCategoriesRoutes(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "User Attribute Categories by Id",
				routeTemplate: "api/users/attributes/categories/{categoryId}",
				defaults: new { controller = "UserAttributeCategories" },
				constraints: new { categoryId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Categories by Name",
				routeTemplate: "api/users/attributes/categories/{categoryName}",
				defaults: new { controller = "UserAttributeCategories" },
				constraints: new { categoryName = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Categories without Name",
				routeTemplate: "api/users/attributes/categories",
				defaults: new { controller = "UserAttributeCategories" });
		}

        private static void RegisterUserAttributeValueRoutes(HttpConfiguration config)
        {
			config.Routes.MapHttpRoute(
					name: "User Attribute Values by Id and CategoryName",
					routeTemplate: "api/users/{userId}/attributes/{categoryName}",
					defaults: new { controller = "UserAttributeValue" },
					constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by Name and CategoryName",
				routeTemplate: "api/users/{loginname}/attributes/{categoryName}",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { loginname = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by Id",
				routeTemplate: "api/users/{userId}/attributes",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

			config.Routes.MapHttpRoute(
				name: "User Attribute Values by name",
				routeTemplate: "api/users/{loginname}/attributes",
				defaults: new { controller = "UserAttributeValue" },
				constraints: new { loginname = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"[a-zA-z]+")) });
		}

        private static void RegisterUserImageRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "User Image Thumbnail by Id",
                routeTemplate: "api/users/{userId}/images/{imageId}/thumbnail",
                defaults: new { controller = "imagethumbnail" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });

            config.Routes.MapHttpRoute(
                name: "User Image by Id",
                routeTemplate: "api/users/{userId}/images/{imageId}",
                defaults: new { controller = "image" },
                constraints: new 
                { 
                    userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")),
                    imageId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+"))
                });

            config.Routes.MapHttpRoute(
                name: "User Image",
                routeTemplate: "api/users/{userId}/images",
                defaults: new { controller = "image" },
                constraints: new { userId = new MultiConstraint(new NotNullConstraint(), new RegexConstraint(@"\d+")) });
        }
    }
}
