using System.Globalization;
using System.Text.RegularExpressions;

namespace Homework_TV_MVC.Constraints
{
    public class SlugConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue("slug", out var slug))
                return false;
            var SlugAsString = Convert.ToString(slug, CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(SlugAsString))
                return false;
            return Regex.IsMatch(SlugAsString, "^[a-zA-Z ]+$");
        }
    }
}