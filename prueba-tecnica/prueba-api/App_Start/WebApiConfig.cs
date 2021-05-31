using System.Web.Http;
using System.Web.Http.Cors;
using core.util;
using core.util.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace prueba_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            //Se asigna al manejador de mensajes la validacion con el TokenValidationHandler para JWT
            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Se habilita el cors para las rutas definidas en el config
            var cors = new EnableCorsAttribute($"{"allowedcors".GetValueAppSetting<string>()}", "*", "*");//Habilita el CORS para swagger
            config.EnableCors(cors);

            // Set JSON formatter as default one and remove XmlFormatter

            var jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
