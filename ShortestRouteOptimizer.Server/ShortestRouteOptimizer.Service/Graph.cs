using ShortestRouteOptimizer.Contracts;
using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Service;

public class Graph<T> : IGraph<T> where T : class, IComparable<T>
{
    private readonly T[] _vertices;
    private Dictionary<T, List<Tuple<T, int>>> _adj;

    public Graph(T[] vertices)
    {
        _vertices = vertices;
        _adj = new();
        foreach (var vertex in vertices)
        {
            _adj[vertex] = new List<Tuple<T, int>>();
        }
    }

    public void AddEdge(T u, T v, int weight)
    {
        _adj[u].Add(new Tuple<T, int>(v, weight));
    }

    public void AddBidirectionalEdge(T u, T v, int weight)
    {
        AddEdge(u, v, weight);
        AddEdge(v, u, weight);
    }

    public Dictionary<T, Trail<T>> Dijkstra(T source)
    {
        if (!_adj.ContainsKey(source))
        {
            throw new ArgumentException(nameof(source));
        }

        // Priority queue to store vertices that are being preprocessed
        var pq = new SortedSet<Tuple<T, int>>(Comparer<Tuple<T, int>>.Create((x, y) =>
        {
            int result = x.Item2.CompareTo(y.Item2);
            return result == 0 ? x.Item1.CompareTo(y.Item1) : result;
        }));

        // Distances from source to all vertices
        Dictionary<T, int> distance = [];

        // Predecessors of all vertices
        Dictionary<T, T?> predecessor = [];
        foreach (var vertex in _vertices)
        {
            distance[vertex] = int.MaxValue;
            predecessor[vertex] = null;
        }

        distance[source] = 0;

        // Add source to priority queue
        pq.Add(new Tuple<T, int>(source, 0));

        while (pq.Count > 0)
        {
            // Remove the vertex with the smallest distance
            var u = pq.Min.Item1;
            pq.Remove(pq.Min);

            // 'u' is now in the shortest path
            foreach (var edge in _adj[u])
            {
                T v = edge.Item1;
                int weight = edge.Item2;

                // Check if there's a shorter path to v through u
                if (distance[u] + weight < distance[v])
                {
                    if (distance[v] != int.MaxValue)
                    {
                        pq.Remove(new Tuple<T, int>(v, distance[v]));
                    }

                    distance[v] = distance[u] + weight;
                    predecessor[v] = u;
                    pq.Add(new Tuple<T, int>(v, distance[v]));
                }
            }
        }

        var dijkstra = new Dictionary<T, Trail<T>>();

        foreach (var vertex in _vertices)
        {
            if (distance[vertex] != int.MaxValue)
            {
                // Build the path by traversing predecessors from destination to source 
                List<T> path = [vertex];
                
                T? p = predecessor[vertex];
                while (p is not null)
                {
                    path = path.Prepend(p).ToList();
                    p = predecessor[p];
                }

                dijkstra[vertex] = new Trail<T>
                {
                    Distance = distance[vertex],
                    Path = path
                };
            }
        }

        return dijkstra;
    }
}