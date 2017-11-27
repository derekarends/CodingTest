using CherwellTest.Core.Model;
using CherwellTest.Core.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherwellTest.Repository
{
  public class TriangleRepository : ITriangleRepository
  {
    private static List<Triangle> _triangles = new List<Triangle>();

    public async Task<IEnumerable<Triangle>> GetAsync()
    {
      return await Task.Run(() => _triangles);
    }

    public async Task<bool> IntializeAsync(IEnumerable<Triangle> triangles)
    {
      return await Task.Run(() =>
      {
        if (_triangles.Count > 0)
          return false;

        _triangles.AddRange(triangles);

        return true;
      });
    }
  }
}
