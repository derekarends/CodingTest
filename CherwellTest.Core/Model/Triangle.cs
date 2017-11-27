using CherwellTest.Core.Enum;
using System.Collections.Generic;

namespace CherwellTest.Core.Model
{
  public class Triangle
  {
    public Row Row { get; set; }
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
