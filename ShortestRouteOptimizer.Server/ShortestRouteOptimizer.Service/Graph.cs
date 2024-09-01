using ShortestRouteOptimizer.Contracts;
using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Service;

public class Graph<T> : IGraph<T> where T : IComparable<T>
{
    // private int _vertices;
    private T[] _vertices;
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
        // new Tuple<T, int>(v, weight)
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
        Dictionary<T, int> dist = [];
        Dictionary<T, T?> prev = [];
        foreach (var vertex in _vertices)
        {
            dist[vertex] = int.MaxValue;
            prev[vertex] = default;
        }

        dist[source] = 0;

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
                if (dist[u] + weight < dist[v])
                {
                    if (dist[v] != int.MaxValue)
                    {
                        pq.Remove(new Tuple<T, int>(v, dist[v]));
                    }

                    dist[v] = dist[u] + weight;
                    prev[v] = u;
                    pq.Add(new Tuple<T, int>(v, dist[v]));
                }
            }
        }

        // Print the distances from the source to all vertices
        var s = new Dictionary<T, Trail<T>>();

        foreach (var vertex in _vertices)
        {
            if (dist[vertex] == int.MaxValue)
            {
                Console.WriteLine($"Vertex {vertex} is unreachable from the source {source}.");
            }
            else
            {
                List<T> path = [vertex];
                T? p = prev[vertex];
                if (p is not null)
                {
                    while (p is not null)
                    {
                        path = path.Prepend(p).ToList();
                        p = prev[p];
                    }
                }

                s[vertex] = new Trail<T>
                {
                    Distance = dist[vertex],
                    Path = path
                };

                Console.Write($"Vertex {vertex}, Distance from source: {dist[vertex]}, Path: ");
                PrintPath(vertex, prev);
                Console.WriteLine();
            }
        }

        return s;
    }

    private void PrintPath(T vertex, Dictionary<T, T?> prev)
    {
        if (prev[vertex] is null)
        {
            Console.Write(vertex);
        }
        else
        {
            PrintPath(prev[vertex]!, prev);
            Console.Write(" -> " + vertex);
        }
    }
}