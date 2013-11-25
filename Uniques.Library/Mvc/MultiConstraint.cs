using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Uniques.Library.Mvc
{
    public class MultiConstraint : IRouteConstraint
    {
        private readonly IEnumerable<IRouteConstraint> _constraints; 

        public MultiConstraint(IRouteConstraint routeConstraint, IRouteConstraint secondRouteConstraint)
        {
            _constraints = new List<IRouteConstraint> { routeConstraint, secondRouteConstraint };
        }

        public MultiConstraint(IEnumerable<IRouteConstraint> routeConstraints)
        {
            _constraints = routeConstraints;
        }

        /// <summary>
        /// Determines whether the URL parameter contains a valid value for this constraint.
        /// </summary>
        /// <returns>
        /// true if the URL parameter contains a valid value; otherwise, false.
        /// </returns>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param><param name="route">The object that this constraint belongs to.</param><param name="parameterName">The name of the parameter that is being checked.</param><param name="values">An object that contains the parameters for the URL.</param><param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated.</param>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _constraints.All(c => c.Match(httpContext, route, parameterName, values, routeDirection));
        }
    }
}
