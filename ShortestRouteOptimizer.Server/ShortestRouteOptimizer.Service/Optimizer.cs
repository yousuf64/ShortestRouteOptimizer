using ShortestRouteOptimizer.Contracts;
using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Service;

public class Optimizer : IOptimizer
{
    private readonly Graph<string> _graph;
    private readonly string[] _nodes;
    
    // A -> [
    //  { B, <6, [A, B]> }
    //  { C, <9, [A, B, C]> }
    // ]
    private readonly Dictionary<string, IDictionary<string, Trail<string>>> _dijkstraCache = new();
    
    public Optimizer()
    {
        _nodes = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];
        _graph = new Graph<string>(_nodes);
        _graph.AddBidirectionalEdge("A", "B", 4);
        _graph.AddBidirectionalEdge("A", "C", 6);
        _graph.AddBidirectionalEdge("B", "F", 2);
        _graph.AddBidirectionalEdge("C", "D", 8);
        _graph.AddBidirectionalEdge("D", "E", 4);
        _graph.AddBidirectionalEdge("D", "G", 1);
        _graph.AddEdge("E", "B", 2);
        _graph.AddBidirectionalEdge("E", "F", 3);
        _graph.AddBidirectionalEdge("E", "I", 8);
        _graph.AddBidirectionalEdge("F", "G", 4);
        _graph.AddBidirectionalEdge("F", "H", 6);
        _graph.AddBidirectionalEdge("G", "H", 5);
        _graph.AddBidirectionalEdge("G", "I", 5);
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