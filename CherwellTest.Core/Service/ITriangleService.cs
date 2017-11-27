using CherwellTest.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherwellTest.Core.Service
{
  public interface ITriangleService
  {
    Task<bool> CreateAsync();
    Task<IEnumerable<Triangle>> GetAsync();
    Task<Triangle> GetAsync(Vertex v1, Vertex v2, Vertex v3);
  }
}
