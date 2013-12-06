
namespace Graphing
{
    internal class PointSequenceInvalidException : System.Exception
    {
        public PointSequenceInvalidException(string message = 
            "The Sequence of Points is invalid") : base(message) { } 
    }
}
