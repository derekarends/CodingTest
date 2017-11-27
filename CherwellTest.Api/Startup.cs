using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(CherwellTest.Api.Startup))]

namespace CherwellTest.Api
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      HttpConfiguration config = new HttpConfiguration();

      SimpleInjectorConfig.Register(config);
      FormatterConfig.Register(config);
      WebApiConfig.Register(config);

      app.UseWebApi(config);
    }
  }
}
