using CherwellTest.Core.Enum;
using CherwellTest.Core.Model;
using CherwellTest.Core.Repository;
using CherwellTest.Core.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CherwellTest.Service
{
  public class TriangleService : ITriangleService
  {
    private readonly ITriangleRepository _triangleRepository;

    public TriangleService(ITriangleRepository triangleRepository)
    {
      _triangleRepository = triangleRepository;
    }

    public async Task<bool> CreateAsync()
    {
      var rows = (Row[])Enum.GetValues(typeof(Row));
      var columns = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

      var triangles = new List<Triangle>();

      foreach (var row in rows)
      {
        foreach (var column in columns)
        {
          var triangle = new Triangle
          {
            Row = row,
            Column = column,
            Vertices = CalculateVertices(row, column)
          };

          triangles.Add(triangle);
        }
      }

      return await _triangleRepository.IntializeAsync(triangles);
    }
    
    public async Task<IEnumerable<Triangle>> GetAsync()
    {
      return await _triangleRepository.GetAsync();
    }

    public async Task<Triangle> GetAsync(Vertex v1, Vertex v2, Vertex v3)
    {
      if (v1 == v2 || v1 == v3 || v2 == v3)
        return new Triangle();

      var triangles = await _triangleRepository.GetAsync();
      foreach (var triangle in triangles)
      {
        if(triangle.Vertices.Contains(v1) &&
           triangle.Vertices.Contains(v2) && 
           triangle.Vertices.Contains(v3))
        {
          return triangle;
        }
      }

      return new Triangle();
    }

    private List<Vertex> CalculateVertices(Row row, int column)
    {
      const int sideLength = 10;

      var result = new List<Vertex>();
      
      var topYCoord = (int)row * sideLength;
      var bottomYCoord = topYCoord + sideLength;

      var isOddColumn = column % 2 == 1;
      if(isOddColumn)
      {
        var rightAngleXCoord = (int)Math.Floor(column / 2d) * sideLength;
        var rightXCoord = rightAngleXCoord + sideLength;
       
        result.Add(new Vertex { X = rightAngleXCoord, Y = topYCoord });
        result.Add(new Vertex { X = rightAngleXCoord, Y = bottomYCoord });
        result.Add(new Vertex { X = rightXCoord, Y = bottomYCoord });
      }
      else
      {
        var rightAngleXCoord = column / 2 * sideLength;
        var leftXCoord = rightAngleXCoord - sideLength;
       
        result.Add(new Vertex { X = leftXCoord, Y = topYCoord });
        result.Add(new Vertex { X = rightAngleXCoord, Y = topYCoord });
        result.Add(new Vertex { X = rightAngleXCoord, Y = bottomYCoord });
      }

      return result;
    }
  }
}
