namespace ShortestRouteOptimizer.Models.Internal;

public struct Trail<T>
{
    public required int Distance { get; init; }
    public required IReadOnlyCollection<T> Path { get; init; } 
}