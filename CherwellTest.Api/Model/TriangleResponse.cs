using CherwellTest.Core.Model;
using System.Collections.Generic;

namespace CherwellTest.Api.Model
{
  public class TriangleResponse
  {
    public string Row { get; set; }
    public int Column { get; set; }

    private List<Vertex> _vertices;
    public List<Vertex> Vertices
    {
      get
      {
        if (_vertices == null)
          _vertices = new List<Vertex>();

        return _vertices;
      }

      set { _vertices = value; }
    }
  }
}