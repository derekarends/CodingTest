using CherwellTest.Core.Enum;
using CherwellTest.Core.Model;
using CherwellTest.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CherwellTest.Service.Test
{
  [TestClass]
  public class TriangleServiceTest
  {
    private TriangleService _triangleService;
    private Mock<ITriangleRepository> _triangleRepsitoryMock;

    [TestInitialize]
    public void Initialize()
    {
      _triangleRepsitoryMock = new Mock<ITriangleRepository>();
      _triangleService = new TriangleService(_triangleRepsitoryMock.Object);
    }

    [TestMethod]
    public async Task Create_NoErrors_ReturnsTrue()
    {
      var triangles = new List<Triangle>();
      _triangleRepsitoryMock.Setup(s => s.IntializeAsync(It.IsAny<IEnumerable<Triangle>>()))
                            .Callback<IEnumerable<Triangle>>(c => triangles.AddRange(c))
                            .ReturnsAsync(true);

      var actual = await _triangleService.CreateAsync();

      Assert.IsTrue(actual);

      var a1 = triangles.FirstOrDefault(f => f.Row == Row.A && f.Column == 1);
      Assert.IsNotNull(a1);
      Assert.IsTrue(a1.Vertices.Contains(new Vertex { X = 0, Y = 0 }));
      Assert.IsTrue(a1.Vertices.Contains(new Vertex { X = 0, Y = 10 }));
      Assert.IsTrue(a1.Vertices.Contains(new Vertex { X = 10, Y = 10 }));

      var d5 = triangles.FirstOrDefault(f => f.Row == Row.D && f.Column == 5);
      Assert.IsNotNull(d5);
      Assert.IsTrue(d5.Vertices.Contains(new Vertex { X = 20, Y = 30 }));
      Assert.IsTrue(d5.Vertices.Contains(new Vertex { X = 20, Y = 40 }));
      Assert.IsTrue(d5.Vertices.Contains(new Vertex { X = 30, Y = 40 }));

      var d6 = triangles.FirstOrDefault(f => f.Row == Row.D && f.Column == 6);
      Assert.IsNotNull(d6);
      Assert.IsTrue(d6.Vertices.Contains(new Vertex { X = 20, Y = 30 }));
      Assert.IsTrue(d6.Vertices.Contains(new Vertex { X = 30, Y = 30 }));
      Assert.IsTrue(d6.Vertices.Contains(new Vertex { X = 30, Y = 40 }));

      var f12 = triangles.FirstOrDefault(f => f.Row == Row.F && f.Column == 12);
      Assert.IsNotNull(f12);
      Assert.IsTrue(f12.Vertices.Contains(new Vertex { X = 50, Y = 50 }));
      Assert.IsTrue(f12.Vertices.Contains(new Vertex { X = 60, Y = 50 }));
      Assert.IsTrue(f12.Vertices.Contains(new Vertex { X = 60, Y = 60 }));
    }

    [TestMethod]
    public async Task Get_WithValidVertices_FindsOneResult()
    {
      var vertices = new List<Vertex>
      {
        new Vertex { X = 0, Y = 0 },
        new Vertex { X = 10, Y = 0 },
        new Vertex { X = 10, Y = 10 }
      };

      var triangles = new List<Triangle>
      {
        new Triangle
        {
          Row = Row.A,
          Column = 1,
          Vertices = vertices
        }
      };

      _triangleRepsitoryMock.Setup(s => s.GetAsync()).ReturnsAsync(triangles);

      var actual = await _triangleService.GetAsync(vertices[1], vertices[2], vertices[0]);
      
      Assert.IsNotNull(actual);
      Assert.AreEqual(Row.A, actual.Row);
      Assert.AreEqual(1, actual.Column);
    }

    [TestMethod]
    public async Task Get_WithVerticesInMiddleOfCollection_FindsOneResult()
    {
      var triangles = new List<Triangle>
      {
        new Triangle
        {
          Row = Row.A,
          Column = 1,
          Vertices = new List<Vertex>
          {
            new Vertex { X = 0, Y = 0 },
            new Vertex { X = 10, Y = 0 },
            new Vertex { X = 10, Y = 10 }
          }
        },
        new Triangle
        {
          Row = Row.B,
          Column = 3,
          Vertices = new List<Vertex>
          {
            new Vertex { X = 10, Y = 10 },
            new Vertex { X = 10, Y = 20 },
            new Vertex { X = 20, Y = 20 }
          }
        },
        new Triangle
        {
          Row = Row.B,
          Column = 4,
          Vertices = new List<Vertex>
          {
            new Vertex { X = 10, Y = 10 },
            new Vertex { X = 20, Y = 10 },
            new Vertex { X = 20, Y = 20 }
          }
        }
      };

      _triangleRepsitoryMock.Setup(s => s.GetAsync()).ReturnsAsync(triangles);

      var actual = await _triangleService.GetAsync(new Vertex { X = 10, Y = 10 },
                                                   new Vertex { X = 10, Y = 20 },
                                                   new Vertex { X = 20, Y = 20 });

      Assert.IsNotNull(actual);
      Assert.AreEqual(Row.B, actual.Row);
      Assert.AreEqual(3, actual.Column);
    }

    [TestMethod]
    public async Task Get_WithInValidVertices_FindsNoResult()
    {
      var triangles = new List<Triangle>
      {
        new Triangle
        {
          Row = Row.A,
          Column = 1,
          Vertices = new List<Vertex>
          {
            new Vertex { X = 0, Y = 0 },
            new Vertex { X = 10, Y = 0 },
            new Vertex { X = 10, Y = 10 }
          }
        }
      };

      _triangleRepsitoryMock.Setup(s => s.GetAsync()).ReturnsAsync(triangles);

      var actual = await _triangleService.GetAsync(new Vertex { X = 0, Y = 0 },
                                                   new Vertex { X = 10, Y = 0 },
                                                   new Vertex { X = 0, Y = 10 });

      Assert.IsNotNull(actual);
      Assert.AreEqual(Row.A, actual.Row);
      Assert.AreEqual(0, actual.Column);
    }
  }
}
