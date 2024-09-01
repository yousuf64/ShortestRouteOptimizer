using ShortestRouteOptimizer.Models.Internal;

namespace ShortestRouteOptimizer.Contracts;

public interface IGraph<T> where T : IComparable<T>
{
    void AddEdge(T u, T v, int weight);
    void AddBidirectionalEdge(T u, T v, int weight);
    Dictionary<T, Trail<T>> Dijkstra(T source);
}