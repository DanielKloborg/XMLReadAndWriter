
namespace Actors {
  internal class Actor {
    private string name;
    internal string Name {
      get { return name; }
      set { name = value; }
    }

    internal string Role { get; set; }
    internal int Year { get; set; }
    internal string Nationality { get; set; }

    public override string ToString() {
      return "[Actor: name=" + name +
                   ", role=" + Role +
                   ", year=" + Year +
            ", nationality=" + Nationality + "]";
    }
  }
}