namespace ShortestRouteOptimizer.Models.Api;

public struct ShortestPathData
{
    public required int Distance { get; init; }
    public required List<string> NodeNames { get; init; }
}