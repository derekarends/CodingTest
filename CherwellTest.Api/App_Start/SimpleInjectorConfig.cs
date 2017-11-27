using CherwellTest.Core.Repository;
using CherwellTest.Core.Service;
using CherwellTest.Repository;
using CherwellTest.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace CherwellTest.Api
{
  public static class SimpleInjectorConfig
  {
    public static void Register(HttpConfiguration config)
    {
      var container = new Container();
      container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

      container.Register<ITriangleRepository, TriangleRepository>();
      container.Register<ITriangleService, TriangleService>();

      container.RegisterWebApiControllers(config);

      container.Verify();

      config.DependencyResolver =
        new SimpleInjectorWebApiDependencyResolver(container);
    }
  }
}