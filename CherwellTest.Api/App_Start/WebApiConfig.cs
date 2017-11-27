using System.Web.Http;

namespace CherwellTest.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
    }
  }
}
