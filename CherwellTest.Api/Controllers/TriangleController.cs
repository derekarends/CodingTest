using CherwellTest.Api.Binder;
using CherwellTest.Api.Model;
using CherwellTest.Core.Enum;
using CherwellTest.Core.Model;
using CherwellTest.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CherwellTest.Api.Controllers
{
  [RoutePrefix("api/v1/Triangle")]
  public class TriangleController : ApiController
  {
    private readonly ITriangleService _triangleService;

    public TriangleController(ITriangleService triangleService)
    {
      _triangleService = triangleService;
    }

    // api/v1/triangle
    [HttpPost, Route("")]
    public async Task<bool> Create()
    {
      return await _triangleService.CreateAsync();
    }

    // api/v1/triangle
    [HttpGet, Route("")]
    public async Task<IEnumerable<TriangleResponse>> Get()
    {
      var triangles = await _triangleService.GetAsync();

      var result = triangles.Select(s => new TriangleResponse
      {
        Row = Enum.GetName(typeof(Row), s.Row),
        Column = s.Column,
        Vertices = s.Vertices
      });

      return result;
    }

    // api/v1/triangle/v1/10,20/v2/10,30/v3/20,30
    [HttpGet, Route("v1/{v1}/v2/{v2}/v3/{v3}")]
    public async Task<RowColumnResponse> Get([ModelBinder(typeof(VertexBinder))] Vertex v1, 
                                            [ModelBinder(typeof(VertexBinder))] Vertex v2, 
                                            [ModelBinder(typeof(VertexBinder))] Vertex v3)
    {
      var triangle = await _triangleService.GetAsync(v1, v2, v3);

      var result = new RowColumnResponse
      {
        Row = Enum.GetName(typeof(Row), triangle.Row),
        Column = triangle.Column
      };

      return result;
    }
  }
}
