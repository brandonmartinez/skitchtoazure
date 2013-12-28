using System.Web.Http;

namespace SkitchToAzure
{
    public static class WebApiConfig
    {
        #region Static Methods

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormatter = config.Formatters.JsonFormatter;
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "{controller}/{id}", defaults: new
            {
                controller = "uploads",
                id = RouteParameter.Optional
            });
        }

        #endregion
    }
}