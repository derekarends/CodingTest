namespace CherwellTest.Core.Model
{
  public class Vertex
  {
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object obj)
    {
      var other = obj as Vertex;
      if (other == null)
        return false;

      return other.X == X && other.Y == Y;
    }

    public override int GetHashCode()
    {
      return X.GetHashCode() * 31 + Y.GetHashCode();
    }
  }
}
