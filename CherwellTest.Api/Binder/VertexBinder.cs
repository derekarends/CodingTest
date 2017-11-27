using CherwellTest.Core.Model;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace CherwellTest.Api.Binder
{
  public class VertexBinder : IModelBinder
  {
    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    {
      if (bindingContext.ModelType != typeof(Vertex))
      {
        return false;
      }

      var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (val == null)
      {
        return false;
      }

      var key = val.RawValue as string;
      if (key == null)
      {
        bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
        return false;
      }

      var values = key.Split(',');
      if (values.Length == 2)
      {
        int x;
        if (!int.TryParse(values[0], out x))
          bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Cannot parse X value {values[0]}");

        int y;
        if (!int.TryParse(values[1], out y))
          bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Cannot parse Y value {values[1]}");

        if (bindingContext.ModelState.IsValid)
        {
          bindingContext.Model = new Vertex { X = x, Y = y };
          return true;
        }
      }


      bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Cannot convert value to Vertex");
      return false;
    }
  }
}