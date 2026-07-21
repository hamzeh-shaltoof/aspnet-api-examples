
namespace RouteConstraints.RouteConstraints
{
    public class RouteConstraintsDate : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var routeValue)
                 || !values.TryGetValue("month",out var monthValue)
                 || !values.TryGetValue("day",out var dayValue) )
                return false;

            int.TryParse(routeValue?.ToString(), out var year);
            int.TryParse(monthValue?.ToString(), out var month);
            int.TryParse(dayValue?.ToString(), out var day);

            if ((year >= 2022 && year <= 2026) &&
                (month >= 1 && month <= 12) &&
                 (day >= 1 && day <= 31))
                return true;

            return false;
        }
    }
}
