using CherwellTest.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherwellTest.Core.Repository
{
  public interface ITriangleRepository
  {
    Task<IEnumerable<Triangle>> GetAsync();
    Task<bool> IntializeAsync(IEnumerable<Triangle> triangle);
  }
}
