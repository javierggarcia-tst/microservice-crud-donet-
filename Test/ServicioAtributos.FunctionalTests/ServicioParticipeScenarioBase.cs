using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace CRUDBasico.FunctionalTests
{
    public class ServicioLiquidacionScenarioBase
    {
        public IHostBuilder CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(ServicioLiquidacionScenarioBase))
                .Location;

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                   
                    webHost.Configure(app => app.Run(async ctx =>
                    await ctx.Response.WriteAsync("Hello World!")));
                });

            return hostBuilder;
        }

        public IHostBuilder CreateServerFunctional()
        {
            IHostBuilder host = new HostBuilder();

            try
            {
                host = Host.CreateDefaultBuilder()
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         webBuilder.UseTestServer();
                         webBuilder.UseStartup<Startup>();
                     });

            }catch(Exception e)
            {
                throw e;
            }

            return host;
        }
             

        public static class Get
        {
            public static string Atributos = "/atributos";

            public static string AtributosBAD = "/atributos1";

            public static string ValidacionAgrupadas = "/liquidacion/validacionagrupadas";

            public static string AtributosBy(int id)
            {
                return $"/atributos/{id}";
            }
        }

        public static class Put
        {
            //public static string CancelOrder = "api/v1/orders/cancel";
            //public static string ShipOrder = "api/v1/orders/ship";
        }
    }
}
