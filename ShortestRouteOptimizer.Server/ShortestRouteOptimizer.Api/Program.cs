using Microsoft.AspNetCore.Mvc;
using ShortestRouteOptimizer.Contracts;
using ShortestRouteOptimizer.Models.Api;
using ShortestRouteOptimizer.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOptimizer, Optimizer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var v1 = app.MapGroup("/api/v1");

v1.MapGet("/nodes", ([FromServices] IOptimizer optimizer) => optimizer.GetNodes())
    .WithName("GetNodes")
    .WithOpenApi();

v1.MapGet("/shortest-path/{fromNodeName}/{toNodeName}",
        ([FromServices] IOptimizer optimizer, string fromNodeName, string toNodeName) =>
        {
            var trail = optimizer.ShortestPath(fromNodeName, toNodeName);
            return new ShortestPathData
            {
                Distance = trail.Distance,
                NodeNames = [..trail.Path]
            };
        })
    .WithName("CalculateShortestPath")
    .WithOpenApi();

app.Run();