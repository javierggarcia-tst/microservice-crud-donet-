using CRUDBasico;
using Microsoft.Extensions.Configuration;

namespace CRUDBasico.FunctionalTests
{
    public class ServicioLiquidacionTestsStartup : Startup
    {
        public ServicioLiquidacionTestsStartup(IConfiguration env) : base(env)
        {
        }

        //protected override void ConfigureAuth(IApplicationBuilder app)
        //{
        //    if (Configuration["isTest"] == bool.TrueString.ToLowerInvariant())
        //    {
        //        app.UseMiddleware<AutoAuthorizeMiddleware>();
        //    }
        //    else
        //    {
        //        base.ConfigureAuth(app);
        //    }
        //}
    }
}
