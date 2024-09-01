using ShortestRouteOptimizer.Contracts;
using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Service;

public class Optimizer : IOptimizer
{
    private readonly IGraph<string> _graph;
    private readonly string[] _nodes;
    
    // A -> [
    //  { B, <6, [A, B]> }
    //  { C, <9, [A, B, C]> }
    // ]
    private readonly IDictionary<string, IDictionary<string, Trail<string>>> _dijkstraCache = new Dictionary<string, IDictionary<string, Trail<string>>>();
    
    public Optimizer()
    {
        _nodes = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];
        _graph = new Graph<string>(_nodes);
        _graph.AddEdge("A", "B", 4);
        _graph.AddEdge("B", "A", 4);
        _graph.AddEdge("A", "C", 6);
        _graph.AddEdge("C", "A", 6);
        _graph.AddEdge("B", "F", 2);
        _graph.AddEdge("F", "B", 2);
        _graph.AddEdge("C", "D", 8);
        _graph.AddEdge("D", "C", 8);
        _graph.AddEdge("F", "E", 3);
        _graph.AddEdge("E", "F", 3);
        _graph.AddEdge("E", "D", 4);
        _graph.AddEdge("D", "E", 4);
        _graph.AddEdge("E", "B", 2);
        _graph.AddEdge("F", "G", 4);
        _graph.AddEdge("G", "F", 4);
        _graph.AddEdge("F", "H", 6);
        _graph.AddEdge("H", "F", 6);
        _graph.AddEdge("G", "H", 5);
        _graph.AddEdge("H", "G", 5);
        _graph.AddEdge("G", "I", 5);
        _graph.AddEdge("I", "G", 5);
        _graph.AddEdge("I", "E", 8);
        _graph.AddEdge("E", "I", 8);
    }

    public IReadOnlyCollection<string> GetNodes() => _nodes;

    public Trail<string> ShortestPath(string fromNode, string toNode)
    {
        if (!_dijkstraCache.TryGetValue(fromNode, out var dijkstra))
        {
            try
            {
                dijkstra = _graph.Dijkstra(fromNode);
                _dijkstraCache[fromNode] = dijkstra;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(null, nameof(fromNode));
            }
        }

        if (!dijkstra.TryGetValue(toNode, out var trail))
        {
            throw new ArgumentException(null, nameof(toNode));
        }
        
        return trail;
    }
}