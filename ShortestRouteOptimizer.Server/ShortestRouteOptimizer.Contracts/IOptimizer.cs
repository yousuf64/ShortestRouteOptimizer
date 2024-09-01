using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Contracts;

public interface IOptimizer
{
    IReadOnlyCollection<string> GetNodes();
    Trail<string> ShortestPath(string fromNode, string toNode);
}