
namespace Graphing
{
    internal class PointOutofBoundsException : System.Exception
    {
        public PointOutofBoundsException(string message = 
            "A point is out of bounds from the graph") : base(message) { } 
    }
}
